namespace FSM
{
    public interface IState
    {
        string String();
        void Tick();
        void OnEnter();
        void OnExit();
    }
}