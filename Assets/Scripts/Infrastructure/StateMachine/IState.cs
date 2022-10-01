namespace TDS.Infrastructure.StateMachine
{
    public interface IState : IExitableState
    {
        void Enter();
    }
}