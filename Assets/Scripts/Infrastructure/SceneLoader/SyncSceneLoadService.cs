using System;
using System.Collections;
using System.Collections.Generic;
using TDS.Infrastructure.Coroutine;
using UnityEngine.SceneManagement;

namespace TDS.Infrastructure.SceneLoader
{
    public class SyncSceneLoadService : ISceneLoadService
    {
        private readonly ICoroutineRunner _coroutineRunner;

        public SyncSceneLoadService(ICoroutineRunner coroutineRunner)
        {
            _coroutineRunner = coroutineRunner;
        }

        public List<string> GetAllScene()
        {
            List<string> scenes = new ();
            for(int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
            {
                scenes.Add(SceneManager.GetSceneByBuildIndex(i).name);
            }
            return scenes;

        }

        public void Load(string sceneName, Action completeCallback)
        {
            SceneManager.LoadScene(sceneName);
            _coroutineRunner.StartCoroutine(WaitFrames(1, completeCallback));
        }

        private IEnumerator WaitFrames(int count, Action completeCallback)
        {
            yield return null;
            completeCallback?.Invoke();
        }
    }
}