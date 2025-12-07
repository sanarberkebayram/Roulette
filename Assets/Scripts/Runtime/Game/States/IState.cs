namespace Runtime.Game
{
    public interface IState
    {
        StateType StateType { get; }
        void OnEnter();
        void OnExit();
    }
}