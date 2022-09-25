using System;
using UnityEngine.SceneManagement;

namespace TDS.Infrastructure.SceneLoader
{
    public class SyncSceneLoadService : ISceneLoadService
    {
        public void Load(string sceneName, Action completeCallback)
        {
            SceneManager.LoadScene(sceneName);
            completeCallback?.Invoke();
        }
    }
}