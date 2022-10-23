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

    private WaitForSeconds delayWait;
    private WaitForSeconds intervalWait;

    public override void Rise()
    {
        spawnPoint = new Vector3(game.Right + spawnPointOffset, 0, 0);
        msg.SubscribeForLevel(Message.GAME_STARTED, _ => StartSpawning());

        delayWait = new WaitForSeconds(startDelay);
        intervalWait = new WaitForSeconds(spawnInterval);
    }

    private void StartSpawning()
    {
        StartCoroutine(SpawnCoroutine());
    }

    private IEnumerator SpawnCoroutine()
    {
        yield return delayWait;

        while (!game.IsGameOver && game.GameStarted)
        {
            Vector3 point = spawnPoint;
            point.y = Random.Range(minSpawnHeight, maxSpawnHeight);
            pool.Spawn(gatePoolTag, point, Quaternion.identity, null, gateSpeed);
            yield return intervalWait;
        }
    }
}
