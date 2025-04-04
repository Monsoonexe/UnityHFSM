using System;

namespace FSM
{
	/// <summary>
	/// A class that allows you to run additional functions (companion code)
	/// before and after the wrapped state's code.
	/// It does not interfere with the wrapped state's timing / needsExitTime / ... behaviour.
	/// </summary>
	public class StateWrapper<TStateId, TEvent>
	{
		public class WrappedState : StateBase<TStateId>, ITriggerable<TEvent>, IActionable<TEvent>
		{
			private Action<IState<TStateId>>
				beforeOnEnter,
				afterOnEnter,

				beforeOnLogic,
				afterOnLogic,

				beforeOnExit,
				afterOnExit;

			private IState<TStateId> state;

			public WrappedState(
					IState<TStateId> state,

					Action<IState<TStateId>> beforeOnEnter = null,
					Action<IState<TStateId>> afterOnEnter = null,

					Action<IState<TStateId>> beforeOnLogic = null,
					Action<IState<TStateId>> afterOnLogic = null,

					Action<IState<TStateId>> beforeOnExit = null,
					Action<IState<TStateId>> afterOnExit = null) : base(state.NeedsExitTime, state.IsGhostState)
			{
				this.state = state;

				this.beforeOnEnter = beforeOnEnter;
				this.afterOnEnter = afterOnEnter;

				this.beforeOnLogic = beforeOnLogic;
				this.afterOnLogic = afterOnLogic;

				this.beforeOnExit = beforeOnExit;
				this.afterOnExit = afterOnExit;
			}

			public override void Init()
			{
				state.Name = Name;
				state.FSM = FSM;

				state.Init();
			}

			public override void OnEnter()
			{
				beforeOnEnter?.Invoke(this);
				state.OnEnter();
				afterOnEnter?.Invoke(this);
			}

			public override void OnLogic()
			{
				beforeOnLogic?.Invoke(this);
				state.OnLogic();
				afterOnLogic?.Invoke(this);
			}

			public override void OnExit()
			{
				beforeOnExit?.Invoke(this);
				state.OnExit();
				afterOnExit?.Invoke(this);
			}

			public override void OnExitRequest()
			{
				state.OnExitRequest();
			}

			public void Trigger(TEvent trigger)
			{
				(state as ITriggerable<TEvent>)?.Trigger(trigger);
			}

			public void OnAction(TEvent trigger) {
				(state as IActionable<TEvent>)?.OnAction(trigger);
			}

			public void OnAction<TData>(TEvent trigger, TData data) {
				(state as IActionable<TEvent>)?.OnAction<TData>(trigger, data);
			}
		}

		private Action<IState<TStateId>>
			beforeOnEnter,
			afterOnEnter,

			beforeOnLogic,
			afterOnLogic,

			beforeOnExit,
			afterOnExit;

		/// <summary>
		/// Initialises a new instance of the StateWrapper class
		/// </summary>
		public StateWrapper(
				Action<IState<TStateId>> beforeOnEnter = null,
				Action<IState<TStateId>> afterOnEnter = null,

				Action<IState<TStateId>> beforeOnLogic = null,
				Action<IState<TStateId>> afterOnLogic = null,

				Action<IState<TStateId>> beforeOnExit = null,
				Action<IState<TStateId>> afterOnExit = null)
		{
			this.beforeOnEnter = beforeOnEnter;
			this.afterOnEnter = afterOnEnter;

			this.beforeOnLogic = beforeOnLogic;
			this.afterOnLogic = afterOnLogic;

			this.beforeOnExit = beforeOnExit;
			this.afterOnExit = afterOnExit;
		}

		public WrappedState Wrap(IState<TStateId> state)
		{
			return new WrappedState(
				state,
				beforeOnEnter,
				afterOnEnter,
				beforeOnLogic,
				afterOnLogic,
				beforeOnExit,
				afterOnExit
			);
		}
	}

	public class StateWrapper : StateWrapper<string, string>
	{
		public StateWrapper(
			Action<IState<string>> beforeOnEnter = null,
			Action<IState<string>> afterOnEnter = null,

			Action<IState<string>> beforeOnLogic = null,
			Action<IState<string>> afterOnLogic = null,

			Action<IState<string>> beforeOnExit = null,
			Action<IState<string>> afterOnExit = null) : base(
			beforeOnEnter, afterOnEnter,
			beforeOnLogic, afterOnLogic,
			beforeOnExit, afterOnExit)
		{
		}
	}
}
