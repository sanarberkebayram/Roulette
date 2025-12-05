namespace Runtime.Game
{
    public interface IGameState
    {
        void OnEnter();
        void OnExit();
        void OnUpdate();
    }
}