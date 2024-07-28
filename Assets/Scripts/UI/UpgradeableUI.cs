using TMPro;
using UnityEngine;
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
    }

    void OnDisable() {
        UpgradeManager.Instance.OnBallUpgrade -= UpdateBallUI;
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
            ballUpgradeAmountText.text = $"MAX";
            ballMoneyAmountText.text = $"MAX";
            return;
        }

        ballUpgradeAmountText.text = $"{upgradeable.currentLevelValue} -> {upgradeable.nextLevelValue}";
        ballMoneyAmountText.text = $"{upgradeable.upgradeCost}";
    }
}
