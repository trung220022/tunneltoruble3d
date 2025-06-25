using UnityEngine;

public class MapScroller : MonoBehaviour
{
    public float scrollSpeed;
    public float acceleration;
    public float resetZ;

    private MapManager mapManager;

    void Start()
    {
        mapManager = FindObjectOfType<MapManager>();
    }

    void Update()
    {
        if (!GameManager.Instance.isGameStarted)
            return;

        if (acceleration > 0)
            scrollSpeed += acceleration * Time.deltaTime;

        transform.Translate(Vector3.back * scrollSpeed * Time.deltaTime);

        if (transform.position.z < resetZ)
        {
            mapManager.RecycleMap(gameObject);
        }
    }
}
