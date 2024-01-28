using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Gameplay
{
    public class PlanetSkinManager : MonoBehaviour
    {
        private static PlanetSkinManager Instance { get; set; }
        
        [SerializeField] private GameObject[] _planetSkins;

        private int[] _skinQueue;
        private int _skinOrder;

        private void Awake()
        {
            Instance = this;
        }

        public static int GetNextSkinIndex()
        {
            return Instance.GetNextSkinIndexInternal();
        }
        
        private int GetNextSkinIndexInternal()
        {
            if (_skinQueue == null)
                InitSkinQueue();
            
            _skinOrder = _skinOrder >= _skinQueue.Length ? 0 : _skinOrder;
            return _skinQueue[_skinOrder++];
        }

        private void InitSkinQueue()
        {
            _skinQueue = Enumerable.Range(0, _planetSkins.Length).OrderBy(_ => Random.value).ToArray(); 
        }

        public static GameObject GetSkin(int index)
        {
            return Instance.GetSkinInternal(index);
        }

        private GameObject GetSkinInternal(int index)
        {
            if (index < 0 || index >= _planetSkins.Length)
                return _planetSkins[Random.Range(0, _planetSkins.Length)];
            return _planetSkins[index];
        }
    }
}