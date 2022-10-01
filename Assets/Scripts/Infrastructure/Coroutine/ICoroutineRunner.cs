using System.Collections;

namespace TDS.Infrastructure.Coroutine
{
    public interface ICoroutineRunner : IService
    {
        UnityEngine.Coroutine StartCoroutine(IEnumerator routine);
    }
}