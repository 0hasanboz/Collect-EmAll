using Base;
using Controllers;
using Enums;
using Game.Level.Objects;

namespace Game.States.GameStates.InGameStates
{
    public class OnMouseUpState : StateMachine
    {
        private readonly CollectableController _collectableController;

        public OnMouseUpState(IContainer container)
        {
            _collectableController = container.GetComponent<CollectableController>();
        }

        protected override void OnEnter()
        {
            _collectableController.Collect();
            SendTrigger((int)StateTriggers.IdleTrigger);
        }
    }
}