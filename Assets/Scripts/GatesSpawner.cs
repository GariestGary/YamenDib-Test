using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VolumeBox.Toolbox;

public class GatesSpawner : MonoCached
{
    [SerializeField] private string gatePoolTag;
    [SerializeField] private float spawnPointOffset;
    [SerializeField] private float maxSpawnHeight;
    [SerializeField] private float minSpawnHeight;
    [SerializeField] private float gateSpeed;
    [SerializeField] private float startDelay;
    [SerializeField] private float spawnInterval;

    [Inject] private GameManager game;
    [Inject] private Pooler pool;
    [Inject] private Messager msg;

    private Vector3 spawnPoint;

    public override void Rise()
    {
        spawnPoint = new Vector3(game.Right + spawnPointOffset, 0, 0);
        msg.Subscribe(Message.GAME_STARTED, _ => StartSpawning());
    }

    private void StartSpawning()
    {
        SpawnCoroutine().StartManual();
    }

    private IEnumerator SpawnCoroutine()
    {
        yield return startDelay;

        while (!game.IsGameOver && game.GameStarted)
        {
            Vector3 point = spawnPoint;
            point.y = Random.Range(minSpawnHeight, maxSpawnHeight);
            pool.Spawn(gatePoolTag, point, Quaternion.identity, null, gateSpeed);
            yield return spawnInterval;
        }
    }
}
