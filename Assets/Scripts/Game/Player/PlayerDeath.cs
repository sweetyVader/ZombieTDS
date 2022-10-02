using System;
using TDS.Infrastructure.StateMachine;
using UnityEngine;

namespace TDS.Game.Player
{
    public class PlayerDeath : MonoBehaviour
    {
        #region Variables

        [SerializeField] private PlayerHp _playerHp;
        [SerializeField] private PlayerAnimation _playerAnimation;
        [SerializeField] private PlayerMovement _playerMovement;
        [SerializeField] private PlayerAttack _playerAttack;
        
        private GameState _gameState;

        #endregion


        public event Action OnHappened;


        #region Properties

        public bool IsDead { get; private set; }

        #endregion


        #region Unity lifecycle

        private void Start() =>
            _playerHp.OnChanged += OnHpChanged;

        #endregion
        //
        //
        // IEnumerator ReloadScene()
        // {
        //     yield return new WaitForSeconds(5f);
        //     GameOver();
        // }


        #region Private methods

        private void OnHpChanged(int hp)
        {
            if (IsDead || hp > 0)
                return;

            IsDead = true;
            _playerAnimation.PlayerDead(IsDead);
            _playerMovement.enabled = false;
            _playerAttack.enabled = false;
            OnHappened?.Invoke();
           
        }

        // private void GameOver()
        // {
        //     //   _sceneLoad.Load(SceneManager.GetActiveScene().name, _gameState.Enter);
        //     IsDead = false;
        //     _playerAnimation.PlayerDead(IsDead);
        // }

        #endregion
    }
}