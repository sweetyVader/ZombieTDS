// using System;
// using TDS.Infrastructure.SceneLoader;
// using UnityEngine;
// using UnityEngine.SceneManagement;
//
// namespace TDS.Game
// {
//     public class GameManager : MonoBehaviour
//     {
//         private ISceneLoadService _sceneLoad;
//         private void Update()
//         {
//             Win();
//         }
//
//         private void Win()
//         {
//             if (GameObject.FindGameObjectWithTag(Tags.Enemy) != null)
//                 return;
//             _sceneLoad.Load("SecondGameScene", onsceneloa);
//             SceneLoader.Instance.LoadNextScene();
//             
//         }
//         private void OnSceneLoaded()
//         {
//             RegisterLocalServices();
//         }
//     }
// }