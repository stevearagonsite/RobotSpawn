using System;

namespace Entities.Enemies.Events
{
    public interface IObservableEnemy
    {
        void SubscribeDestroyEnemy(Action<BaseEnemy> observer);
        void UnSubscribeDestroyEnemy(Action<BaseEnemy> observer);
    }
}
