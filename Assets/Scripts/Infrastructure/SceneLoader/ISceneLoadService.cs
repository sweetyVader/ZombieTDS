using System;
using System.Collections.Generic;

namespace TDS.Infrastructure.SceneLoader
{
    public interface ISceneLoadService : IService
    {
        List<string> GetAllScene();
        void Load(string sceneName, Action completeCallback);
    }
}