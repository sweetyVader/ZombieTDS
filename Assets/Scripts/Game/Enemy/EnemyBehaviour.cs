using System;
using UnityEngine;

namespace TDS.Game.Enemy
{
    public abstract class EnemyBehaviour : MonoBehaviour
    {
        private bool _isActive;

        private void Update()
        {
            if (_isActive)
                OnUpdate();
        }

        public virtual void Activate() { }

        public virtual void Deactivate() { }
        protected virtual void OnUpdate() { }
    }
}