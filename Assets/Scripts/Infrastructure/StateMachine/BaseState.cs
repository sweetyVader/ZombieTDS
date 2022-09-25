namespace TDS.Infrastructure.StateMachine
{
    public abstract class BaseState : IState
    {
        protected IGameStateMachine StateMachine { get; }

        protected BaseState(IGameStateMachine gameStateMachine)
        {
            StateMachine = gameStateMachine;
        }
        public abstract void Enter();

        public abstract void Exit();
    }
}