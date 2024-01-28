using UnityEngine;

namespace Gameplay.Gravity
{
    [RequireComponent(typeof(Rigidbody))]
    public abstract class GravityObject : MonoBehaviour
    {
        public Rigidbody GravityObjectRigidbody { private set; get; }

        private void Awake()
        {
            GravityObjectRigidbody = GetComponent<Rigidbody>();
        }
    }
}
