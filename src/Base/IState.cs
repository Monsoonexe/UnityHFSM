namespace FSM
{
    public interface IState<TStateId>
    {
        void Init();
        void OnEnter();
        void OnExit();
        void OnExitRequest();
        void OnLogic();

        bool NeedsExitTime { get; set; }
        bool IsGhostState { get; set; }
        TStateId Name { get; set; }

        IStateMachine<TStateId> FSM { get; set; }
    }
}