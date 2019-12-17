using System;

namespace Entities.Enemies.Events
{
    public interface IObservableSpawnerEnemy
    {
        void SubscribeDestroySpawnerEnemy(Action<BaseEnemy> observer);
        void UnSubscribeDestroySpawnerEnemy(Action<BaseEnemy> observer);
    }
}
