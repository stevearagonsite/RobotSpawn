using UnityEngine;


namespace SO
{
    [CreateAssetMenu(fileName = "New Spawner Enemy", menuName = "Enemies/Spawner Enemy")]
    public class SpawnerEnemySO : ScriptableObject
    {
        public string name;
        public float life;
        public float spawnRate;
        public int maxFleet;
    }
}