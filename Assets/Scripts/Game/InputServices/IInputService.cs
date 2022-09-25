using TDS.Infrastructure;
using UnityEngine;

namespace TDS.Game.InputServices
{
    public interface IInputService : IService
    {
        Vector2 Axes { get; }
        Vector3 LookDirection { get; }
    }
}