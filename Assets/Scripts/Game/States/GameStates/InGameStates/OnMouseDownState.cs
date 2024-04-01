using Base;
using Controllers;
using Enums;
using Game.Level.Objects;
using UnityEngine;

namespace Game.States.GameStates.InGameStates
{
    public class OnMouseDownState : StateMachine
    {
        private const float MaxRaycastDistance = 20f;

        private readonly CollectableController _collectableController;

        private RaycastHit _hitInfo;

        private Camera _mainCamera;

        public OnMouseDownState(IContainer container)
        {
            _collectableController = container.GetComponent<CollectableController>();
        }

        protected override void OnEnter()
        {
            _mainCamera = Camera.main;
        }

        protected override void OnUpdate()
        {
            if (Input.GetMouseButtonUp(0))
            {
                SendTrigger((int)StateTriggers.OnMouseUpTrigger);
                return;
            }

            if (_mainCamera == null) return;

            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out _hitInfo, MaxRaycastDistance))
            {
                Collectable collectable = _hitInfo.collider.GetComponent<Collectable>();
                if (collectable == null)
                {
                    _collectableController.Deselect();
                    return;
                }

                _collectableController.Select(collectable);
                SendTrigger((int)StateTriggers.OnMouseDownTrigger);
            }
        }
    }
}