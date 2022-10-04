using TDS.Infrastructure;
using TDS.Infrastructure.StateMachine;
using UnityEngine;
using UnityEngine.UI;

namespace TDS.Menu.UI
{
    public class WinScreen : MonoBehaviour
    {
        [SerializeField] private Button _menuButton;
        [SerializeField] private Button _exitButton;

        private IGameStateMachine _stateMachine;

        private void Awake()
        {
            _menuButton.onClick.AddListener(OnMenuButtonClick);
            _exitButton.onClick.AddListener(OnExitButtonClick);
        }

        private void Start() =>
            _stateMachine = Services.Container.Get<IGameStateMachine>();

        private void OnMenuButtonClick() =>
            _stateMachine.Enter<GameState, string>("MenuScene");

        private void OnExitButtonClick()
        {
            UnityEditor.EditorApplication.isPlaying = false;
            Application.Quit();
        }
    }
}