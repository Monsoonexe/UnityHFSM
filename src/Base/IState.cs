using UnityHFSM.Inspection;

namespace UnityHFSM
{
    public interface IState
    {
        void Init();
        void OnEnter();
        void OnExit();
        void OnExitRequest();
        void OnLogic();
        string GetActiveHierarchyPath();

        void AcceptVisitor(IStateVisitor visitor);

        bool needsExitTime { get; }
        bool isGhostState { get; }
        IStateTimingManager fsm { get; set; }
    }

    public interface IState<TStateId> : IState
    {
        TStateId name { get; set; }
    }
}