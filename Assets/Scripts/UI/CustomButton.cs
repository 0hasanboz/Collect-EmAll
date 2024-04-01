using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace UI
{
    public class CustomButton : MonoBehaviour, IPointerClickHandler
    {
        public UnityEvent onClick;
        
        public void OnPointerClick(PointerEventData eventData)
        {
            onClick.Invoke();
        }
    }
}