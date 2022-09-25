using TDS.Infrastructure.SceneLoader;

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
            ISceneLoadService sceneLoadService = Services.Container.Get<ISceneLoadService>();
            sceneLoadService.Load("MenuScene", OnSceneLoaded);

            StateMachine.Enter<MenuState>();
        }

        private void OnSceneLoaded()
        {
            StateMachine.Enter<MenuState>();
        }

        public override void Exit()
        {
        }

        private void RegisterAllGlobalServices()
        {
            Services.Container.Register<ISceneLoadService>(new SyncSceneLoadService());
        }
    }
}