using TDS.Infrastructure.Coroutine;
using TDS.Infrastructure.LoadingScreen;
using TDS.Infrastructure.SceneLoader;
using UnityEngine;

namespace TDS.Infrastructure.StateMachine
{
    public class BootstrapState : BaseState
    {
        public BootstrapState(IGameStateMachine gameStateMachine) : base(gameStateMachine)
        {
        }

        public override void Enter()
        {
            RegisterAllGlobalServices();
            StateMachine.Enter<MenuState>();
            StateMachine.Enter<MenuState>();
        }

        public override void Exit()
        {
        }

        private void RegisterAllGlobalServices()
        {
            CreateCoroutineRunner();

            Services.Container.Register<ISceneLoadService>(
                new AsyncSceneLoadService(Services.Container.Get<ICoroutineRunner>()));

            Services.Container.Register<ILoadingScreenService>(new LoadingScreenService());
        }

        private void CreateCoroutineRunner()
        {
            CoroutineRunner coroutineRunner = new GameObject(nameof(CoroutineRunner)).AddComponent<CoroutineRunner>();
            Object.DontDestroyOnLoad(coroutineRunner);
            Services.Container.Register<ICoroutineRunner>(coroutineRunner);
        }
    }
}