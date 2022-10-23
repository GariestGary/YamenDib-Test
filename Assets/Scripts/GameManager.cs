using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VolumeBox.Toolbox;

public class GameManager : CachedSingleton<GameManager>
{
    private bool isGameOver;
    private int currentScore;
    private bool gameStarted;
    
    public float Top { get; private set; }
    public float Bottom { get; private set; }
    public float Left { get; private set; }
    public float Right { get; private set; }

    public bool GameStarted => gameStarted;
    public bool IsGameOver => isGameOver;
    public int CurrentScore => currentScore;

    [Inject] private Messager msg;
    [Inject] private Traveler trvl;

    public void RecalculateBoundaries()
    {
        Vector3 pointHeight = Camera.main.ViewportToWorldPoint(Vector3.right);
        Vector3 pointWidth = Camera.main.ViewportToWorldPoint(Vector3.up);

        Top = -pointHeight.y;
        Bottom = pointHeight.y;
        Left = pointWidth.x;
        Right = -pointWidth.x;
    }

    public override void Rise()
    {
        RecalculateBoundaries();
        msg.Subscribe(Message.OUT_OF_GATE, _ => OutOfGateHandle());
        msg.Subscribe(Message.PASSED_GATE, _ => currentScore++);
        msg.Subscribe(Message.GAME_STARTED, _ => gameStarted = true);
    }

    private void OutOfGateHandle()
    {
        isGameOver = true;
    }

    public void RestartGame()
    {
        trvl.LoadScene(trvl.CurrentLevelName);
    }
}
