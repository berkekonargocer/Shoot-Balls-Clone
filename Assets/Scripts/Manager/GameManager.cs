using NOJUMPO;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

[DisallowMultipleComponent]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [field: SerializeField] public AudioClip BackgroundMusic { get; private set; }
    [field: SerializeField] public AudioClip WinGameSFX { get; private set; }
    [field: SerializeField] public AudioClip LoseGameSFX { get; private set; }

    public static event Action<float> OnShooterExperienceChanged;
    public event Action OnStartGame;
    public event Action<float> OnLoseGame;
    public event Action<float> OnWinGame;

    public float ShooterExperience { get; private set; } = 0.00f;
    int _shooterLevel = 0;
    bool isGameOver = false;

    public float BallPower { get; private set; } = 0.5f;


    void OnEnable() {
        Shooter.OnScored += IncreaseShooterExperience;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable() {
        Shooter.OnScored -= IncreaseShooterExperience;
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void Awake() {
        InitializeSingleton();
    }

    void IncreaseShooterExperience(float increaseAmount) {
        ShooterExperience += increaseAmount / 100;
        OnShooterExperienceChanged?.Invoke(ShooterExperience);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode) {
        isGameOver = false;
    }

    public void StartGame() {
        OnStartGame?.Invoke();
        AudioManager.Instance.PlayMusic(BackgroundMusic);
    }

    public void WinGame() {
        AudioManager.Instance.StopMusic();
        AudioManager.Instance.PlaySFX(WinGameSFX);
        OnWinGame?.Invoke(ScoreManager.Instance.Score);
    }

    public void LoseGame() {
        if (isGameOver)
            return;

        isGameOver = true;
        AudioManager.Instance.StopMusic();
        AudioManager.Instance.PlaySFX(LoseGameSFX);
        OnLoseGame?.Invoke(ScoreManager.Instance.Score);
    }

    public void RestartLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void InitializeSingleton() {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}