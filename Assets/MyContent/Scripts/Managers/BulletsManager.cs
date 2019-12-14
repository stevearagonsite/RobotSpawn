using Bullets;
using UnityEngine;

namespace Managers
{
    public class BulletsManager : MonoBehaviour
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
            return _basicBulletPool.GetObjectFromPool();;
        }

        private BaseBullet BulletFactory()
        {
            return Instantiate<BaseBullet>(_basicBulletPrefab);
        }

        public void ReturnBulletToPool(BaseBullet bullet)
        {
            _basicBulletPool.DisablePoolObject(bullet);
        }
    }
}
