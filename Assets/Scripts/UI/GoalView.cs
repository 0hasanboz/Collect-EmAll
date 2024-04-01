using Base;
using Game.Level;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class GoalView : GameView
    {
        [SerializeField] private Image _goalImage;
        [SerializeField] private TMP_Text _goalText;

        public void SetGoal(ObjectData objectData, int desiredAmount)
        {
            _goalImage.sprite = objectData.Icon;
            _goalText.text = desiredAmount.ToString();
        }

        public void UpdateGoalText(int currentAmount)
        {
            _goalText.text = $"{currentAmount}";
        }
    }
}