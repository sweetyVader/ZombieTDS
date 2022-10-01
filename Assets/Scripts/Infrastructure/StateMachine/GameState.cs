﻿using TDS.Game;
using TDS.Game.InputService;
using TDS.Game.Npc;
using TDS.Game.Player;
using TDS.Game.UI;
using TDS.Infrastructure.LoadingScreen;
using TDS.Infrastructure.SceneLoader;
using UnityEngine;

namespace TDS.Infrastructure.StateMachine
{
    public class GameState : BaseExitableState, IPayloadState<string>
    {
        private ISceneLoadService _sceneLoadService;
        private ILoadingScreenService _loadingScreenService;
        private INpcService _npcService;

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
        }

        private void Dispose()
        {_npcService.Dispose();
            
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
        }

        private void RegisterLocalServices()
        {
            PlayerMovement playerMovement = Object.FindObjectOfType<PlayerMovement>();
            HUD hud = Object.FindObjectOfType<HUD>();
            PlayerHp playerHp = Object.FindObjectOfType<PlayerHp>();
            _npcService = Services.Container.Register<INpcService>(new NpcService());

            RegisterInputService(playerMovement);
            InitPlayerMovement(playerMovement);
            InitHUD(hud, playerHp);
            Win();
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
            Services.Container.UnRegisterAndNullRef(ref _npcService);
        }

        private void Win()
        {
            if (GameObject.FindGameObjectWithTag(Tags.Enemy) != null)
                return;
            _sceneLoadService.Load("SecondGameScene", OnSceneLoaded);
        }
    }
}