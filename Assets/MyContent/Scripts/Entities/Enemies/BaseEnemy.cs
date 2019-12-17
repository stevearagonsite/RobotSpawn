using System;
using UnityEngine;

using Managers;

namespace Entities.Enemies
{
    public abstract class BaseEnemy : MonoBehaviour, IDamage
    {
        #region Parameters & attributes
        protected string _name;
        protected float _damage,_speed,_life;
        public virtual string Name { get { return _name;} protected set{_name = value;}}
        public virtual float Speed  { get { return _speed;} protected set{_speed = value;}}
        public virtual float Life { get { return _life;} protected set{_life = value;}}
        public virtual float Damage { get { return _damage;} protected set{_damage = value;}}
        #endregion Parameters & attributes
        
        #region MonoBehavior
        protected virtual void Start()
        {
            UpdateManager.Instance.ExecuteFixed += FixedExecute;
        }
        #endregion MonoBehavior
        
        #region IDamage
        public virtual void RecibeDamage(float amount)
        {
            _life -= amount;
            if (_life <= 0)
            {
                // TODO: Dispatch the event with observer.
            }
        }
        #endregion IDamage
        
        protected abstract void FixedExecute();

        public static void InitializeEnemy(BaseEnemy enemyObj)
        {
            enemyObj.gameObject.SetActive(true);
            enemyObj.Initialize();
        }

        public static void DisposeEnemy(BaseEnemy enemyObj)
        {
            enemyObj.Dispose();
            enemyObj.gameObject.SetActive(false);
        }

        protected virtual void Initialize()
        {
            transform.position = Vector3.zero;
            if (UpdateManager.Instance) UpdateManager.Instance.ExecuteFixed += FixedExecute;
        }

        protected virtual void Dispose()
        {
            if (UpdateManager.Instance) UpdateManager.Instance.ExecuteFixed -= FixedExecute;
        }
    }
}

