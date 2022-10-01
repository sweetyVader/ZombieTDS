using System;
using System.Collections.Generic;

namespace TDS.Infrastructure.StateMachine
{
    public class GameStateMachine : IGameStateMachine
    {
        private readonly Dictionary<Type, IExitableState> _states;

        private IExitableState _currentState;

        public GameStateMachine()
        {
            _states = new Dictionary<Type, IExitableState>()
            {
                {typeof(BootstrapState), new BootstrapState(this)},
                {typeof(MenuState), new MenuState(this)},
                {typeof(GameState), new GameState(this)}
            };
        }

        public void Enter<TState>() where TState : class, IState
        {
            _currentState?.Exit();
            TState newState = _states[typeof(TState)] as TState;
            newState.Enter();
            _currentState = newState;
        }

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadState<TPayload>
        {
            _currentState?.Exit();
            TState newState = _states[typeof(TState)] as TState;
            newState.Enter(payload);
            _currentState = newState;

        }
    }
}