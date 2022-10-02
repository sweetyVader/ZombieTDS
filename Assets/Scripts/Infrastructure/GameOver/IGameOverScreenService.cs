using System;
using TDS.Game.Player;
using TDS.Infrastructure.SceneLoader;

namespace TDS.Infrastructure.GameOver
{
    public interface IGameOverScreenService : IService
    {
        public event Action OnRestartGame;
        void IsGameOver(PlayerDeath playerDeath, ISceneLoadService sceneLoad, Action completeCallback);
        void ShowScreen();
       
    }
}