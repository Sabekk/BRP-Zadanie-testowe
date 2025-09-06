using Gameplay.Inputs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnenmiesController : MonoBehaviour
{
    [SerializeField] private List<EnemyData> AllEnemies;
    [SerializeField] private List<SpawnPoint> SpawnPoints;
    [SerializeField] private GameObject EnemyPrefab;

    private int _maxEnemies = 3;
    private int _currentEnemies = 0;
    private SpawnPoint _currentSelectedPoint;

    private InputManager InputManager => InputManager.Instance;
    private InputBindsController Binds => InputManager?.InputBindsController;

    private void Awake()
    {
        ConfigureEnemiesController();
    }

    private void Start()
    {
        SpawnEnemies();
    }

    private void OnEnable()
    {
        AttachListeners();
    }

    private void OnDisable()
    {
        DettachListeners();
    }

    private void AttachListeners()
    {
        GameEvents.EnemyKilled += EnemyKilled;
        if (Binds != null)
        {
            Binds.GameplayInputs.OnNavigate += HandleNavigate;
        }
    }

    private void DettachListeners()
    {
        GameEvents.EnemyKilled -= EnemyKilled;
        if (Binds != null)
        {
            Binds.GameplayInputs.OnNavigate -= HandleNavigate;
        }
    }

    private void HandleNavigate(Vector2 navigation)
    {
        if (_currentEnemies == 0)
            return;

        if (_currentSelectedPoint != null)
        {
            TrySelectNextPoint(navigation);
        }
        else
        {
            _currentSelectedPoint = SpawnPoints[0];
        }

        RefreshSelection();
    }

    private void TrySelectNextPoint(Vector2 direction)
    {
        if (direction.normalized.x > 0)
            TrySelectNeighbour(1);
        else if (direction.normalized.x < 0)
            TrySelectNeighbour(-1);
    }

    private void RefreshSelection()
    {
        foreach (var spawnPoint in SpawnPoints)
        {
            if (spawnPoint == _currentSelectedPoint)
                spawnPoint.Enemy.OnSelect();
            else
                spawnPoint.Enemy.OnDeselect();
        }
    }

    private void EnemyKilled(EnemyKilledEventArgs e)
    {
        FreeSpawnPoint(e.Enemy.GetEnemyPosition());
        DestroyKilledEnemy(e.Enemy.GetEnemyObject());
        StartCoroutine(SpawnEnemyViaCor());
    }

    private void SpawnEnemies()
    {
        while (_currentEnemies < _maxEnemies)
        {
            SpawnEnemy();
        }
    }

    private IEnumerator SpawnEnemyViaCor()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        SpawnEnemy();
    }

    private void SpawnEnemy()
    {
        if (_currentEnemies >= _maxEnemies)
        {
            Debug.LogError("Max Enemies reached! Kil some to spawn new");
            return;
        }

        int freeSpawnPointIndex = -1;
        for (int i = 0; i < SpawnPoints.Count; i++)
        {
            if (SpawnPoints[i].IsOccupied) continue;

            freeSpawnPointIndex = i;
            break;
        }

        if (freeSpawnPointIndex == -1) return;

        SoulEnemy enemy = Instantiate(EnemyPrefab, SpawnPoints[freeSpawnPointIndex].Position.position, Quaternion.identity, transform).GetComponent<SoulEnemy>();
        SpawnPoints[freeSpawnPointIndex].Enemy = enemy;
        int enemyIntex = Random.Range(0, AllEnemies.Count);
        enemy.SetupEnemy(AllEnemies[enemyIntex], SpawnPoints[freeSpawnPointIndex]);
        _currentEnemies++;
    }

    private void DestroyKilledEnemy(GameObject enemy)
    {
        Destroy(enemy);
    }

    private void FreeSpawnPoint(SpawnPoint spawnPoint)
    {
        for (int i = 0; i < SpawnPoints.Count; i++)
        {
            if (spawnPoint != SpawnPoints[i]) continue;

            if (_currentSelectedPoint == spawnPoint)
                TrySelectNeighbour(1);

            SpawnPoints[i].Enemy = null;
            _currentEnemies--;
            break;
        }
    }

    private void TrySelectNeighbour(int dir)
    {
        int count = SpawnPoints?.Count ?? 0;
        if (count == 0)
            return;

        int currentIndex = _currentSelectedPoint == null ? -1 : SpawnPoints.IndexOf(_currentSelectedPoint);
        if (currentIndex >= count)
            currentIndex = -1;

        SpawnPoint current = _currentSelectedPoint;
        for (int step = 1; step <= count; step++)
        {
            int nextIndex = (currentIndex + step * dir % count + count) % count;
            var newPoint = SpawnPoints[nextIndex];
            if (newPoint != null && newPoint.IsOccupied)
            {
                SetSelected(newPoint); return;
            }
        }

        SetSelected(null);

        void SetSelected(SpawnPoint point)
        {
            if (current == point)
                return;

            if (current?.Enemy != null) current.Enemy.OnDeselect();
            _currentSelectedPoint = point;
            if (point?.Enemy != null) point.Enemy.OnSelect();
        }
    }

    private void ConfigureEnemiesController()
    {
        _maxEnemies = SpawnPoints != null ? SpawnPoints.Count : 3;
    }

}

[System.Serializable]
public class SpawnPoint
{
    public Transform Position;
    public SoulEnemy Enemy;
    public bool IsOccupied => Enemy != null;
}