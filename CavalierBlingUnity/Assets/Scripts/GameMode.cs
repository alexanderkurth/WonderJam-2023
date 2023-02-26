using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    private float _TimeBeforeStart = 3.0f;

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

        StartChevalierAndEnemies();
        SpawnEnnemies();
        Inventory.Instance.ChangeCurrencyValue(0);
        _mGameState = GameState.InProgress;

    }

    void StartChevalierAndEnemies()
    {
        _chevalier.GetComponent<ChevalierMove>().SetStartTimer(_TimeBeforeStart);
        _chevalier.GetComponentInChildren<EnemySpawner>().SetStartTimer(_TimeBeforeStart);
    }


    public void DayEnd()
    {
        DailyTax.Instance.DeductTax();
        GlobalDataHolder.Instance.IncreaseDayCount();

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
        GameObject winScreen = Instantiate(_winScreen, _canvas.transform);
        Button firstButton = winScreen.GetComponentInChildren<Button>();
        EventSystem.current.SetSelectedGameObject(firstButton.gameObject);
        _mGameState = GameState.Ending;
    }
    
    public void GameOver(GameOverCondition gameOverCondition)
    {
        Time.timeScale = 0f;
        GameObject gameOver = Instantiate(_gameOverScreen, _canvas.transform);
        Button firstButton = gameOver.GetComponentInChildren<Button>();
        EventSystem.current.SetSelectedGameObject(firstButton.gameObject);
        
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
