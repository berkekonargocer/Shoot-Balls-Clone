using NOJUMPO;
using UnityEngine;

public class UpgradeButton : MonoBehaviour
{
    public void UpgradeBall() {
        UpgradeManager.Instance.UpgradeBall();
    }

    public void UpgradeEvolve() {
        if (ScoreManager.Instance.Score > UpgradeManager.Instance.CurrentEvolveUpgrade.upgradeCost)
        {
            GameManager.Instance.IncreaseShooterExperience(10);
        }
    }

    public void UpgradeIncome() {
        UpgradeManager.Instance.UpgradeIncome();
    }
}
