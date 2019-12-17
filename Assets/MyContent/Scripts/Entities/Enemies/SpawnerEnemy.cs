using System;
using System.Collections.Generic;
using Entities.Enemies.Events;
using SO;
using UnityEngine;
using Managers;
using Vuforia;

namespace Entities.Enemies
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Collider))]
    public class SpawnerEnemy : BaseEnemy, IObservableSpawnerEnemy, IObserverEnemy
    {
        #region Parameters & attributes
        public SpawnerEnemySO scriptableObject;
        public Transform[] TransformSpawners;
        protected event Action<BaseEnemy> OnDestroySpawnerEnemy = delegate { };
        public float SpawnRate { get; private set; }
        public float CurrentTimeToSpawn { get; private set; }
        public int MaxFleet { get; set;}
        public int FleetCount { get; set;}
        #endregion Parameters & attributes

        #region MonoBehavior
        protected void Awake()
        {
            #if UNITY_EDITOR
            if (!scriptableObject) Debug.LogError($"The {transform.name} need the scriptable object.");
            for (int i = 0; i < TransformSpawners.Length; i++)
            {
                var transformSpawner = TransformSpawners[i];
                if (!transformSpawner) Debug.LogError($"The Cannon {i} hasn't reference.");
            }
            #endif
            
            Name = scriptableObject.name;
            Life = scriptableObject.life;
            SpawnRate = scriptableObject.spawnRate;
            CurrentTimeToSpawn = 0;
        }

        protected override void Start()
        {
            base.Start();
        }
        #endregion MonoBehavior
        
        #region BaseEnemy
        protected override void FixedExecute()
        {
            CurrentTimeToSpawn += Time.deltaTime;

            if (CurrentTimeToSpawn >= SpawnRate)
            {
                CurrentTimeToSpawn -= SpawnRate;
                if (FleetCount < MaxFleet)
                {
                    SpawnShooterEnemy();
                }
            }
        }

        protected override void Dispose()
        {
            Life = scriptableObject.life;
            base.Dispose();
        }
        
        protected override void Initialize()
        {
            base.Initialize();
            Life = scriptableObject.life;
        }
        #endregion BaseEnemy

        #region IObservableEnemy
        public void SubscribeDestroySpawnerEnemy(Action<BaseEnemy> observer)
        {
            OnDestroySpawnerEnemy += observer;
        }

        public void UnSubscribeDestroySpawnerEnemy(Action<BaseEnemy> observer)
        {
            OnDestroySpawnerEnemy -= observer;
        }
        #endregion IObservableEnemy


        #region IObserverDestroyEnemy
        public void OnDestroyEnemy(BaseEnemy bulletObj)
        {
            FleetCount--;
        }
        #endregion IObserverDestroyEnemy

        void SpawnShooterEnemy()
        {
            FleetCount++;
            var enemyObj = EnemiesManager.Instance.GetShooterEnemy();
            var random = UnityEngine.Random.Range(0, TransformSpawners.Length - 1);
            var transformReference = TransformSpawners[random];
            var eventsEnemy = (IObservableEnemy)enemyObj;
            eventsEnemy.SubscribeDestroyEnemy(OnDestroyEnemy);
            
            enemyObj.transform.position = transformReference.position;
            enemyObj.transform.rotation = transformReference.rotation;
            enemyObj.transform.SetParent(transform);
        }
    }
}
