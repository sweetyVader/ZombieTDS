using Lean.Pool;
using UnityEngine;

namespace TDS.Game.PickUp
{
    public abstract class PickUpBase : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D col)
        {
            if (!col.gameObject.CompareTag(Tags.Player))
                return;
            LeanPool.Despawn(gameObject);
            ApplyEffect(col);
        }
        protected abstract void ApplyEffect(Collision2D col);
    }
}