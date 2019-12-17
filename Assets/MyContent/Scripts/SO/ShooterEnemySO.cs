using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SO
{
    [CreateAssetMenu(fileName = "New Shooter Enemy", menuName = "Enemies/Shooter Enemy")]
    public class ShooterEnemySO : ScriptableObject
    {
        public string name;
        public float speed;
        public float life;
        public float damage;
        public float rangeForFire;
        public float fireRate;
    }
}

