using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VolumeBox.Toolbox;

public class Gate : MonoCached, IPooled
{
    [SerializeField] private float offscreenDespawnOffset;
    private float speed;

    [Inject] private Pooler pool;
    [Inject] private GameManager game;
    
    public void OnSpawn(object data)
    {
        speed = (float) data;
    }

    public override void FixedTick()
    {
        if (!game.IsGameOver && game.GameStarted)
        {
            transform.position += Vector3.left * speed * fixedDelta;

            if (transform.position.x < game.Left - offscreenDespawnOffset)
            {
                pool.Despawn(gameObject);
            }
        }
    }
}
