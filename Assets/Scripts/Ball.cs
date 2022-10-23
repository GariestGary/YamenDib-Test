using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using VolumeBox.Toolbox;
using UnityEngine.InputSystem;

public class Ball : MonoCached
{
    [SerializeField] private float gravity;
    [SerializeField] private float jumpVelocity;
    [SerializeField] [Tag] private string outAreaTag;
    [SerializeField] [Tag] private string passAreaTag;

    [Inject] private Messager msg;
    [Inject] private GameManager game;
    
    private float yVelocity;
    private bool jumpedOnce;
    private bool pressed;

    public override void FixedTick()
    {
        if (!game.IsGameOver && game.GameStarted)
        {
            UpdateVelocity();
            ApplyVelocity();
            CheckBoundaries();
        }
    }

    private void CheckBoundaries()
    {
        Vector3 currentPosition = transform.position;
        
        if (currentPosition.y >= game.Top)
        {
            currentPosition.y = game.Top;
            transform.position = currentPosition;
            yVelocity = 0;
        }

        if (currentPosition.y <= game.Bottom)
        {
            currentPosition.y = game.Bottom;
            transform.position = currentPosition;
            Fail();
        }
    }

    private void Fail()
    {
        msg?.Send(Message.OUT_OF_GATE);
    }

    private void UpdateVelocity()
    {
        if (pressed)
        {
            yVelocity = jumpVelocity;
        }
        else
        {
            yVelocity -= gravity * fixedDelta;
        }
    }

    private void ApplyVelocity()
    {
        transform.position += Vector3.up * yVelocity * fixedDelta;
    }

    public void Jump(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            if (!jumpedOnce)
            {
                msg?.Send(Message.GAME_STARTED);
                jumpedOnce = true;
            }

            pressed = true;
        }

        if (ctx.canceled)
        {
            pressed = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag(outAreaTag))
        {
            Fail();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(passAreaTag))
        {
            msg?.Send(Message.PASSED_GATE);
        }
    }
}
