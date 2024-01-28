using System.Collections.Generic;
using Misc;
using UnityEngine;

namespace Saving
{
    public class PositionSaver : MonoBehaviour, ISavable
    {
        public void FillValuesToSave(List<SerializablePair> savedValues)
        {
            savedValues.Add(new SerializablePair(nameof(transform.position), transform.position.ToParsableString()));
            savedValues.Add(new SerializablePair(nameof(transform.rotation.eulerAngles), transform.rotation.eulerAngles.ToParsableString()));
            savedValues.Add(new SerializablePair(nameof(transform.localScale), transform.localScale.ToParsableString()));
        }

        public void SetLoadedValues(List<SerializablePair> loadedValues)
        {
            transform.position = loadedValues.GetValue(nameof(transform.position)).ToVector3();
            transform.eulerAngles = loadedValues.GetValue(nameof(transform.rotation.eulerAngles)).ToVector3();
            transform.localScale = loadedValues.GetValue(nameof(transform.localScale)).ToVector3();
        }
    }
}
