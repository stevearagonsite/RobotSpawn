using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Utils.Consts;
using Managers;
using Bullets;

namespace Player
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(SphereCollider))]
    public class Player : MonoBehaviour
    {
        #region MonoBehavior
        private void OnCollisionEnter(Collision c)
        {
            if (c.gameObject.layer == Layers.BULLETS_NUM_LAYER)
            {
                Debug.Log("Che me atacaron!");
            }
        }
        #endregion MonoBehavior

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                var b = BulletsManager.Instance.GetBasicBullet();
                b.transform.position = transform.position + transform.forward * 5;
                b.transform.rotation = transform.rotation;
            }
        }
    }
}

