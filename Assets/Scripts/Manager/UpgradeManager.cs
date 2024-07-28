using System;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager Instance;

    public event Action<Upgradeable> OnBallUpgrade;
    public event Action OnEvolveUpgrade;
    public event Action OnIncomeUpgrade;

    [SerializeField] Upgradeable currentBallUpgrade;
    [SerializeField] Upgradeable currentEvolveUpgrade;
    [SerializeField] Upgradeable currentIncomeUpgrade;


    void Awake() {
        InitializeSingleton();
    }

    void Start() {

    }

    void Update() {

    }


    public void UpgradeBall() {
        if (currentBallUpgrade.IsMaxLevel)
            return;

        currentBallUpgrade = currentBallUpgrade.nextUpgrade;
        OnBallUpgrade?.Invoke(currentBallUpgrade);
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
