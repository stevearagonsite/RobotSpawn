using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Managers;

namespace Bullets
{
    public class BasicBullet : BaseBullet
    {
        protected override void ExecuteFixed()
        {
            _currentTimeLife += Time.deltaTime;
            transform.position += transform.forward * (_speed * Time.deltaTime);

            Debug.Log(_currentTimeLife);
            if (_currentTimeLife >= MAX_TIME_LIFE)
            {
                DisposeBullet(this);
            }
        }
    }
}

