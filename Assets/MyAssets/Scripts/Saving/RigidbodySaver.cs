using System.Collections.Generic;
using Misc;
using UnityEngine;

namespace Saving
{
    [RequireComponent(typeof(Rigidbody))]
    public class RigidbodySaver : MonoBehaviour, ISavable
    {
        public void FillValuesToSave(List<SerializablePair> savedValues)
        {
            var rigidbody = GetComponent<Rigidbody>();
            
            savedValues.Add(new SerializablePair(nameof(rigidbody.velocity), rigidbody.velocity.ToParsableString()));
            savedValues.Add(new SerializablePair(nameof(rigidbody.angularVelocity), rigidbody.angularVelocity.ToParsableString()));
        }

        public void SetLoadedValues(List<SerializablePair> loadedValues)
        {
            var rigidbody = GetComponent<Rigidbody>();

            rigidbody.velocity = loadedValues.GetValue(nameof(rigidbody.velocity)).ToVector3();
            rigidbody.angularVelocity = loadedValues.GetValue(nameof(rigidbody.angularVelocity)).ToVector3();
        }
    }
}
