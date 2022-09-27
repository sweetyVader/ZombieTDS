using TDS.Game;
using TDS.Game.InputService;
using TDS.Game.Player;
using TDS.Game.UI;
using TDS.Infrastructure.SceneLoader;
using UnityEngine;

namespace TDS.Infrastructure.StateMachine
{
    public class GameState : BaseState
    {
        private ISceneLoadService _sceneLoad;
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
            HUD hud = Object.FindObjectOfType<HUD>();
            PlayerHp playerHp = Object.FindObjectOfType<PlayerHp>();
            
            RegisterInputService(playerMovement);
            InitPlayerMovement(playerMovement);
            InitHUD(hud, playerHp);
            Win();
        }

        private void InitHUD(HUD hud, PlayerHp playerHp)
        {
            Debug.LogError($"hud {playerHp}");
            hud.HpBar(playerHp);
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
        
        private void Win()
        {
            if (GameObject.FindGameObjectWithTag(Tags.Enemy) != null)
                return;
            _sceneLoad.Load("SecondGameScene", OnSceneLoaded);
            
            
        }
    }
}