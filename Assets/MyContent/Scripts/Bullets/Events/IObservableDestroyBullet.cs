using System;
using Bullets;

public interface IObservableBullet 
{
    void SubscribeDestroyBullet(Action<BaseBullet> observer);
    void UnSubscribeDestroyBullet(Action<BaseBullet> observer);
}
