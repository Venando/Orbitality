using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Gravity
{
    public class GravityController : MonoBehaviour
    {
        [SerializeField] private float _g = 7f;

        private readonly List<GravityApplier> _gravityAppliers = new();
        private readonly List<GravityReceiver> _gravityReceivers = new();

        public static GravityController Instance { private set; get; }

        private void Awake()
        {
            Instance = this;
        }

        private void FixedUpdate()
        {
            foreach (GravityReceiver receiver in _gravityReceivers)
            {
                ReceiveGravity(receiver);
            }
        }

        private void ReceiveGravity(GravityReceiver receiver)
        {
            Vector3 forceSum = Vector3.zero;

            Rigidbody gravityReceiverRigidbody = receiver.GravityObjectRigidbody;
            float gravityReceiverMass = gravityReceiverRigidbody.mass;
            
            GravityApplier receiverIgnoreGravityApplier = receiver.IgnoreGravityApplier;
            
            foreach (GravityApplier applier in _gravityAppliers)
            {
                if (receiverIgnoreGravityApplier == applier)
                    continue;

                Vector3 direction = applier.transform.position - receiver.transform.position;
                float sqrDistance = direction.sqrMagnitude;
                float forceMagnitude = _g * ((gravityReceiverMass * applier.GravityObjectRigidbody.mass) / sqrDistance);
                Vector3 normalizedDirection = direction.normalized / Mathf.Sqrt(sqrDistance);
                forceSum += normalizedDirection * forceMagnitude;
            }
            
            gravityReceiverRigidbody.AddForce(forceSum);
        }

        public void AddGravityApplier(GravityApplier gravityApplier)
        {
            if (!_gravityAppliers.Contains(gravityApplier))
                _gravityAppliers.Add(gravityApplier);
        }

        public void RemoveGravityApplier(GravityApplier gravityApplier)
        {
            if (_gravityAppliers.Contains(gravityApplier))
                _gravityAppliers.Remove(gravityApplier);
        }

        public void AddGravityReceiver(GravityReceiver gravityReceiver)
        {
            if (!_gravityReceivers.Contains(gravityReceiver))
                _gravityReceivers.Add(gravityReceiver);
        }

        public void RemoveGravityReceiver(GravityReceiver gravityReceiver)
        {
            if (_gravityReceivers.Contains(gravityReceiver))
                _gravityReceivers.Remove(gravityReceiver);
        }
    }
}
