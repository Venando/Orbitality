using System.Collections.Generic;
using System.Globalization;
using Saving;
using UnityEngine;

namespace Gameplay.Player
{
    public class OrbitalObject : MonoBehaviour, ISavable
    {
        private float _movementSpeed;
        private float _currentAngle;
        private float _orbitalRadius;

        private void FixedUpdate()
        {
            Move();
        }

        private void Move()
        {
            float angleSpeed = Mathf.PI * _movementSpeed;

            _currentAngle += angleSpeed * Time.fixedDeltaTime;

            transform.position = GetPosition(_currentAngle);

            if (_currentAngle > 360 * Mathf.Deg2Rad)
                _currentAngle -= 360 * Mathf.Deg2Rad;
        }

        private Vector3 GetPosition(float angle)
        {
            return _orbitalRadius * new Vector3(Mathf.Cos(angle), 0f, Mathf.Sin(angle));
        }

        public float GetOrbitalRadius()
        {
            return _orbitalRadius;
        }

        public void SetPosition(float radius, float angle)
        {
            _orbitalRadius = radius;
            _currentAngle = angle;
            transform.position = GetPosition(_currentAngle);
        }

        public void SetMovementSpeed(float speed)
        {
            _movementSpeed = speed;
        }

        public void FillValuesToSave(List<SerializablePair> savedValues)
        {
            savedValues.Add(new SerializablePair(nameof(_currentAngle), _currentAngle.ToString(CultureInfo.InvariantCulture)));
            savedValues.Add(new SerializablePair(nameof(_orbitalRadius), _orbitalRadius.ToString(CultureInfo.InvariantCulture)));
            savedValues.Add(new SerializablePair(nameof(_movementSpeed), _movementSpeed.ToString(CultureInfo.InvariantCulture)));
        }

        public void SetLoadedValues(List<SerializablePair> loadedValues)
        {
            _currentAngle = float.Parse(loadedValues.GetValue(nameof(_currentAngle)));
            _orbitalRadius = float.Parse(loadedValues.GetValue(nameof(_orbitalRadius)));
            _movementSpeed = float.Parse(loadedValues.GetValue(nameof(_movementSpeed)));
            SetPosition(_orbitalRadius, _currentAngle);
        }

    }
}
