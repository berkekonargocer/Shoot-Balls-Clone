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

    public event Action OnStartGame;
    public event Action<int> OnLoseGame;
    public event Action<int> OnWinGame;

    bool isGameOver = false;


    void Awake() {
        InitializeSingleton();
    }

    void OnEnable() {

    }

    void Update() {
        
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
        }
        else
        {
            Destroy(gameObject);
        }
    }
}