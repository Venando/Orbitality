using Gameplay.Player;
using UnityEngine;

namespace Gameplay
{
    public class RocketsManager : MonoBehaviour
    {
        [SerializeField] private Rocket[] _rocketPrefabs;
        
        private static RocketsManager Instance { get; set; }

        private void Awake()
        {
            Instance = this;
        }
        
        public static int GetRandomRocketIndex()
        {
            return Random.Range(0, Instance._rocketPrefabs.Length);
        }
        
        public static Rocket GetRocketPrefab(int index)
        {
            if (index < 0 || index >= Instance._rocketPrefabs.Length)
                index = GetRandomRocketIndex();
            return Instance._rocketPrefabs[index];
        }
    }
}
