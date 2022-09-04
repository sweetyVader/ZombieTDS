using TDS.Game.Player;
using UnityEngine;

namespace TDS.Game.Objects
{
    public class Medkit : MonoBehaviour
    {
        [SerializeField] private int _heal = 50;
        
        private PlayerHp _playerHp;

        private void Start()
        {
            _playerHp = FindObjectOfType<PlayerHp>();
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.CompareTag(Tags.Player))
            {
                Destroy(gameObject);
                _playerHp.ApplyHeal(_heal);
            }
                
        }
    }
}