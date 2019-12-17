using Bullets;
using UnityEngine;

namespace Managers
{
    public class BulletsManager : MonoBehaviour, IObserverBullet
    {
        [SerializeField]
        private BasicBullet _basicBulletPrefab;
        
        private Pool<BaseBullet> _basicBulletPool;

        private static BulletsManager _instance;
        public static BulletsManager Instance => _instance;

        private void Awake()
        {
            _instance = this;
            _basicBulletPool = new Pool<BaseBullet>(8, BulletFactory, BaseBullet.InitializeBullet, BaseBullet.DisposeBullet, true);
        }

        public BaseBullet GetBasicBullet()
        {
            return _basicBulletPool.GetObjectFromPool();
        }

        private BaseBullet BulletFactory()
        {
            var bulletObj = Instantiate<BaseBullet>(_basicBulletPrefab);
            var eventsBullet = (IObservableBullet)bulletObj;
            eventsBullet.SubscribeDestroyBullet(OnDestroyBullet);
            
            return bulletObj;
        }

        private void ReturnBulletToPool(BaseBullet bullet)
        {
            if (BulletsManager.Instance) _basicBulletPool.DisablePoolObject(bullet);
        }

        #region IObserverBullet
        public void OnDestroyBullet(BaseBullet bulletObj)
        {
            ReturnBulletToPool(bulletObj);
        }
        #endregion IObserverBullet

    }
}
