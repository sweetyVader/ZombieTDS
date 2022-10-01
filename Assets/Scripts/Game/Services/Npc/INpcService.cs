using System;
using TDS.Infrastructure;

namespace TDS.Game.Npc
{
    public interface INpcService : IService
    {
        event Action OnAllDead;
        void Init();
        void Dispose();
    }
}