using UnityEngine;

namespace Game.Level.Objects
{
    public class Collectable : MonoBehaviour
    {
        private const float ScaleFactor = 1.3f;

        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private Collider _collider;

        public Vector3 waitingOffset;

        private ObjectData _objectData;
        public ObjectData ObjectData => _objectData;

        private void OnDisable()
        {
            ResetPhysicValue();
        }
        
        public void Deselect()
        {
            transform.localScale = Vector3.one;
        }

        public void Select()
        {
            transform.localScale = Vector3.one * ScaleFactor;
        }

        public void Merge()
        {
            gameObject.SetActive(false);
        }

        public void SetObjectData(ObjectData objectData)
        {
            _objectData = objectData;
        }

        public void TurnOffPhysics()
        {
            _rigidbody.isKinematic = true;
            _collider.enabled = false;
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;
        }

        public void TurnOnPhysics()
        {
            _rigidbody.isKinematic = false;
            _collider.enabled = true;
        }

        private void ResetPhysicValue()
        {
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;
        }
    }
}