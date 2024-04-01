using System.Collections.Generic;
using System.Linq;
using Base;
using Game.Level;
using Game.Level.Objects;
using Managers;

namespace Controllers
{
    public class CollectableController : IInitializable
    {
        private LevelManager _levelManager;
        private Showcase _showcase;

        private List<Collectable> _collectedObjects = new();
        private List<Collectable> _objectsToBeMerged = new();

        private Collectable _selectedCollectable;

        public void Initialize(IContainer container)
        {
            _levelManager = container.GetComponent<LevelManager>();
            _showcase = container.GetComponent<Showcase>();
        }

        public void Select(Collectable collectable)
        {
            if (_selectedCollectable == collectable) return;

            Deselect();

            _selectedCollectable = collectable;
            _selectedCollectable.Select();
        }

        public void Deselect()
        {
            if (_selectedCollectable == null) return;
            _selectedCollectable.Deselect();
            _selectedCollectable = null;
        }

        public void Collect()
        {
            if (_selectedCollectable == null) return;

            _selectedCollectable.TurnOffPhysics();

            AddCollectableToCollected(_selectedCollectable);

            MergeSameObjects();

            _showcase.UpdateShowcase(_collectedObjects);

            _levelManager.DecreaseGoalAmount(_selectedCollectable.ObjectData);

            _selectedCollectable = null;

            if (_collectedObjects.Count == _showcase.GetWaitingPointsCount()) InvokeLevelFailed();
        }

        private void AddCollectableToCollected(Collectable collectable)
        {
            var orderIndex = _collectedObjects.Count;

            for (var i = _collectedObjects.Count - 1; i >= 0; i--)
            {
                if (_collectedObjects[i].ObjectData != collectable.ObjectData) continue;
                orderIndex = i + 1;
                break;
            }

            _collectedObjects.Insert(orderIndex, collectable);
        }

        private void MergeSameObjects()
        {
            if (!AreThereThreeSameObjectsCollected(_selectedCollectable.ObjectData)) return;

            _objectsToBeMerged.AddRange(_collectedObjects.Where(collectable =>
                collectable.ObjectData == _selectedCollectable.ObjectData));

            _collectedObjects.RemoveAll(collectable => collectable.ObjectData == _selectedCollectable.ObjectData);

            _objectsToBeMerged.ForEach(collectable => collectable.Merge());

            _objectsToBeMerged.Clear();
        }

        private bool AreThereThreeSameObjectsCollected(ObjectData objectData)
        {
            return _collectedObjects.Count(collectable => collectable.ObjectData == objectData) == 3;
        }

        private void InvokeLevelFailed()
        {
            _levelManager.InvokeLevelFailed();
        }

        public void Reset()
        {
            _collectedObjects.Clear();
            _objectsToBeMerged.Clear();
            _showcase.Reset();
            Deselect();
        }
    }
}