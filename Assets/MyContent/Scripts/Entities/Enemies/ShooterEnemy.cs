using System;
using UnityEngine;

using Managers;
using Entities.Controller;
using SO;

namespace Entities.Enemies
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Collider))]
    public class ShooterEnemy : BaseEnemy, IFire
    {
        #region Parameters & attributes
        public ShooterEnemySO scriptableObject;
        public Transform Target { get; set; }
        public bool IsFiring { get; private set;}
        public float RangeForFire { get; private set; }

        public float FireRate { get { return _life;} private set{_life = value;}}
        #endregion Parameters & attributes

        #region MonoBehavior
        protected override void Awake()
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

        protected void Start()
        {
            var player = FindObjectOfType<Player>().transform;
            #if UNITY_EDITOR
            if (!player) Debug.LogError("The player isn't in the scene.");
            #endif
        }
        #endregion MonoBehavior

        #region BaseEnemy
        protected override void FixedExecute()
        {
            var distance = Vector3.Distance(transform.position, Target.position);
            if (distance < RangeForFire)
            {
                StartFire();
            }else if (IsFiring)
            {
                StopFire();
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
    }
}
