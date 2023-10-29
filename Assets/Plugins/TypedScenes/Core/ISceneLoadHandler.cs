namespace Necrotor.TypedScenes
{
    public interface ISceneLoadHandler<T>
    {
        void OnSceneLoaded(T argument);
    }
}
