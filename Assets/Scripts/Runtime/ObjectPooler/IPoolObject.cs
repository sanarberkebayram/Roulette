namespace Runtime.ObjectPooler
{
    public interface IPoolObject
    {
        void Release();
        void Get();
        void Destroy();
    }
}