using System.Collections;
using TDS.Game.Enemy;
using TDS.Infrastructure.SceneLoader;
using TDS.Infrastructure.StateMachine;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TDS.Game.Player
{
    public class PlayerDeath : MonoBehaviour
    {
        #region Variables

        [SerializeField] private PlayerHp _playerHp;
        [SerializeField] private PlayerAnimation _playerAnimation;
        [SerializeField] private PlayerMovement _playerMovement;
        [SerializeField] private PlayerAttack _playerAttack;

       private SyncSceneLoadService _sceneLoad;
        private GameState _gameState;

        #endregion


        #region Properties

        public bool IsDead { get; private set; }

        #endregion


        #region Unity lifecycle

        private void Start() =>
            _playerHp.OnChanged += OnHpChanged;


        #endregion


        IEnumerator ReloadScene()
        {
            yield return new WaitForSeconds(5f);
            GameOver();
        }


        #region Private methods

        private void OnHpChanged(int hp)
        {
            if (IsDead || hp > 0)
                return;

            IsDead = true;
            _playerAnimation.PlayerDead(IsDead);
            _playerMovement.enabled = false;
            _playerAttack.enabled = false;
            //OnHappened?.Invoke();
            StartCoroutine(ReloadScene());
        }

        private void GameOver()
        {
         //   _sceneLoad.Load(SceneManager.GetActiveScene().name, _gameState.Enter);
            IsDead = false;
            _playerAnimation.PlayerDead(IsDead);
        }

       

        #endregion
    }
}