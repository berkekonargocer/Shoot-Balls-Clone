using System;
using NOJUMPO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UpgradeManager : MonoBehaviour
{

    public static UpgradeManager Instance;

    public event Action<Upgradeable> OnBallUpgrade;
    public event Action<Upgradeable> OnEvolveUpgrade;
    public event Action<Upgradeable> OnIncomeUpgrade;

    [field: SerializeField] public Upgradeable CurrentEvolveUpgrade { get; private set; }
    [SerializeField] Upgradeable currentBallUpgrade;
    [SerializeField] Upgradeable currentIncomeUpgrade;

    void OnEnable() {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable() {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void Awake() {
        InitializeSingleton();
    }


    void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode) {
        SetUpgrades();
    }

    void SetUpgrades() {
        OnBallUpgrade?.Invoke(currentBallUpgrade);
        OnEvolveUpgrade?.Invoke(CurrentEvolveUpgrade);
        OnIncomeUpgrade?.Invoke(currentIncomeUpgrade);
    }


    public void UpgradeBall() {
        if (currentBallUpgrade.IsMaxLevel || ScoreManager.Instance.Score < currentBallUpgrade.upgradeCost)
            return;

        currentBallUpgrade = currentBallUpgrade.nextUpgrade;
        OnBallUpgrade?.Invoke(currentBallUpgrade);
        ScoreManager.Instance.DecrementScore(currentBallUpgrade.upgradeCost);
    }

    public void UpgradeEvolve() {
        if (CurrentEvolveUpgrade.IsMaxLevel)
            return;

        CurrentEvolveUpgrade = CurrentEvolveUpgrade.nextUpgrade;
        OnEvolveUpgrade?.Invoke(CurrentEvolveUpgrade);
    }

    public void UpgradeIncome() {
        if (currentIncomeUpgrade.IsMaxLevel || ScoreManager.Instance.Score < currentBallUpgrade.upgradeCost)
            return;

        currentIncomeUpgrade = currentIncomeUpgrade.nextUpgrade;
        OnIncomeUpgrade?.Invoke(currentIncomeUpgrade);
        ScoreManager.Instance.DecrementScore(currentBallUpgrade.upgradeCost);
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
