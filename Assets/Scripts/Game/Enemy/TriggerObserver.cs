using System;
using UnityEngine;

namespace TDS.Game.Enemy
{
    public class TriggerObserver : MonoBehaviour
    {
        public event Action<Collider2D> OnEntered; 
        public event Action<Collider2D> OnExited; 

        private void OnTriggerEnter2D(Collider2D col) =>
            OnEntered?.Invoke(col);

        private void OnTriggerExit2D(Collider2D other) =>
            OnExited?.Invoke(other);
    }
}