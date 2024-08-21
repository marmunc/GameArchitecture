using System.Collections;
using UnityEngine;

namespace Architecture
{
    public class Scene
    {
        private RepositoriesBase _repositoryBase;
        private InteractorsBase _interactorsBase;
        private SceneConfig _sceneConfig;

        public Scene(SceneConfig sceneConfig)
        {
            _sceneConfig = sceneConfig;
            _repositoryBase = new RepositoriesBase(_sceneConfig);
            _interactorsBase = new InteractorsBase(_sceneConfig);
        }

        public Coroutine InitializeAsync()
        {
            return Coroutines.StartRoutine(InitializeRoutine());
        }

        public IEnumerator InitializeRoutine()
        {
            _interactorsBase.CreateAllInteractors();
            _repositoryBase.CreateAllRepositories();
            yield return null;

            _interactorsBase.SendOnCreateToAllInteractors();
            _repositoryBase.SendOnCreateToAllRepositories();
            yield return null;

            _interactorsBase.InitializeAllInteractors();
            _repositoryBase.InitializeAllRepositories();
            yield return null;

            _interactorsBase.SendOnStartToAllInteractors();
            _repositoryBase.SendOnStartToAllRepositories();
            yield return null;
        }

        public T GetRepository<T>() where T : Repository
        {
            return _repositoryBase.GetRepository<T>();
        }

        public T GetInteractor<T>() where T : Interactor
        {
            return _interactorsBase.GetInteractor<T>();
        }
    }
}