using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Level
{
    public class GameArea : MonoBehaviour
    {
        public List<Vector3> points = new();

        public List<Vector3> GenerateRandomPoints(int count)
        {
            points.Clear();
            for (int i = 0; i < count; i++)
            {
                points.Add(GenerateRandomPointInPrism());
            }

            return points;
        }

        private Vector3 GenerateRandomPointInPrism()
        {
            var center = transform.position;
            var size = transform.localScale;
            float minX = center.x - size.x / 2f;
            float maxX = center.x + size.x / 2f;
            float minY = center.y - size.y / 2f;
            float maxY = center.y + size.y / 2f;
            float minZ = center.z - size.z / 2f;
            float maxZ = center.z + size.z / 2f;

            float randomX = Random.Range(minX, maxX);
            float randomY = Random.Range(minY, maxY);
            float randomZ = Random.Range(minZ, maxZ);

            return new Vector3(randomX, randomY, randomZ);
        }

        private void OnDrawGizmos()
        {
            var gizmoColor = Color.red;
            gizmoColor.a = 0.5f;
            Gizmos.color = gizmoColor;
            Gizmos.DrawCube(transform.position, transform.localScale);
            Gizmos.matrix = transform.localToWorldMatrix;
        }
    }
}