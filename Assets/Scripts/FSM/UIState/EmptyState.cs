namespace FSM.UIState
{
    public class EmptyState : IState
    {
        public string String()
        {
            return "";
        }

        public void Tick()
        {
        }

        public void OnEnter()
        {
        }

        public void OnExit()
        {
        }
    }
}