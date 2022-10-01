using TDS.Infrastructure.LoadingScreen;
using TDS.Infrastructure.SceneLoader;

namespace TDS.Infrastructure.StateMachine
{
    public class MenuState : BaseState
    {
        private ILoadingScreenService _loadingScreenService;
        private ISceneLoadService _sceneLoadService;

        public MenuState(IGameStateMachine gameStateMachine) : base(gameStateMachine)
        {
        }

        public override void Enter()
        {
            Services.Container.Get(out _sceneLoadService);
            Services.Container.Get(out _loadingScreenService);

            _loadingScreenService.ShowScreen();
            _sceneLoadService.Load("MenuScene", OnSceneLoaded);
        }

        private void OnSceneLoaded()
        {
            Services.Container.Get<ILoadingScreenService>();
            _loadingScreenService.HideScreen();
        }

        public override void Exit()
        {
            _loadingScreenService = null;
            _sceneLoadService = null;
        }
    }
}