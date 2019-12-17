using Managers;
using UnityEngine;
using Utils.Consts;

namespace Entities.Controller
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
        
        void Update()
        {
            #if UNITY_ANDROID || UNITY_IOS
            if (Input.touchCount > 0 && Input.touchCount < 2)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    CheckTouch(Input.GetTouch(0).position);
                }
            }
            #endif
            
            #if UNITY_EDITOR_WIN
            if (Input.GetMouseButtonDown(0))
            {
                CheckTouch(Input.mousePosition);
            }
            #endif
        }
 
        private void CheckTouch(Vector3 pos)
        {
            RaycastHit hit;
            var ray = Camera.main.ScreenPointToRay(pos);
            if (!Physics.Raycast(ray, out hit)) return;
            
            var objectHit = hit.transform;
            if (objectHit.gameObject.layer == Layers.ENEMIES_NUM_LAYER)
            {
                var b = BulletsManager.Instance.GetBasicBullet();
                var spawnPosition =  transform.position + transform.forward * 5;
                var forward = Vector3.Cross(spawnPosition, objectHit.transform.position).normalized;

                b.transform.position = spawnPosition;
                b.transform.Rotate(forward);
            }
        }
    }
}

