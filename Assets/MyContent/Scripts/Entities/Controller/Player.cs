using Managers;
using UnityEngine;
using Utils.Consts;

namespace Entities.Controller
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(SphereCollider))]
    public class Player : MonoBehaviour
    {
        private void Start()
        {

        }

        #region MonoBehavior
        private void OnCollisionEnter(Collision c)
        {
            if (c.gameObject.layer == Layers.BULLETS_NUM_LAYER)
            {
                Debug.Log("Che me atacaron!");
            }
        }
        #endregion MonoBehavior
        
        void Update()
        {
            #if UNITY_ANDROID || UNITY_IOS
            if (Input.touchCount > 0 && Input.touchCount < 2)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    CheckTouch();
                }
            }
            #endif
            
            #if UNITY_EDITOR_WIN
            if (Input.GetMouseButtonDown(0))
            {
                CheckTouch();
            }
            #endif
        }
 
        private void CheckTouch()
        {
            var b = BulletsManager.Instance.GetBasicBullet();
            var spawnPosition =  transform.position + transform.forward * 5;
            b.transform.position = spawnPosition;
            b.transform.rotation = transform.rotation;
        }
    }
}

