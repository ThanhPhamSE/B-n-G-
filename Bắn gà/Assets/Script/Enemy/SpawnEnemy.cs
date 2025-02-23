using System.Collections;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] private string enemyTag = "Enemy";
    [SerializeField] private EnemyController enemyPrefab;
    [SerializeField] private float enemySpawnInterval;
    [SerializeField] private EnemyPath[] paths;
    [SerializeField] private bool active;
    [SerializeField] private int minTotalEnemies;
    [SerializeField] private int maxTotalEnemies;
    [SerializeField] private int totalGroups;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //StartCoroutine(IfTestCoroutine());
        //StartCoroutine(IESpawnEnemies());
        StartCoroutine(IESpawnGroups(totalGroups));
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator IESpawnGroups(int groups)
    {
        //for (int i = 0; i < groups; i++)
        //{
        //    int totalEnemies = Random.Range(minTotalEnemies, maxTotalEnemies);
        //    EnemyPath path = paths[Random.Range(0, paths.Length)];
        //    yield return StartCoroutine(IESpawnEnemies(totalEnemies, path));
        //    yield return new WaitForSeconds(3f);
        //}

        while (true) // Lặp vô hạn
        {
            for (int i = 0; i < groups; i++)
            {
                int totalEnemies = Random.Range(minTotalEnemies, maxTotalEnemies);
                EnemyPath path = paths[Random.Range(0, paths.Length)];
                yield return StartCoroutine(IESpawnEnemies(totalEnemies, path));
                yield return new WaitForSeconds(3f); // Nghỉ giữa các nhóm
            }

            yield return new WaitForSeconds(5f); // Chờ trước khi lặp lại toàn bộ vòng
        }
    }
    private IEnumerator IESpawnEnemies(int totalEnemies, EnemyPath path)
    {
        for (int i = 0; i < totalEnemies; i++)
        {
            {
                yield return new WaitUntil(() => active);
                yield return new WaitForSeconds(enemySpawnInterval);
                //EnemyController enemy = Instantiate(enemyPrefab, transform);
                //enemy.Init(path.WayPoints);

                // Lấy enemy từ ObjectPool thay vì Instantiate
                GameObject enemyObj = ObjectPool.instance.GetPooledObject("Enemy", transform.position, Quaternion.identity);

                if (enemyObj != null)
                {
                    EnemyController enemy = enemyObj.GetComponent<EnemyController>();
                    enemy.Init(path.WayPoints);
                }
                else
                {
                    Debug.LogWarning("Không tìm thấy Enemy trong ObjectPool!");
                }
            }
        }
    }
}
