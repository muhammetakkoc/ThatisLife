using UnityEngine;

public class WoodSpawner : MonoBehaviour
{
    public GameObject treePrfab;
    public Transform treeSpawnArea;

    [SerializeField]public int minTrees =7;
    [SerializeField] public int maxTrees = 15;
    [Range(0,1f)]public float treeSpawnRate;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (Random.value > treeSpawnRate) return;
        Bounds bounds = treeSpawnArea.GetComponent<SpriteRenderer>().bounds;

        int treeCount = Random.Range(minTrees, maxTrees + 1);
        for (int i = 0; i < treeCount; ++i)
        {
            float x = Random.Range(bounds.min.x, bounds.max.x);
            float y = Random.Range(bounds.min.y, bounds.max.y);

            Instantiate(treePrfab, new Vector2(x, y), Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
