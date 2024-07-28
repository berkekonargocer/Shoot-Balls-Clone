using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager Instance;

    public event Action OnBallUpgrade;
    public event Action OnEvolveUpgrade;
    public event Action OnIncomeUpgrade;

    [SerializeField] Upgradeable currentBallUpgrade;
    [SerializeField] Upgradeable currentEvolveUpgrade;
    [SerializeField] Upgradeable currentIncomeUpgrade;


    void Awake() {
        InitializeSingleton();
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }


    public void UpgradeBall(Upgradeable upgradeable) {
        currentBallUpgrade = upgradeable;
        OnBallUpgrade?.Invoke();
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
