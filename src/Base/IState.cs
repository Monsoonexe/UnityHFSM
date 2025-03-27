namespace FSM
{
    public interface IState<TStateId>
    {
        void Init();
        void OnEnter();
        void OnExit();
        void OnExitRequest();
        void OnLogic();

        bool needsExitTime { get; set; }
        bool isGhostState { get; set; }
        TStateId name { get; set; }

        IStateMachine<TStateId> fsm { get; set; }
    }
}