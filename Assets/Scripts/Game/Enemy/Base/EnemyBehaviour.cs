using UnityEngine;

namespace TDS.Game.Enemy.Base
{
    public abstract class EnemyBehaviour : MonoBehaviour
    {
        public bool IsActive { get; private set; }

        private void Update()
        {
            if (IsActive)
                OnUpdate();
        }

        public virtual void Activate() { }

        public virtual void Deactivate() { }
        protected virtual void OnUpdate() { }
        protected virtual void OnActiveUpdate() { }
    }
}