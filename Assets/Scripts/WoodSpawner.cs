//using UnityEngine;

//public class WoodSpawner : MonoBehaviour
//{
//    public GameObject treePrfab;
//    public Transform treeSpawnArea;

//    [SerializeField] public int minTrees = 5;
//    [SerializeField] public int maxTrees = 5;
//    [Range(0, 1f)] public float treeSpawnRate = 1f;

//    void Start()
//    {
//        // Kay?t var m? kontrol et
//        if (PlayerPrefs.HasKey("TreeData"))
//        {
//            string data = PlayerPrefs.GetString("TreeData");
//            string[] trees = data.Split(';');
//            foreach (string t in trees)
//            {
//                if (string.IsNullOrEmpty(t)) continue;
//                string[] parts = t.Split(',');
//                float x = float.Parse(parts[0]);
//                float y = float.Parse(parts[1]);
//                bool chopped = parts[2] == "1";
//                if (!chopped)
//                    Instantiate(treePrfab, new Vector2(x, y), Quaternion.identity);
//            }
//            return;
//        }

//        // ?lk defa giri? — rastgele a?aç üret
//        Bounds bounds = treeSpawnArea.GetComponent<SpriteRenderer>().bounds;
//        int treeCount = Random.Range(minTrees, maxTrees + 1);
//        string saveData = "";
//        for (int i = 0; i < treeCount; ++i)
//        {
//            float x = Random.Range(bounds.min.x, bounds.max.x);
//            float y = Random.Range(bounds.min.y, bounds.max.y);
//            Instantiate(treePrfab, new Vector2(x, y), Quaternion.identity);
//            saveData += $"{x},{y},0;";
//        }
//        PlayerPrefs.SetString("TreeData", saveData);
//        PlayerPrefs.Save();
//    }

//    // Bu fonksiyon kesilen a?ac? i?aretler
//    public static void MarkTreeChopped(Vector2 pos)
//    {
//        if (!PlayerPrefs.HasKey("TreeData")) return;
//        string data = PlayerPrefs.GetString("TreeData");
//        string[] trees = data.Split(';');
//        for (int i = 0; i < trees.Length; i++)
//        {
//            if (string.IsNullOrEmpty(trees[i])) continue;
//            string[] parts = trees[i].Split(',');
//            float x = float.Parse(parts[0]);
//            float y = float.Parse(parts[1]);
//            if (Vector2.Distance(new Vector2(x, y), pos) < 0.1f)
//            {
//                trees[i] = $"{x},{y},1"; // 1 = kesildi
//                break;
//            }
//        }
//        PlayerPrefs.SetString("TreeData", string.Join(";", trees));
//        PlayerPrefs.Save();
//    }
//}


using UnityEngine;

public class SimpleTreeSpawner : MonoBehaviour
{
    public GameObject treePrefab;
    public Transform spawnArea;  // SpriteRenderer olan obje

    public int minTrees = 5;
    public int maxTrees = 10;

    void Start()
    {
        // Alan bounding box'unu al
        SpriteRenderer sr = spawnArea.GetComponent<SpriteRenderer>();
        Bounds bounds = sr.bounds;

        // Kaç a?aç ç?kacak
        int treeCount = Random.Range(minTrees, maxTrees + 1);

        for (int i = 0; i < treeCount; i++)
        {
            float x = Random.Range(bounds.min.x, bounds.max.x);
            float y = Random.Range(bounds.min.y, bounds.max.y);

            Vector2 pos = new Vector2(x, y);

            Instantiate(treePrefab, pos, Quaternion.identity);
        }
    }
}
