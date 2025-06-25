using UnityEngine;

public class MapManager : MonoBehaviour
{
    [Header("Danh sách prefab Map")]
    public GameObject[] mapPrefabs;

    [Header("Số lượng map active")]
    public int mapCount = 3;

    [Header("Vị trí Z mong muốn cho mỗi map")]
    public float[] startPositions; // Nhập trong Inspector: VD: [0, 0, 45]

    private GameObject[] spawnedMaps;

    void Start()
    {
        spawnedMaps = new GameObject[mapCount];

        for (int i = 0; i < mapCount; i++)
        {
            SpawnMapAtIndex(i);
        }
    }

    void SpawnMapAtIndex(int index)
    {
        int rand = Random.Range(0, mapPrefabs.Length);
        float customZ = startPositions[index]; // Dùng vị trí do bạn nhập
        GameObject newMap = Instantiate(mapPrefabs[rand], new Vector3(0, 0, customZ), Quaternion.identity);
        spawnedMaps[index] = newMap;
    }

    public void RecycleMap(GameObject oldMap)
    {
        int rand = Random.Range(0, mapPrefabs.Length);
        GameObject newMap = Instantiate(mapPrefabs[rand]);

        float highestZ = float.MinValue;
        foreach (var map in spawnedMaps)
        {
            if (map != null && map.transform.position.z > highestZ)
                highestZ = map.transform.position.z;
        }

        newMap.transform.position = new Vector3(0, 0, highestZ + 95f); // Vẫn giữ khoảng cách 100 cho map tiếp theo

        Destroy(oldMap);
        for (int i = 0; i < spawnedMaps.Length; i++)
        {
            if (spawnedMaps[i] == oldMap)
            {
                spawnedMaps[i] = newMap;
                break;
            }
        }
    }
}
