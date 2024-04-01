using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class HoverButton : CustomButton, IPointerDownHandler,
        IPointerUpHandler
    {
        public float scaleFactor = 0.9f;
        private Vector3 _originalScale;
        
        void Awake()
        {
            _originalScale = transform.localScale;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            transform.localScale = _originalScale * scaleFactor;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            transform.localScale = _originalScale;
        }
    }
}