using System;
using UnityEngine;


namespace NOJUMPO
{
    public class ScoreManager : MonoBehaviour
    {
        // --------------------------------- FIELDS --------------------------------
        public static ScoreManager Instance { get; private set; }

        public int Score { get { return _score; } private set { _score = Mathf.Clamp(value, 0, int.MaxValue); } }
        int _score;

        public event Action<int> OnScoreChanged;


        // ------------------------- UNITY BUILT-IN METHODS ------------------------
        void Awake() {
            InitializeSingleton();
        }


        // ------------------------- CUSTOM PUBLIC METHODS -------------------------
        public void SetScore(int newScore) {
            Score = newScore;
            OnScoreChanged?.Invoke(Score);
        }

        public void IncrementScore(int incrementAmount) {
            Score += incrementAmount;
            OnScoreChanged?.Invoke(Score);
        }

        public void DecrementScore(int decrementAmount) {
            Score -= decrementAmount;
            OnScoreChanged?.Invoke(Score);
        }



        // ------------------------- CUSTOM PRIVATE METHODS ------------------------
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
}