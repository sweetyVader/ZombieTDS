using System;
using UnityEngine;

namespace TDS.Infrastructure.PauseScreen
{
    public class PauseScreenService : MonoBehaviour, IPauseScreenService
    {
        public event Action OnRestartGame;
        private const string PauseScreenPath = "PauseScreen";

        private GameObject _pauseScreen;
        private bool _isPause = false;
        private PauseScreen _screen;

        private void Update()
        {
            if (!Input.GetKeyDown(KeyCode.Escape))
                return;
            TogglePause();
        }

        public void InitPauseScreen()
        {
            if (_pauseScreen == null)
                PauseScreen();
            _pauseScreen.SetActive(false);
            _screen.OnContinue += TogglePause;
            _screen.OnRestart += RestartGame;
            _screen.OnExit += ExitGame;
        }

        private void TogglePause()
        {
            _isPause = !_isPause;
            _pauseScreen.SetActive(_isPause);
            Time.timeScale = _isPause ? 0 : 1;
        }

        private void PauseScreen()
        {
            GameObject prefab = Resources.Load<GameObject>(PauseScreenPath);
            _pauseScreen = Instantiate(prefab);
            _screen = FindObjectOfType<PauseScreen>();
        }

        private void RestartGame() =>
            OnRestartGame?.Invoke();

        private void ExitGame()
        {
            UnityEditor.EditorApplication.isPlaying = false;
            Application.Quit();
        }
    }
}