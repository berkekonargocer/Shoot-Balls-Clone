using UnityEngine;

[CreateAssetMenu(fileName = "NewUpgradeable", menuName = "NOJUMPO/Scriptable Objects/Upgradeable/New Upgradeable")]
public class Upgradeable : ScriptableObject
{
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public Upgradeable nextUpgrade { get; private set; }
    [field: SerializeField] public float currentLevelValue { get; private set; }
    [SerializeField] public float nextLevelValue { get { return nextUpgrade.currentLevelValue; } }
    [field: SerializeField] public float upgradeCost { get; private set; }
    [field: SerializeField] public Sprite sprite { get; private set; }
    [field: SerializeField] public Material ShooterEvolveMaterial { get; private set; }
    [field: SerializeField] public bool IsMaxLevel { get; private set; }


}
