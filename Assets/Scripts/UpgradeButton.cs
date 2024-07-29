using UnityEngine;

public class UpgradeButton : MonoBehaviour
{
    public void UpgradeBall() {
        UpgradeManager.Instance.UpgradeBall();
    }

    public void UpgradeEvolve() {
        UpgradeManager.Instance.UpgradeEvolve();
    }

    public void UpgradeIncome() {
        UpgradeManager.Instance.UpgradeIncome();
    }
}
