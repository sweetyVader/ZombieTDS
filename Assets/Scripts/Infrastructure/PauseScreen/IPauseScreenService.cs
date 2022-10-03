using System;

namespace TDS.Infrastructure.PauseScreen
{
    public interface IPauseScreenService : IService
    {
        public event Action OnRestartGame;
        void InitPauseScreen();
    }
}