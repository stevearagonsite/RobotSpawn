using System;
using Entities.Enemies;
using Entities.Enemies.Events;
using UnityEngine;

namespace Managers
{
    public class EnemiesManager : MonoBehaviour, IObserverEnemy 
    {
        public GameObject shooterEnemyPrefab;
        private Pool<BaseEnemy> _shooterEnemyPool;
        private SpawnerEnemy spawnerEnemy;
        private static EnemiesManager _instance;
        public static EnemiesManager Instance => _instance;
        
        private void Awake()
        {
            if (_instance != null)
            {
                Destroy(this);
                _instance = this;
            }
            else
            {
                _instance = this;
            }
            _shooterEnemyPool = new Pool<BaseEnemy>(8, EnemyFactory, BaseEnemy.InitializeEnemy, BaseEnemy.DisposeEnemy, true);
        }


        private void Start()
        {
            spawnerEnemy = FindObjectOfType<SpawnerEnemy>();
        }

        public BaseEnemy GetShooterEnemy()
        {
            return _shooterEnemyPool.GetObjectFromPool();
        }

        private BaseEnemy EnemyFactory()
        {
            var enemyObj = Instantiate(shooterEnemyPrefab).GetComponent<BaseEnemy>();
            var eventsEnemy = (IObservableEnemy)enemyObj;
            eventsEnemy.SubscribeDestroyEnemy(OnDestroyEnemy);
            
            return enemyObj;
        }

        private void ReturnEnemyToPool(BaseEnemy enemy)
        {
            _shooterEnemyPool.DisablePoolObject(enemy);
        }
        
        #region IObserverEnemy
        public void OnDestroyEnemy(BaseEnemy bulletObj)
        {
            ReturnEnemyToPool(bulletObj);
        }
        #endregion IObserverEnemy
    }
}

