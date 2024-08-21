using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Architecture
{
    public abstract class SceneManagerBase
    {
        public event Action<Scene> OnSceneLoadedEvent;

        public Scene scene { get; private set; }
        public bool IsLoading { get; private set; }

        protected Dictionary<string, SceneConfig> _sceneConfigMap;

        public SceneManagerBase()
        {
            _sceneConfigMap = new Dictionary<string, SceneConfig>();
        }

        public abstract void InitSceneMap();

        public Coroutine LoadCurrentSceneAsync()
        {
            if (IsLoading)
                throw new Exception("Scene is loading now");

            var sceneName = SceneManager.GetActiveScene().name;
            var config = _sceneConfigMap[sceneName];
            return Coroutines.StartRoutine(LoadCurrentSceneRoutine(config));
        }

        private IEnumerator LoadCurrentSceneRoutine(SceneConfig sceneConfig)
        {
            IsLoading = true;

            yield return Coroutines.StartRoutine(InitializeSceneRoutine(sceneConfig));

            IsLoading = false;
            OnSceneLoadedEvent?.Invoke(scene);
        }

        public Coroutine LoadNewSceneAsync(string sceneName)
        {
            if (IsLoading)
                throw new Exception("Scene is loading now");

            var config = _sceneConfigMap[sceneName];
            return Coroutines.StartRoutine(LoadNewSceneRoutine(config));
        }

        private IEnumerator LoadNewSceneRoutine(SceneConfig sceneConfig)
        {
            IsLoading = true;

            yield return Coroutines.StartRoutine(LoadSceneAsync(sceneConfig));
            yield return Coroutines.StartRoutine(InitializeSceneRoutine(sceneConfig));

            IsLoading = false;
            OnSceneLoadedEvent?.Invoke(scene);
        }

        private IEnumerator LoadSceneAsync(SceneConfig sceneConfig)
        {
            var async = SceneManager.LoadSceneAsync(sceneConfig.sceneName);
            async.allowSceneActivation = false;

            while (async.progress < 0.9f)
                yield return null;

            async.allowSceneActivation = true;

        }

        private IEnumerator InitializeSceneRoutine(SceneConfig sceneConfig)
        {
            scene = new Scene(sceneConfig);
            yield return scene.InitializeAsync();
        }


        public T GetRepository<T>() where T : Repository
        {
            return scene.GetRepository<T>();
        }
        public T GetInteractor<T>() where T : Interactor
        {
            return scene.GetInteractor<T>();
        }
    }
}