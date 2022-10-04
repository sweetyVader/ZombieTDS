using System;
using System.Collections;
using System.Collections.Generic;
using TDS.Infrastructure.Coroutine;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TDS.Infrastructure.SceneLoader
{
    public class AsyncSceneLoadService : ISceneLoadService
    {
        private readonly ICoroutineRunner _coroutineRunner;

        public AsyncSceneLoadService(ICoroutineRunner coroutineRunner)
        {
            _coroutineRunner = coroutineRunner;
        }
        public void Load(string sceneName, Action completeCallback) =>
            _coroutineRunner.StartCoroutine(LoadInternal(sceneName, completeCallback));

        private IEnumerator LoadInternal(string sceneName, Action completeCallback)
        {
            AsyncOperation loadSceneAsync = SceneManager.LoadSceneAsync(sceneName);
            while (!loadSceneAsync.isDone)
                yield return null;

            completeCallback?.Invoke();
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
    }
}