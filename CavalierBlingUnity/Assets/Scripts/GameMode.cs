using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMode : AbstractSingleton<GameMode>
{
    public enum GameOverCondition
    {
        OutOfScreen,
        Madness, 
        NotEnoughMoney
    }

    [SerializeField] 
    private Canvas _canvas;
    [SerializeField] 
    private GameObject _gameOverScreen;
    [SerializeField] 
    private GameObject _winScreen;
    [SerializeField] 
    private GameObject _ennemy;
    
    public int dayCount = 0;

    private void Start()
    {
        Time.timeScale = 1f;
    }

    public void DayEnd()
    {
        dayCount++;
        DailyTax.Instance.DeductTax();
        // Call shop 
    }
    
    public void WinGame()
    {
        Time.timeScale = 0f;
        Instantiate(_winScreen, _canvas.transform);
    }
    
    public void GameOver(GameOverCondition gameOverCondition)
    {
        Time.timeScale = 0f;
        Instantiate(_gameOverScreen, _canvas.transform);
        
        switch (gameOverCondition)
        {
            case GameOverCondition.OutOfScreen:
                break;
            case GameOverCondition.Madness:
                break;
            case GameOverCondition.NotEnoughMoney:
                break;
        }
    }

    public void SpawnEnnemy()
    {
        
    }
}
