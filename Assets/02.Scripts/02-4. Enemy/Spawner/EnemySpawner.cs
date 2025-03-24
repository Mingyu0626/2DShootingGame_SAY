using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct SpawnedEnemyInfo
{
    private EnemyType _enemyType;
    private int _probability;

    public EnemyType EnemyType { get => _enemyType; }
    public int Probability { get => _probability; }

    public SpawnedEnemyInfo(EnemyType enemyType, int probability)
    {
        _enemyType = enemyType;
        _probability = probability;
    }
}

public class EnemySpawner : MonoBehaviour
{
    // 추후 SortedDictionary로 리팩토링
    private List<SpawnedEnemyInfo> spawnedEnemyInfoList = new List<SpawnedEnemyInfo>()
        {
            new SpawnedEnemyInfo(EnemyType.Normal, 40),
            new SpawnedEnemyInfo(EnemyType.Target, 30),
            new SpawnedEnemyInfo(EnemyType.Trace, 20),
            new SpawnedEnemyInfo(EnemyType.Split, 10)
        };

    [SerializeField] private float _delayMin;
    [SerializeField] private float _delayMax;

    [SerializeField] private Direction _spawnedEnemyDirection;
    [SerializeField] private EnemyFactory _enemyFactory;

    private void Awake()
    {
    }
    private void OnEnable()
    {
        StartCoroutine(Spawn());
    }
    private IEnumerator Spawn()
    {
        while (true)
        {
            Enemy enemy = EnemyPool.Instance.GetObject(GetNextSpawnEnemy(), transform.position);
            enemy.InitDirection(_spawnedEnemyDirection);
            yield return new WaitForSeconds(GetRandomSpawnIntervalTime());
        }
    }
    private float GetRandomSpawnIntervalTime()
    {
        return Random.Range(_delayMin, _delayMax);
    }

    private EnemyType GetNextSpawnEnemy()
    {
        int randNum = Random.Range(0, 100);
        int probabilityPrefixSum = 0, enemyIndex = 0;
        foreach (var info in spawnedEnemyInfoList)
        {
            probabilityPrefixSum += info.Probability;
            if (randNum < probabilityPrefixSum)
            {
                return info.EnemyType;
            }
            enemyIndex++;
        }
        return EnemyType.Normal;
    }

}
