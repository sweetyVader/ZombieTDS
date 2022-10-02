using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace TDS.Infrastructure.PauseScreen
{
    public class PauseScreenService : IPauseScreenService
    {
        private const string PauseScreenPath = "PauseScreen";
        
        private GameObject _pauseScreen;
        
        
        public event Action<bool> OnPause;
        
        public void ShowScreen()
        {
            if (_pauseScreen == null)
                PauseScreen();

            _pauseScreen.SetActive(true);
        }

        public void HideScreen() =>
            _pauseScreen.SetActive(false);

        private void PauseScreen()
        {
            GameObject prefab = Resources.Load<GameObject>(PauseScreenPath);
            _pauseScreen = Object.Instantiate(prefab);
            Object.DontDestroyOnLoad(_pauseScreen);
        }
    }
}