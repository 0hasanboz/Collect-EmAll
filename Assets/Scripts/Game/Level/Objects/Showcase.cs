using System.Collections.Generic;
using UnityEngine;

namespace Game.Level.Objects
{
    public class Showcase : MonoBehaviour
    {
        [SerializeField] private Transform[] waitingPoints;

        public void UpdateShowcase(List<Collectable> collectedObjects)
        {
            for (var i = 0; i < collectedObjects.Count; i++)
            {
                PlaceObject(i, collectedObjects[i]);
            }
        }

        private void PlaceObject(int index, Collectable collectedObject)
        {
            var waitingPoint = waitingPoints[index];
            var collectableTransform = collectedObject.transform;
            collectableTransform.localScale = Vector3.one;
            collectableTransform.position = waitingPoint.position + collectedObject.waitingOffset;
            collectableTransform.rotation = waitingPoint.rotation;
        }

        public int GetWaitingPointsCount()
        {
            return waitingPoints.Length;
        }

        public void Reset()
        {
        }
    }
}