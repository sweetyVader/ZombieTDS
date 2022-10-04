using TDS.Game.InputService;
using TDS.Game.Npc;
using TDS.Game.Player;
using TDS.Game.UI;
using TDS.Infrastructure.GameOver;
using TDS.Infrastructure.LoadingScreen;
using TDS.Infrastructure.PauseScreen;
using TDS.Infrastructure.SceneLoader;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TDS.Infrastructure.StateMachine
{
    public class GameState : BaseExitableState, IPayloadState<string>
    {
        private ISceneLoadService _sceneLoadService;
        private ILoadingScreenService _loadingScreenService;
        private INpcService _npcService;
        private IPauseScreenService _pauseScreenService;
        private IGameOverScreenService _gameOverScreenService;

        public GameState(IGameStateMachine gameStateMachine) : base(gameStateMachine)
        {
        }

        public void Enter(string sceneName)
        {
            Services.Container.Get(out _sceneLoadService);
            Services.Container.Get(out _loadingScreenService);

            _loadingScreenService.ShowScreen();
            _sceneLoadService.Load(sceneName, OnSceneLoaded);
        }

        public override void Exit()
        {
            Dispose();
            UnregisterLocalServices();

            _loadingScreenService = null;
            _sceneLoadService = null;
            _pauseScreenService = null;
        }

        private void Dispose()
        {
            if (_npcService != null)
                _npcService.Dispose();
        }

        private void OnSceneLoaded()
        {
            RegisterLocalServices();

            Initialize();
            _loadingScreenService.HideScreen();
        }

        private void Initialize()
        {
            _npcService.Init();
            _npcService.OnAllDead += LoadNextLevel;
        }

        private void RegisterLocalServices()
        {
            PlayerMovement playerMovement = Object.FindObjectOfType<PlayerMovement>();
            HUD hud = Object.FindObjectOfType<HUD>();
            PlayerHp playerHp = Object.FindObjectOfType<PlayerHp>();
            PlayerDeath playerDeath = Object.FindObjectOfType<PlayerDeath>();

            _npcService = Services.Container.Register<INpcService>(new NpcService());
            _gameOverScreenService = Services.Container.Register<IGameOverScreenService>(
                new GameOverScreenService());

            CreatePauseRunner();
            RegisterInputService(playerMovement);
            InitPlayerMovement(playerMovement);
            InitHUD(hud, playerHp);
            GameOver(playerDeath);
        }

        private void CreatePauseRunner()
        {
            _pauseScreenService = new GameObject(nameof(PauseScreenService)).AddComponent<PauseScreenService>();
            Services.Container.Register(_pauseScreenService);
            InitPause();
        }

        private void InitPause()
        {
            _pauseScreenService.InitPauseScreen();
            _pauseScreenService.OnRestartGame += RestartGame;
        }

        private void GameOver(PlayerDeath playerDeath)
        {
            _gameOverScreenService.IsGameOver(playerDeath, _sceneLoadService, OnSceneLoaded);
            _gameOverScreenService.OnRestartGame += RestartGame;
        }

        private void RestartGame()
        {
            _gameOverScreenService.OnRestartGame -= RestartGame;
            Exit();
            Enter("GameScene");
        }

        private void InitHUD(HUD hud, PlayerHp playerHp) =>
            hud.InitHpBar(playerHp);

        private void RegisterInputService(PlayerMovement playerMovement) =>
            Services.Container.Register<IInputService>(
                new StandaloneInputService(Camera.main, playerMovement.transform));

        private void InitPlayerMovement(PlayerMovement playerMovement) =>
            playerMovement.Construct(Services.Container.Get<IInputService>());

        private void UnregisterLocalServices()
        {
            Services.Container.UnRegister<IInputService>();
            Object.Destroy(_pauseScreenService as Object);
            Services.Container.UnRegisterAndNullRef(ref _npcService);
            Services.Container.UnRegisterAndNullRef(ref _pauseScreenService);
            Services.Container.UnRegisterAndNullRef(ref _gameOverScreenService);
        }

        private void LoadNextLevel()
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            Debug.Log($"currentSceneIndex {currentSceneIndex}");
            string nextScene = SceneContainer.Scenes[currentSceneIndex + 1];
            Debug.Log($"nextScene is {nextScene}");
            _sceneLoadService.Load(nextScene, OnSceneLoaded);
            Exit();
        }
    }
}