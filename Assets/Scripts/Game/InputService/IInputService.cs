using TDS.Infrastructure;
using UnityEngine;

namespace TDS.Game.InputService
{
    public interface IInputService : IService
    {
        Vector2 Axes { get; }
        Vector3 LookDirection { get; }
    }
}