using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    InProgress = 1,
    InShop = 2,
    Ending = 3
}

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
    private GameState _mGameState = GameState.InProgress;
    public GameState GameState { get => _mGameState; }
	
    [SerializeField]
    private GameObject _chevalier;

    public int dayCount { get => GlobalDataHolder.Instance.CurrentDay; }
    public bool isGameOver = false; 

    private void Start()
    {
        Time.timeScale = 1f;
        DailyTax.Instance.DisplayTax();

        SpawnEnnemies();
        _mGameState = GameState.InProgress;
    }

    public void DayEnd()
    {
        GlobalDataHolder.Instance.IncreaseDayCount();
        DailyTax.Instance.DeductTax();
		
        if (_mGameState != GameState.Ending)
        {
            Shop.Instance.InitializeShopOfTheDay();

            _mGameState = GameState.InShop;
            Camera.main.enabled = false;
            ShopSceneManager.Instance.StartShoppingLoop();
        }
    }

    public void StartNewDay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        IncreaseDifficulty(dayCount);
    }
    
    public void WinGame()
    {
        Time.timeScale = 0f;
        Instantiate(_winScreen, _canvas.transform);
        _mGameState = GameState.Ending;
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

        _mGameState = GameState.Ending;
        
        // Stop All Audio
        AudioListener[] allAudioListeners = FindObjectsOfType<AudioListener>();
        foreach (AudioListener listener in allAudioListeners)
        {
            listener.enabled = false;
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
