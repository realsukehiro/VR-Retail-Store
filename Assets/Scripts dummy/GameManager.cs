using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance => _instance;

    public enum GameState { Playing, ProductInteraction, CartOpen, Checkout }
    private GameState _currentState = GameState.Playing;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        // Subscribe to events
        GameEvents.OnGamePaused += HandleGamePaused;
        GameEvents.OnGameResumed += HandleGameResumed;
        GameEvents.OnProductUIOpened += () => SetGameState(GameState.ProductInteraction);
        GameEvents.OnProductUIClosed += () => SetGameState(GameState.Playing);
        GameEvents.OnCartUIToggled += ToggleCartState;
        GameEvents.OnCheckoutCompleted += () => SetGameState(GameState.Checkout);
    }

    private void OnDisable()
    {
        // Unsubscribe from events
        GameEvents.OnGamePaused -= HandleGamePaused;
        GameEvents.OnGameResumed -= HandleGameResumed;
        GameEvents.OnProductUIOpened -= () => SetGameState(GameState.ProductInteraction);
        GameEvents.OnProductUIClosed -= () => SetGameState(GameState.Playing);
        GameEvents.OnCartUIToggled -= ToggleCartState;
        GameEvents.OnCheckoutCompleted -= () => SetGameState(GameState.Checkout);
    }

    private void HandleGamePaused()
    {
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void HandleGameResumed()
    {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void ToggleCartState()
    {
        if (_currentState == GameState.CartOpen)
            SetGameState(GameState.Playing);
        else
            SetGameState(GameState.CartOpen);
    }

    public void SetGameState(GameState newState)
    {
        if (_currentState == newState) return;

        // Exit previous state
        switch (_currentState)
        {
            case GameState.Playing:
                GameEvents.PauseGame();
                break;
            case GameState.ProductInteraction:
            case GameState.CartOpen:
            case GameState.Checkout:
                break;
        }

        // Enter new state
        _currentState = newState;
        switch (_currentState)
        {
            case GameState.Playing:
                GameEvents.ResumeGame();
                break;
            case GameState.ProductInteraction:
            case GameState.CartOpen:
            case GameState.Checkout:
                GameEvents.PauseGame();
                break;
        }
    }

    public GameState GetCurrentState() => _currentState;
}