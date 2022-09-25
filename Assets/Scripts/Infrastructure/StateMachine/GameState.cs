using TDS.Game.InputServices;
using TDS.Game.Player;
using TDS.Infrastructure.SceneLoader;
using UnityEngine;

namespace TDS.Infrastructure.StateMachine
{
    public class GameState : BaseState
    {
        public GameState(IGameStateMachine gameStateMachine) : base(gameStateMachine)
        {
        }

        public override void Enter()
        {
            ISceneLoadService sceneLoadService = Services.Container.Get<ISceneLoadService>();
            sceneLoadService.Load("GameScene", OnSceneLoaded);
        }

        private void OnSceneLoaded()
        {
            RegisterLocalServices();
        }

        private void RegisterLocalServices()
        {
            PlayerMovement playerMovement = Object.FindObjectOfType<PlayerMovement>();
            RegisterInputService(playerMovement);
            InitPlayerMovement(playerMovement);
        }

        private void RegisterInputService(PlayerMovement playerMovement)
        {
            Services.Container.Register<IInputService>(
                new StandaloneInputService(Camera.main, playerMovement.transform));
        }

        private void InitPlayerMovement(PlayerMovement playerMovement)
        {
            playerMovement.Construct(Services.Container.Get<IInputService>());
        }

        public override void Exit()
        {
            UnregisterLocalServices();
        }

        private void UnregisterLocalServices()
        {
            Services.Container.UnRegister<IInputService>();
        }
    }
}