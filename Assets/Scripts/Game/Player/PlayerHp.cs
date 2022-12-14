using System;
using TDS.Game.Objects;
using UnityEngine;

namespace TDS.Game.Player
{
    public class PlayerHp : MonoBehaviour, IHealth
    {
        #region Variables

        [SerializeField] private int _startHp;
        [SerializeField] private int _maxHp;

        #endregion


        public event Action<int> OnChanged;


        #region Properties

        public int CurrentHp { get; private set; }
        public int MaxHp => _maxHp;

        #endregion


        #region Unity lifecycle

        private void Awake()
        {
            CurrentHp = _startHp;
            OnChanged?.Invoke(CurrentHp);
        }

        #endregion


        #region Public methods

        public void ApplyDamage(int damage)
        {
            CurrentHp -= damage;
            OnChanged?.Invoke(CurrentHp);
        }

        public void ApplyHeal(int heal)
        {
            CurrentHp = Mathf.Min(_maxHp, CurrentHp + heal);
            OnChanged?.Invoke(CurrentHp);
        }

        #endregion
    }
}