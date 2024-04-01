using System;
using System.Collections.Generic;
using Base;

namespace Core
{
    public class ComponentContainer : IContainer
    {
        private Dictionary<string, object> _components;

        private List<IInitializable> _initializables = new();
        private List<IStartable> _startables = new();
        private List<IUpdatable> _updatables = new();
        private List<IDisposable> _disposables = new();

        public ComponentContainer()
        {
            _components = new Dictionary<string, object>();
        }

        public void AddComponent<T>(T component, string id = null) where T : class
        {
            var key = !string.IsNullOrEmpty(id) ? $"{typeof(T).Name}_{id}" : typeof(T).Name;

            if (_components.ContainsKey(key))
            {
                Logger.LogError("Component already exists: " + id);
                return;
            }

            _components.Add(key, component);
            if (component is IInitializable initializable)
            {
                _initializables.Add(initializable);
            }

            if (component is IStartable startable)
            {
                _startables.Add(startable);
            }

            if (component is IUpdatable updatable)
            {
                _updatables.Add(updatable);
            }

            if (component is IDisposable disposable)
            {
                _disposables.Add(disposable);
            }
        }

        public void Resolve<T>(T component, string id = null) where T : class
        {
            var key = string.IsNullOrEmpty(id) ? typeof(T).Name : $"{typeof(T).Name}_{id}";
            if (_components.ContainsKey(key))
            {
                return;
            }

            AddComponent(component);
        }

        public T GetComponent<T>(string id = null) where T : class
        {
            var key = !string.IsNullOrEmpty(id) ? $"{typeof(T).Name}_{id}" : typeof(T).Name;

            if (!_components.ContainsKey(key))
            {
                return null;
            }

            return _components[key] as T;
        }

        public void InitializeComponents()
        {
            foreach (var initializable in _initializables)
            {
                initializable.Initialize(this);
            }
        }

        public void StartComponents()
        {
            foreach (var startable in _startables)
            {
                startable.Start();
            }
        }

        public void Update()
        {
            foreach (var updatable in _updatables)
            {
                updatable.Update();
            }
        }

        public void Dispose()
        {
            foreach (var disposable in _disposables)
            {
                disposable.Dispose();
            }

            _components.Clear();
            _initializables.Clear();
            _startables.Clear();
            _updatables.Clear();
            _disposables.Clear();
        }
    }
}