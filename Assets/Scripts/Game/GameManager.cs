using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TDS.Game
{
    public class GameManager : MonoBehaviour
    {
        private void Update()
        {
            Win();
        }

        private void Win()
        {
            if (GameObject.FindGameObjectWithTag(Tags.Enemy) != null)
                return;
            
            SceneLoader.Instance.LoadNextScene();
            
        }
    }
}