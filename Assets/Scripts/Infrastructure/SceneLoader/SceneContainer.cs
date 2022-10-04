using UnityEngine;

namespace TDS.Infrastructure.SceneLoader
{
    public class SceneContainer : MonoBehaviour
    {
        private const string FirstGameScene = "GameScene";
        private const string SecondGameScene = "SecondGameScene";
        private const string ThirdGameScene = "ThirdGameScene";
        private const string FourthGameScene = "FourthGameScene";
        private const string WinScene = "FourthGameScene";

        public static readonly string[] Scenes = new string[] {"BootstrapScene", "MenuScene",
            FirstGameScene, SecondGameScene, ThirdGameScene, FourthGameScene, WinScene};
    }
}