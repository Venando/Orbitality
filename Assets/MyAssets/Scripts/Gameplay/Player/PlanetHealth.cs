using System;
using System.Collections.Generic;
using Saving;
using Ui;
using UnityEngine;

namespace Gameplay.Player
{
    public class PlanetHealth : MonoBehaviour, IDamageable, ISliderValueProvider, ISavable
    {
        private const int MaxHealth = 250;
        public int Health { get; private set; } = MaxHealth;
        public bool IsAlive => Health > 0;

        public event Action<PlanetHealth> DeathCallback;
    
        [SerializeField] private GameObject _deathExplosionPrefab;

        private void Start()
        {
            if (PlayerHelper.IsPlayer(this))
                UiSpawner.SpawnVerticalSlider(this, 0, Color.green);
            else
                UiSpawner.SpawnAttachedSlider(transform, this, 19, Color.green);
        }

        public void Damage(int damage)
        {
            Health -= damage;

            if (Health <= 0)
            {
                Death();
            }
        }

        private void Death()
        {
            Instantiate(_deathExplosionPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
            DeathCallback?.Invoke(this);
        }

        public float GetSliderFillPercent()
        {
            return Health / (float)MaxHealth;
        }
        
        public void FillValuesToSave(List<SerializablePair> savedValues)
        {
            savedValues.Add(new SerializablePair(nameof(Health), Health.ToString()));
        }

        public void SetLoadedValues(List<SerializablePair> loadedValues)
        {
            Health = int.Parse(loadedValues.GetValue(nameof(Health)));
        }
    }
}
