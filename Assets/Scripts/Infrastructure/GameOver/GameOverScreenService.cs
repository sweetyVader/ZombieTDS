using System;
using TDS.Game.Player;
using TDS.Infrastructure.LoadingScreen;
using TDS.Infrastructure.SceneLoader;
using UnityEngine;
using Object = UnityEngine.Object;

namespace TDS.Infrastructure.GameOver
{
    public class GameOverScreenService : IGameOverScreenService
    {
        private const string GameOverScreenPath = "GameOverScreen";

        private ISceneLoadService _sceneLoad;
        private Action _action;
        
        private GameObject _gameOverScreen;

        private GameOverScreen _screen;

        public event Action OnRestartGame;

        public void IsGameOver(PlayerDeath playerDeath, ISceneLoadService sceneLoad, Action completeCallback)
        {
            playerDeath.OnHappened += ShowScreen;
            _sceneLoad = sceneLoad;
            _action = completeCallback;
        }

        public void ShowScreen()
        {
            ActivateGameOverScreen();
            _gameOverScreen.SetActive(true);
            _screen.OnRestart += RestartGame;
            _screen.OnExit += ExitGame;
        }

        private void ActivateGameOverScreen()
        {
            GameObject prefab = Resources.Load<GameObject>(GameOverScreenPath);
            _gameOverScreen = Object.Instantiate(prefab);

            _screen = Object.FindObjectOfType<GameOverScreen>();
        }

        private void RestartGame()
        {
            
            Debug.Log("restart game");
           // _sceneLoad.Load("GameScene", _action);
            OnRestartGame?.Invoke();
            
            
        }

        private void ExitGame()
        {
            UnityEditor.EditorApplication.isPlaying = false;
            Application.Quit();
        }

    }
}