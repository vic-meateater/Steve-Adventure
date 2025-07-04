namespace SteveAdventure
{
    public abstract class Transition
    {
        protected readonly BrainFSM Brain;

        protected Transition(BrainFSM brain)
        {
            Brain = brain;
        }

        /// <summary>
        /// Возвращает true, если условия для перехода выполнены.
        /// </summary>
        public abstract bool ShouldTransition();

        /// <summary>
        /// Содержит логику переключения состояния.
        /// Обычно вызывает ChangeState.
        /// </summary>
        public abstract void OnTransition();
    }

    // public class FromPatrolToAttackTransition : Transition
    // {
    //     public FromPatrolToAttackTransition(BrainFSM brainFsm) : base(brainFsm)
    //     {
    //     }
    //
    //     public override bool ShouldTransition()
    //     {
    //         return true;
    //     }
    //
    //     public override void OnTransition()
    //     {
    //         BrainFsm.ChangeState<AttackState>();
    //     }
    // }
}