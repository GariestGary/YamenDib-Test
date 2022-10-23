using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using VolumeBox.Toolbox;

public class UIHandler : MonoCached
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject tipText;
    [SerializeField] private GameObject restartButton;

    [Inject] private Messager msg;
    [Inject] private GameManager game;
    
    public override void Rise()
    {
        msg.Subscribe(Message.GAME_STARTED, _ => OnGameStart());
        msg.Subscribe(Message.OUT_OF_GATE, _ => OnGameOver());
        msg.Subscribe(Message.PASSED_GATE, _ => OnGatePass());
        msg.Subscribe(Message.RESTART_GAME, _ => OnGameRestart());
        OnGameRestart();
    }

    private void OnGatePass()
    {
        scoreText.text = $"Score: {game.CurrentScore}";
    }
    
    private void OnGameStart()
    {
        tipText.SetActive(false);
    }

    private void OnGameRestart()
    {
        OnGatePass();
        restartButton.SetActive(false);
        tipText.SetActive(true);
    }

    private void OnGameOver()
    {
        restartButton.SetActive(true);
    }
}
