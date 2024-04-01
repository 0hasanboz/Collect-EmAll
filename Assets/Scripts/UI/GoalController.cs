using Game.Level;
using UnityEngine;

namespace UI
{
    public class GoalController : MonoBehaviour
    {
        [SerializeField] private GoalView _goalView;
        private GoalObjectData _goalObjectData;

        public void SetGoal(GoalObjectData goalObjectData)
        {
            _goalObjectData = goalObjectData;
            _goalView.SetGoal(goalObjectData.desiredObject, goalObjectData.desiredAmount);
        }

        public void DecreaseTargetAmount(int amount = 1)
        {
            _goalObjectData.desiredAmount -= amount;
            _goalView.UpdateGoalText(_goalObjectData.desiredAmount);
        }

        public bool IsGoalCompleted()
        {
            return _goalObjectData.desiredAmount <= 0;
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}