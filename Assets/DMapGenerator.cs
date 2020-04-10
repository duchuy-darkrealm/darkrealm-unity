using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DMapGenerator : MonoBehaviour
{
    class MapObject
    {
        public MapObject(Vector3 position, string objectName)
        {
            this.position = position;
            this.objectName = objectName;
        }
        public Vector3 position { get; set; }
        public string objectName { get; set; }
        public GameObject gameObject { get; set; }
    }

    class Pivot
    {
        public Pivot(Vector3 position, DMapHabitat habitat)
        {
            this.position = position;
            this.habitat = habitat;
        }

        public Vector3 position { get; set; }
        public DMapHabitat habitat { get; set; }
    }

    float MAP_SIZE_X = 30f; //60
    float MAP_SIZE_Y = 30f; //60
    int SPAWN_NUMBER = 1000; //4000

    public Vector3 PLAYER_START_PÓSITION;
    public float START_RADIUS = 2f;
    public DMapHabitat[] habitats;
    public int[] habitatNumbers;

    List<Pivot> pivots;
    // Main list contain information about position but not render to GameObject 
    List<MapObject> mapObjects;

    // Contain objects that will render
    List<MapObject> pools;
    List<int> poolIndexs;
    List<string> poolSeens;

    List<int> currentSeenIds;

    float SEEN_RANGE = 7f;

    void Start()
    {
        pivots = new List<Pivot>();
        mapObjects = new List<MapObject>();
        pools = new List<MapObject>();
        currentSeenIds = new List<int>();
        poolIndexs = new List<int>();
        poolSeens = new List<string>();

        CreateMap();
    }

    public void CreateMap()
    {
        for (int i = 0; i < habitats.Length; i++)
        {
            for (int j = 0; j < habitatNumbers[i]; j++)
            {
                Vector3 position = new Vector3(Random.Range(-MAP_SIZE_X, MAP_SIZE_X), Random.Range(-MAP_SIZE_Y, MAP_SIZE_Y));
                pivots.Add(new Pivot(position, habitats[i]));
            }
        }

        for (int i = 0; i < SPAWN_NUMBER; i++)
        {
            Vector3 random = new Vector3(Random.Range(-MAP_SIZE_X, MAP_SIZE_X), Random.Range(-MAP_SIZE_Y, MAP_SIZE_Y));
            if (Vector3.Distance(random, PLAYER_START_PÓSITION) < START_RADIUS)
                continue;

            int nearest = NearestPoint(random);

            AddWithHabitat(random, pivots[nearest].habitat);
        }

        for (int i = 0; i < mapObjects.Count; i++)
        {
            DGameSystem.LoadPool(mapObjects[i].objectName, mapObjects[i].position);
        }
    }

    public int NearestPoint(Vector3 input)
    {
        int result = 0;

        float min = Vector3.Distance(input, pivots[result].position);

        for (int i = 1; i < pivots.Count; i++)
        {
            if (Vector3.Distance(input, pivots[i].position) < min)
            {
                result = i;
                min = Vector3.Distance(input, pivots[i].position);
            }
        }
        return result;
    }

    void AddWithHabitat(Vector3 position, DMapHabitat habitat)
    {
        float random = Random.Range(0, 100);
        int pivot = 0;
        for (int i = 0; i < habitat.objectNames.Length; i++)
        {
            pivot += habitat.percents[i];
            if (random < pivot)
            {
                mapObjects.Add(new MapObject(position, habitat.objectNames[i]));
                return;
            }
        }
    }

    private void Update()
    {
        //UpdatePool();
    }
    
    void UpdatePool()
    {
        if (DGameSystem.player == null) return;

        List<int> seen = new List<int>();
        List<int> unseen = new List<int>();
        for (int i = 0; i < mapObjects.Count; i++)
        {
            if (Vector3.Distance(mapObjects[i].position, DGameSystem.player.transform.position) < SEEN_RANGE)
                seen.Add(i);
            else
                unseen.Add(i);
        }

        // Scan poolIndexs để biết được object nào seen, object nào unseen
        for (int i = 0; i < poolIndexs.Count; i++)
        {
            if (seen.Contains(poolIndexs[i]))
                poolSeens[i] = "seen";
            else
                poolSeens[i] = "unseen";
        }

        for (int i = 0; i < seen.Count; i++)
        {
            // Nếu poolIndexs không chứa seen[i] (xuất hiện mới), tìm trong unseen object có name tương tự
            // để change vị trí cho vật xuất hiện mới
            // Trường hợp không tìm thấy thì tạo object mới và thêm vào pool
            if (!poolIndexs.Contains(seen[i]))
            {
                int index = seen[i];
                bool found = false;
                for (int j = 0; j < poolSeens.Count; j++)
                {
                    if (poolSeens[j] == "unseen" && pools[j].objectName == mapObjects[index].objectName)
                    {
                        pools[j].gameObject.transform.position = mapObjects[index].position;
                        poolSeens[j] = "seen";
                        poolIndexs[j] = index;
                        found = true;
                        break;
                    }
                }

                if (!found)
                {
                    GameObject obj = DGameSystem.LoadPool(mapObjects[index].objectName, mapObjects[index].position);
                    MapObject mapObj = mapObjects[index];
                    mapObj.gameObject = obj;
                    pools.Add(mapObj);
                    poolIndexs.Add(index);
                    poolSeens.Add("seen");
                }
            }
        }
    }
}
