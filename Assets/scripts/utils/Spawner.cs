using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
    float minOxPos, maxOxPos, minOyPos, maxOyPos;
    float asterRadius;
    Timer spawnerTimer;
    float spawnTime = ConfigurationUtils.AsteroidsSpawnTime;
    float difficultyFrequencyIncreaseMultyplicator = ConfigurationUtils.DifficultySpawnTimeMultiplicator;
    
    void Start() {
        spawnerTimer = gameObject.AddComponent<Timer>();
        spawnerTimer.Duration = spawnTime;
        spawnerTimer.AddTimerFinishedListener(SpawnRandomAsteroid);
        spawnerTimer.Run();

        TEventManager<int>.AddListener(EventEnum.IncreaseDifficultyEvent, IncreaseSpawnFrequency);
        TEventManager<int>.AddListener(EventEnum.ShipDestroyedEvent, StopSpawning);
        TEventManager<int>.AddListener(EventEnum.GameOver, ResetSpawnerDifficulty);

        asterRadius = Resources.Load<GameObject>(@"prefabs\AsteroidPref").GetComponent<CircleCollider2D>().radius;
        float screenZ = -Camera.main.transform.position.z;
        Vector3 botLeftCorner = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, screenZ));
        Vector3 topRightCorner = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, screenZ));
        minOxPos = botLeftCorner.x - asterRadius;
        minOyPos = botLeftCorner.y - asterRadius;
        maxOxPos = topRightCorner.x + asterRadius;
        maxOyPos = topRightCorner.y + asterRadius;

        foreach (Direction side in Enum.GetValues(typeof(Direction))) {
            SpawnAsteroid(side);
        }
    }

    public void SpawnAsteroid(Direction side) {
        Vector2 spawnPosition = new Vector2();
        switch (side) {
            case Direction.Down:
                spawnPosition.y = minOyPos;
                spawnPosition.x = UnityEngine.Random.Range(minOxPos, maxOxPos);
                side = Direction.Up;
                break;
            case Direction.Up:
                spawnPosition.y = maxOyPos;
                spawnPosition.x = UnityEngine.Random.Range(minOxPos, maxOxPos);
                side = Direction.Down;
                break;
            case Direction.Left:
                spawnPosition.x = minOxPos;
                spawnPosition.y = UnityEngine.Random.Range(minOyPos, maxOyPos);
                side = Direction.Right;
                break;
            case Direction.Right:
                spawnPosition.x = maxOxPos;
                spawnPosition.y = UnityEngine.Random.Range(minOyPos, maxOyPos);
                side = Direction.Left;
                break;
        }
        GameObject newAster = ObjectsPool.GetAsteroidFromPool(spawnPosition);
        newAster.GetComponent<Asteroid>().Push(side);
    }
    void SpawnRandomAsteroid() {
        Direction side = (Direction)UnityEngine.Random.Range(0, 3);
        SpawnAsteroid(side);
        spawnerTimer.Run();
    }
    void StopSpawning(int noValue) {
        spawnerTimer.Stop();
    }
    void IncreaseSpawnFrequency(int noValue) {
        spawnerTimer.Stop();
        spawnTime *= (1 - difficultyFrequencyIncreaseMultyplicator);
        spawnerTimer.Duration = spawnTime;
        spawnerTimer.Run();
    }
    void ResetSpawnerDifficulty(int noValue) {
        spawnTime = ConfigurationUtils.AsteroidsSpawnTime;
    }
}
