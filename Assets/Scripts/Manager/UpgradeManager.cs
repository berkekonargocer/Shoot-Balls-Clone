using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UpgradeManager : MonoBehaviour
{

    public static UpgradeManager Instance;

    public event Action<Upgradeable> OnBallUpgrade;
    public event Action<Upgradeable> OnEvolveUpgrade;
    public event Action<Upgradeable> OnIncomeUpgrade;

    [SerializeField] Upgradeable currentBallUpgrade;
    [SerializeField] Upgradeable currentEvolveUpgrade;
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

    void Start() {

    }

    void Update() {

    }

    void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode) {
        SetUpgrades();
    }

    void SetUpgrades() {
        OnBallUpgrade?.Invoke(currentBallUpgrade);
        OnEvolveUpgrade?.Invoke(currentEvolveUpgrade);
        OnIncomeUpgrade?.Invoke(currentIncomeUpgrade);
    }


    public void UpgradeBall() {
        if (currentBallUpgrade.IsMaxLevel)
            return;

        currentBallUpgrade = currentBallUpgrade.nextUpgrade;
        OnBallUpgrade?.Invoke(currentBallUpgrade);
    }

    public void UpgradeEvolve() {
        if (currentEvolveUpgrade.IsMaxLevel)
            return;

        currentEvolveUpgrade = currentEvolveUpgrade.nextUpgrade;
        OnEvolveUpgrade?.Invoke(currentEvolveUpgrade);
    }

    public void UpgradeIncome() {
        if (currentIncomeUpgrade.IsMaxLevel)
            return;

        currentIncomeUpgrade = currentIncomeUpgrade.nextUpgrade;
        OnIncomeUpgrade?.Invoke(currentIncomeUpgrade);
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
