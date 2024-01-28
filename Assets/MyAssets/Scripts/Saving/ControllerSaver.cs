using System;
using System.Collections.Generic;
using Gameplay.Controllers;
using UnityEngine;

namespace Saving
{
    public class ControllerSaver : MonoBehaviour, ISavable
    {
        public void FillValuesToSave(List<SerializablePair> savedValues)
        {
            var controller = GetComponent<IController>();
            savedValues.Add(new SerializablePair("Controller", controller.GetType().ToString()));
        }

        public void SetLoadedValues(List<SerializablePair> loadedValues)
        {
            gameObject.AddComponent(Type.GetType(loadedValues.GetValue("Controller")));
        }
    }
}
