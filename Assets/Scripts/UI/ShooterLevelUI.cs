using UnityEngine;

public class ShooterLevelUI : MonoBehaviour
{
    

    void OnEnable() {
        Shooter.OnShooterExperienceChanged += UpdateShooterLevelUI;
    }

    void OnDisable() {
        Shooter.OnShooterExperienceChanged -= UpdateShooterLevelUI;
    }


    void UpdateShooterLevelUI(float points) {

    }
}
