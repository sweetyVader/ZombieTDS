using UnityEngine;
using UnityEngine.UI;

namespace TDS.Game.UI
{
    public class HpBar : MonoBehaviour
    {
        [SerializeField] private Image _fillImage;

        public void SetFill(float fillAmount) =>
            _fillImage.fillAmount = fillAmount;
    }
}