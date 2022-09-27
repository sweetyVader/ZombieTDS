using System;

namespace TDS.Infrastructure.SceneLoader
{
    public interface ISceneLoadService : IService
    {
        void Load(string sceneName, Action completeCallback);
    }
}