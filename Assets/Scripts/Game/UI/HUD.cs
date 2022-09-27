using TDS.Game.Player;
using UnityEngine;

namespace TDS.Game.UI
{
    public class HUD : MonoBehaviour
    {
        [SerializeField] private CharacterUI _characterUI;

        public void HpBar(PlayerHp playerHp)
        {
            _characterUI.Construct(playerHp);
        }
    }
}