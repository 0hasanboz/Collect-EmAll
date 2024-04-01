using Base;
using Enums;
using UnityEngine;

namespace Game.States.GameStates.InGameStates
{
    public class IdleState : StateMachine
    {
        public IdleState(IContainer container)
        {
        }

        protected override void OnUpdate()
        {
            if (Input.GetMouseButtonDown(0))
            {
                SendTrigger((int)StateTriggers.OnMouseDownTrigger);
            }
        }
    }
}