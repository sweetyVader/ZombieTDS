using System;
using TDS.Game.Enemy;
using TDS.Game.Player;
using UnityEngine;

namespace TDS.Game.Objects
{
    public class Explosive : MonoBehaviour
    {
        [SerializeField] private int _damage = 15;
        

        private void Explode()
        {
            Collider2D[] coliders = Physics2D.OverlapCircleAll(Vector3.zero, 10);

            foreach (Collider2D col in coliders)
            {
                IHealth health = col.GetComponentInParent<IHealth>();
                if (health != null)
                {
                    health.ApplyDamage(_damage);
                    
                }
               
            }
        }
    }
}