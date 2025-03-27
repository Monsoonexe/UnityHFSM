namespace FSM
{
    public interface IState
    {
        void Init();
        void OnEnter();
        void OnExit();
        void OnExitRequest();
        void OnLogic();

        bool NeedsExitTime { get; set; }
        bool IsGhostState { get; set; }
    }

    public interface IState<TStateId> : IState
    {
        TStateId Name { get; set; }

        IStateMachine<TStateId> FSM { get; set; }
    }
}
