using System.Collections.Generic;
using System.Globalization;
using Gameplay.Gravity;
using Saving;
using Ui;
using UnityEngine;

namespace Gameplay.Player
{
    [RequireComponent(typeof(SphereCollider))]
    public class RocketFiring : MonoBehaviour, ISliderValueProvider, ISavable
    {
        [SerializeField] private Transform _firePosition;

        private int _selectedRocket = -1;
        private Rocket _rocketPrefab;
        private float _fireTime;
        private float _cooldownTill;
        private SphereCollider _sphereCollider;
        private GravityApplier _gravityApplier;

        private void Awake()
        {
            _sphereCollider = GetComponent<SphereCollider>();
            _gravityApplier = GetComponent<GravityApplier>();
        }

        private void Start()
        {
            SpawnCooldownHud();
            CheckRocket();
        }

        private void SpawnCooldownHud()
        {
            if (PlayerHelper.IsPlayer(this))
                UiSpawner.SpawnVerticalSlider(this, 14, Color.yellow);
            else
                UiSpawner.SpawnAttachedSlider(transform, this, 27, Color.yellow);
        }

        private void CheckRocket()
        {
            if (_selectedRocket == -1)
            {
                _selectedRocket = RocketsManager.GetRandomRocketIndex();
            }
            
            GetSelectedRocketPrefab();
        }

        private void GetSelectedRocketPrefab()
        {
            _rocketPrefab = RocketsManager.GetRocketPrefab(_selectedRocket);
        }

        public void Fire()
        {
            if (!CanFire())
                return;

            Vector3 fireDirection = GetFireDirection();
            var rocketRenderer = _rocketPrefab.GetComponentInChildren<Renderer>();
            Vector3 firePosition = _firePosition.position + fireDirection * (_sphereCollider.radius + rocketRenderer.bounds.extents.x);
            Rocket rocket = SavablesManager.InstantiateSavable(_rocketPrefab, firePosition, Quaternion.LookRotation(fireDirection));
            rocket.SetLauncher(gameObject);
            rocket.GetComponent<GravityReceiver>().IgnoreGravityApplier = _gravityApplier;

            _fireTime = Time.time;
            _cooldownTill = _fireTime + rocket.GetCooldown();
        }

        public bool CanFire()
        {
            return _cooldownTill < Time.time;
        }
        
        public float GetCooldown()
        {
            return _cooldownTill - Time.time;
        }

        public Vector3 GetFireDirection()
        {
            return _firePosition.forward;
        }

        public float GetSliderFillPercent()
        {
            float cooldown = 1 - (_cooldownTill - Time.time) / (_cooldownTill - _fireTime);
            return cooldown > 1 || float.IsNaN(cooldown) ? 1 : cooldown;
        }

        public void FillValuesToSave(List<SerializablePair> savedValues)
        {
            savedValues.Add(new SerializablePair("TimeTillCooldown", GetCooldown().ToString(CultureInfo.InvariantCulture)));
            savedValues.Add(new SerializablePair("TimeSinceFiring", (Time.time - _fireTime).ToString(CultureInfo.InvariantCulture)));
            savedValues.Add(new SerializablePair(nameof(_selectedRocket), _selectedRocket.ToString()));
        }

        public void SetLoadedValues(List<SerializablePair> loadedValues)
        {
            _cooldownTill = Time.time + float.Parse(loadedValues.GetValue("TimeTillCooldown"));
            _fireTime = Time.time - float.Parse(loadedValues.GetValue("TimeSinceFiring"));
            _selectedRocket = int.Parse(loadedValues.GetValue(nameof(_selectedRocket)));
        }
    }
}
