using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;                        // Tốc độ di chuyển
    public float maxX, maxY;                       // Giới hạn vùng di chuyển theo trục X và Y
    public float tiltAngle;                        // Góc nghiêng trái/phải khi di chuyển
    public float tiltForwardAngle;                 // Góc nghiêng lên/xuống mặc định
    public float tiltSmooth;                       // Độ mượt khi thay đổi góc nghiêng

    public Vector3 defaultRotation = new Vector3(20f, 180f, 0f);  // Rotation mặc định chỉnh trong Unity Inspector

    public AudioClip dieSound;                     // Âm thanh khi chết
    [Range(0f, 0.25f)]
    public float dieVolume = 0.25f;                // Âm lượng phát tiếng die

    public Joystick joystick;                      // Tham chiếu đến joystick (kéo trong Inspector)

    private AudioSource audioSource;
    private bool hasDied = false;
    private const string SoundEffectsKey = "SoundEffects";

    void Start()
    {
        transform.rotation = Quaternion.Euler(defaultRotation);
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    void Update()
    {
        if (!GameManager.Instance.isGameStarted)
            return;

        // ✅ Ưu tiên bàn phím, nếu không có thì lấy từ joystick
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        if (Mathf.Approximately(inputX, 0f) && joystick != null)
            inputX = joystick.Horizontal;

        if (Mathf.Approximately(inputY, 0f) && joystick != null)
            inputY = joystick.Vertical;

        Vector3 move = new Vector3(inputX, inputY, 0f) * moveSpeed * Time.deltaTime;
        Vector3 newPos = transform.position + move;

        newPos.x = Mathf.Clamp(newPos.x, -maxX, maxX);
        newPos.y = Mathf.Clamp(newPos.y, -maxY, maxY);
        transform.position = new Vector3(newPos.x, newPos.y, transform.position.z);

        float tiltZ = inputX * tiltAngle;
        float tiltX = (inputY < 0) ? (tiltForwardAngle + inputY * 60f) : tiltForwardAngle;
        Quaternion targetRot = Quaternion.Euler(tiltX, defaultRotation.y, tiltZ);

        transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, Time.deltaTime * tiltSmooth);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!hasDied && other.CompareTag("MyFinish"))
        {
            hasDied = true;

            bool isSFXOn = PlayerPrefs.GetInt(SoundEffectsKey, 1) == 1;
            if (dieSound != null && isSFXOn)
                audioSource.PlayOneShot(dieSound, dieVolume);

            GameManager.Instance.GameOver();
        }
    }
}
