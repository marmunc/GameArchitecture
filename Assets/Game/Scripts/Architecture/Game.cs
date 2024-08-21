using System;
using System.Collections;

namespace Architecture
{
    public class Game
    {
        public static event Action OnGameInitializedEvent;

        public static SceneManagerBase SceneManagerB { get; private set; }

        public static void Run()
        {
            SceneManagerB = new SceneManagerExample();
            Coroutines.StartRoutine(InitializeGameRoutine());
        }

        private static IEnumerator InitializeGameRoutine()
        {
            SceneManagerB.InitSceneMap();
            yield return SceneManagerB.LoadCurrentSceneAsync();
            OnGameInitializedEvent?.Invoke();
        }

        public static T GetInteractor<T>() where T : Interactor
        {
            return SceneManagerB.GetInteractor<T>();
        }

        public static T GetRepository<T>() where T : Repository
        {
            return SceneManagerB.GetRepository<T>();
        }
    }
}