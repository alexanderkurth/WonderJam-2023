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
    private int _ennemyCount = 10;
    [SerializeField]
    private GameObject _chevalier;

    public int dayCount = 0;
    public bool isGameOver = false; 

    private void Start()
    {
        Time.timeScale = 1f;
        SpawnEnnemies();
    }

    public void DayEnd()
    {
        dayCount++;
        DailyTax.Instance.DeductTax();
        // Call shop 

        IncreaseDifficulty(dayCount);
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
        isGameOver = true;
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

    public void SpawnEnnemies()
    {
        EnemySpawner.Instance.StartSpawn(_ennemyCount);
    }


    public void IncreaseDifficulty(int level)
    {
        ChevalierMove chevalier = _chevalier.GetComponent<ChevalierMove>();
        if(chevalier.m_Speed + level >= 10.0f)
        {
            chevalier.m_Speed = 10.0f;
        }
        else
        {
            chevalier.m_Speed += level;
        }

        // TODO : Impact on the Madness
    }
}
