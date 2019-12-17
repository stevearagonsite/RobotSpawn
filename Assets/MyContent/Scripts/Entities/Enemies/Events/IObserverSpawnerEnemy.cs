namespace Entities.Enemies.Events
{
    public interface IObserverEnemy 
    {
        void OnDestroyEnemy(BaseEnemy bulletObj);
    }
}
