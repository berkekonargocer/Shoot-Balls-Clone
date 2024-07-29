using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UpgradeableUI : MonoBehaviour
{
    [SerializeField] GameObject ballUpgradeArrow;
    [SerializeField] TextMeshProUGUI ballsText;
    [SerializeField] TextMeshProUGUI ballUpgradeAmountText;
    [SerializeField] Image ballSprite;
    [SerializeField] Image ballMoneySprite;
    [SerializeField] TextMeshProUGUI ballMoneyAmountText;

    [Space]

    [SerializeField] GameObject evolveUpgradeArrow;
    [SerializeField] TextMeshProUGUI evolveText;
    [SerializeField] TextMeshProUGUI evolveUpgradeAmountText;
    [SerializeField] Image evolveSprite;
    [SerializeField] Image evolveMoneySprite;
    [SerializeField] TextMeshProUGUI evolveMoneyAmountText;

    [Space]

    [SerializeField] GameObject incomeUpgradeArrow;
    [SerializeField] TextMeshProUGUI incomesText;
    [SerializeField] TextMeshProUGUI incomeUpgradeAmountText;
    [SerializeField] Image incomeSprite;
    [SerializeField] Image incomeMoneySprite;
    [SerializeField] TextMeshProUGUI incomeMoneyAmountText;


    void OnEnable() {
        UpgradeManager.Instance.OnBallUpgrade += UpdateBallUI;
        UpgradeManager.Instance.OnEvolveUpgrade += UpdateEvolveUI;
        UpgradeManager.Instance.OnIncomeUpgrade += UpdateIncomeUI;
    }

    void OnDisable() {
        UpgradeManager.Instance.OnBallUpgrade -= UpdateBallUI;
        UpgradeManager.Instance.OnEvolveUpgrade -= UpdateEvolveUI;
        UpgradeManager.Instance.OnIncomeUpgrade -= UpdateIncomeUI;
    }


    void Start() {

    }

    void Update() {

    }


    void UpdateBallUI(Upgradeable upgradeable) {
        if (upgradeable.IsMaxLevel)
        {
            ballUpgradeArrow.gameObject.SetActive(false);
            ballMoneySprite.gameObject.SetActive(false);
            ballUpgradeAmountText.text = $"{upgradeable.currentLevelValue}";
            ballMoneyAmountText.text = $"MAX";
            return;
        }

        ballUpgradeAmountText.text = $"{upgradeable.currentLevelValue} -> {upgradeable.nextLevelValue}";
        ballMoneyAmountText.text = $"{upgradeable.upgradeCost}";
    }

    void UpdateEvolveUI(Upgradeable upgradeable) {
        if (upgradeable.IsMaxLevel)
        {
            evolveUpgradeArrow.gameObject.SetActive(false);
            evolveMoneySprite.gameObject.SetActive(false);
            evolveUpgradeAmountText.text = $"{upgradeable.currentLevelValue}";
            evolveMoneyAmountText.text = $"MAX";
            return;
        }

        evolveUpgradeAmountText.text = $"{upgradeable.currentLevelValue} -> {upgradeable.nextLevelValue}";
        evolveMoneyAmountText.text = $"{upgradeable.upgradeCost}";
    }

    void UpdateIncomeUI(Upgradeable upgradeable) {
        if (upgradeable.IsMaxLevel)
        {
            incomeUpgradeArrow.gameObject.SetActive(false);
            incomeMoneySprite.gameObject.SetActive(false);
            incomeUpgradeAmountText.text = $"{upgradeable.currentLevelValue}";
            incomeMoneyAmountText.text = $"MAX";
            return;
        }

        incomeUpgradeAmountText.text = $"{upgradeable.currentLevelValue} -> {upgradeable.nextLevelValue}";
        incomeMoneyAmountText.text = $"{upgradeable.upgradeCost}";
    }
}
