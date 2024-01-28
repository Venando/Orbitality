using System.Collections.Generic;
using System.Globalization;
using Saving;
using UnityEngine;
using UnityEngine.Serialization;

namespace Gameplay.Player
{
    [RequireComponent(typeof(Rigidbody), typeof(Collider))]
    public class Rocket : MonoBehaviour, IDamageable, ISavable
    {
        [SerializeField] private float _acceleration;
        [SerializeField] private float _cooldown;
        [SerializeField] private int _rocketDamage;
        [SerializeField] private float _exposionScale;
        [SerializeField] private GameObject _rocketExplosion;

        private Rigidbody _rigidbody;
        private GameObject _rocketLauncher;
        private float _autoDestroyTime;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }
        private void Start()
        {
            AddStartSpeed();
            _autoDestroyTime = Time.time + 10f;
        }

        private void AddStartSpeed()
        {
            _rigidbody.AddForce(transform.forward * _acceleration * 150);
        }

        private void FixedUpdate()
        {
            _rigidbody.AddForce(transform.forward * _acceleration);
            if (_rigidbody.velocity.sqrMagnitude > 0f)
            {
                var angle = Mathf.Atan2(_rigidbody.velocity.z, -_rigidbody.velocity.x) * Mathf.Rad2Deg - 90f;
                transform.localEulerAngles = new Vector3(0f, angle, 0f);
            }

            if (Time.time > _autoDestroyTime)
                Destroy(gameObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject == _rocketLauncher)
                return;

            other.GetComponent<IDamageable>()?.Damage(_rocketDamage);
            MakeExplosion();
        }

        private void MakeExplosion()
        {
            GameObject explosionGameObject = Instantiate(_rocketExplosion, transform.position, Quaternion.identity);
            explosionGameObject.transform.localScale = Vector3.one * _exposionScale;
            Destroy(gameObject);
        }

        public void Damage(int damage)
        {
            if (gameObject != null)
                Destroy(gameObject);
        }

        public void SetLauncher(GameObject launcherGameObject)
        {
            _rocketLauncher = launcherGameObject;
        }

        public void FillValuesToSave(List<SerializablePair> savedValues)
        {
            savedValues.Add(new SerializablePair("TimeTillDestroy", (_autoDestroyTime - Time.time).ToString(CultureInfo.InvariantCulture)));
        }

        public void SetLoadedValues(List<SerializablePair> loadedValues)
        {
            _autoDestroyTime = Time.time + float.Parse(loadedValues.GetValue("TimeTillDestroy"));
        }

        public float GetCooldown()
        {
            return _cooldown;
        }
    }
}
