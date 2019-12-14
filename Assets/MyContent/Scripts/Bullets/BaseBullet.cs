using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Managers;

namespace Bullets
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(SphereCollider))]
    public abstract class BaseBullet : MonoBehaviour
    {
        [SerializeField]
        protected float _speed;

        private TrailRenderer[] _trails;
        protected const float MAX_TIME_LIFE = 5f;
        protected float _currentTimeLife = 0;

        #region MonoBehavior
        protected void Awake()
        {
            _trails = GetComponentsInChildren<TrailRenderer>();
        }

        protected void OnDestroy()
        {
            if (UpdateManager.Instance) UpdateManager.Instance.ExecuteFixed -= ExecuteFixed;
        }
        #endregion MonoBehavior

        protected abstract void ExecuteFixed();

        private void Initialize()
        {
            _currentTimeLife = 0;
            transform.position = Vector3.zero;

            if (UpdateManager.Instance) UpdateManager.Instance.ExecuteFixed += ExecuteFixed;

            foreach (var trail in _trails)
            {
                trail.time = 1f;
            }
        }

        private void Dispose()
        {
            if (UpdateManager.Instance) UpdateManager.Instance.ExecuteFixed -= ExecuteFixed;

            foreach (var trail in _trails)
            {
                trail.time = 0;
            }
        }

        public static void InitializeBullet(BaseBullet bulletObj)
        {
            bulletObj.gameObject.SetActive(true);
            bulletObj.Initialize();
        }

        public static void DisposeBullet(BaseBullet bulletObj)
        {
            bulletObj.Dispose();
            bulletObj.gameObject.SetActive(false);
        }
    }
}

