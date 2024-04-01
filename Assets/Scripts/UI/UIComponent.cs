using Base;
using UnityEngine;

namespace UI
{
    public class UIComponent : MonoBehaviour
    {
        [SerializeField] private GameView[] _canvases;

        public T GetCanvas<T>() where T : GameView
        {
            foreach (var canvas in _canvases)
            {
                if (canvas is T)
                {
                    return canvas as T;
                }
            }

            Debug.LogError($"Canvas of type {typeof(T)} not found");
            return null;
        }
    }
}