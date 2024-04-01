using UnityEngine;

namespace Game.Level
{
    [CreateAssetMenu(fileName = "ObjectData", menuName = "Level/ObjectData")]
    public class ObjectData : ScriptableObject
    {
        [SerializeField] private string _name;
        [SerializeField] private GameObject _prefab;
        [SerializeField] private Sprite _icon;

        public string Name => _name;
        public GameObject Prefab => _prefab;
        public Sprite Icon => _icon;
    }
}