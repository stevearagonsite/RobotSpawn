using System;
using Entities.Controller;
using Entities.Enemies.Events;
using SO;
using UnityEngine;

namespace Entities.Enemies
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Collider))]
    public class ShooterEnemy : BaseEnemy, IFire, IObservableEnemy
    {
        #region Parameters & attributes
        public ShooterEnemySO scriptableObject;
        public Transform[] TransformCannonSpawners;
        public Transform Target { get; set; }
        protected event Action<BaseEnemy> OnDestroyEnemy = delegate { };
        public bool IsFiring { get; private set;}
        public float RangeForFire { get; private set; }

        public float FireRate { get { return _life;} private set{_life = value;}}
        #endregion Parameters & attributes

        #region MonoBehavior
        protected void Awake()
        {
            #if UNITY_EDITOR
            if (!scriptableObject) Debug.LogError($"The {transform.name} need the scriptable object.");
            #endif
            
            Name = scriptableObject.name;
            Life = scriptableObject.life;
            Speed = scriptableObject.speed;
            Damage = scriptableObject.damage;
            FireRate = scriptableObject.fireRate;
            RangeForFire = scriptableObject.rangeForFire;
        }

        protected override void Start()
        {
            base.Start();
            Target = FindObjectOfType<Player>().transform;
            
            #if UNITY_EDITOR
            if (!Target) Debug.LogError("The player isn't in the scene.");
            foreach (var transformSpawner in TransformCannonSpawners)
            {
                if (!transformSpawner) Debug.LogError("The Cannon N1 hasn't reference.");
            }
            #endif
        }
        #endregion MonoBehavior

        #region BaseEnemy
        protected override void FixedExecute()
        {

            var distance = Vector3.Distance(transform.position, Target.position);
            transform.LookAt(Target);
            
            if (distance < RangeForFire)
            {
                StartFire();
            }else if (IsFiring)
            {
                StopFire();
            }
            else
            {
                transform.position += transform.forward * (Speed * Time.deltaTime);
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
        
        #region IFire
        public void StartFire()
        {
            Debug.Log("Is Firing");
        }

        public void StopFire()
        {
            Debug.Log("Stop Fire");
        }
        #endregion IFire

        #region IObservableEnemy
        public void SubscribeDestroyEnemy(Action<BaseEnemy> observer)
        {
            OnDestroyEnemy += observer;
        }

        public void UnSubscribeDestroyEnemy(Action<BaseEnemy> observer)
        {
            OnDestroyEnemy -= observer;
        }
        #endregion IObservableEnemy
    }
}
