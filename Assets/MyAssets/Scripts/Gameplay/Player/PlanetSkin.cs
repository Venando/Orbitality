using System.Collections.Generic;
using Saving;
using UnityEngine;

namespace Gameplay.Player
{
    public class PlanetSkin : MonoBehaviour, ISavable
    {
        private int _selectedSkin = -1;

        private void Start()
        {
            SetupSkin();
        }

        private void SetupSkin()
        {
            if (_selectedSkin == -1)
            {
                _selectedSkin = PlanetSkinManager.GetNextSkinIndex();
            }
            
            InstantiateSkin();
        }

        private void InstantiateSkin()
        {
            Instantiate(PlanetSkinManager.GetSkin(_selectedSkin), transform);
        }

        public void FillValuesToSave(List<SerializablePair> savedValues)
        {
            savedValues.Add(new SerializablePair(nameof(_selectedSkin), _selectedSkin.ToString()));
        }

        public void SetLoadedValues(List<SerializablePair> loadedValues)
        {
            _selectedSkin = int.Parse(loadedValues.GetValue(nameof(_selectedSkin)));
        }
    }
}
