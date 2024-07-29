using System;
using UnityEngine;


namespace NOJUMPO
{
    public class ScoreManager : MonoBehaviour
    {
        // --------------------------------- FIELDS --------------------------------
        public static ScoreManager Instance { get; private set; }

        public float Score { get { return _score; } private set { _score = Mathf.Clamp(value, 0, float.MaxValue); } }
        float _score;

        public event Action<float> OnScoreChanged;

        public float Income { get; private set; } = 25.0f;


        void OnEnable() {
            UpgradeManager.Instance.OnIncomeUpgrade += OnIncomeUpgrade;
        }

        void OnDisable() {
            UpgradeManager.Instance.OnIncomeUpgrade -= OnIncomeUpgrade;
        }

        void Awake() {
            InitializeSingleton();
        }


        public void SetScore(float newScore) {
            Score = newScore;
            OnScoreChanged?.Invoke(Score);
        }

        public void IncrementScore(float incrementAmount) {
            Score += incrementAmount;
            OnScoreChanged?.Invoke(Score);
        }

        public void DecrementScore(float decrementAmount) {
            Score -= decrementAmount;
            OnScoreChanged?.Invoke(Score);
        }


        void OnIncomeUpgrade(Upgradeable upgradeable) {
            Income *= upgradeable.currentLevelValue;
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
}