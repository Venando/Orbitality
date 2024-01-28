using System.Linq;
using Gameplay.Controllers;
using Gameplay.Player;
using Saving;
using UnityEngine;
using UnityEngine.Serialization;

namespace Gameplay
{
    public class PlanetSpawnManager : MonoBehaviour
    {
        [SerializeField] private GameObject _planetPrefab;
        [SerializeField] float _distanceBetweenPlanets = 3;
        [SerializeField] private float _minOrbitalRadius = 4.5f;
        [SerializeField] private Vector2 _minMaxMoveSpeed = new(0.1f, 0.3f);
        [SerializeField] private float _angularVelocity = 4f;
        [SerializeField] private Vector2Int _minMaxAiPlayersNumber = new(3, 4);

        private void Awake()
        {
            if (SaveManager.IsSaveLoading())
            {
                SaveManager.GameLoaded();
                SavablesManager.LoadSave(SaveManager.GetSave());
            }
            else
            {
                Spawn();
            }

            SetupCameraHeight();
        }

        private void Spawn()
        {
            int numberOfPlanets = 1 + Random.Range(_minMaxAiPlayersNumber.x, _minMaxAiPlayersNumber.y + 1);
            int humanPlayerPlanetIndex = Random.Range(0, numberOfPlanets);

            for (int i = 0; i < numberOfPlanets; i++)
            {
                float spawnRadius = _minOrbitalRadius + i * _distanceBetweenPlanets;
                float spawnAngle = Random.Range(0f, 360f) * Mathf.Deg2Rad;

                GameObject planet = SavablesManager.InstantiateSavable(_planetPrefab);

                var rigidbody = planet.GetComponent<Rigidbody>();

                float angularVelocity = _angularVelocity * (Random.value > 0.5 ? -1f : 1f);
                rigidbody.angularVelocity = new Vector3(0f, angularVelocity, 0f) ;
                planet.transform.localEulerAngles = new Vector3(0f, Random.Range(0f, 360f), 0f);

                var orbitalObject = planet.GetComponent<OrbitalObject>();

                orbitalObject.SetMovementSpeed(Random.Range(_minMaxMoveSpeed.x, _minMaxMoveSpeed.y));
                orbitalObject.SetPosition(spawnRadius, spawnAngle);

                if (i == humanPlayerPlanetIndex)
                    planet.AddComponent<PlayerController>();
                else
                    planet.AddComponent<AiController>();
            }

        }

        private void SetupCameraHeight()
        {
            Camera mainCamera = Camera.main;
            float maxRadius = FindObjectsOfType<OrbitalObject>().Max(o => o.GetOrbitalRadius()) + _distanceBetweenPlanets;
            float halfObserveAngle = mainCamera.fieldOfView / 2f;
            float height = Mathf.Sin((90f - halfObserveAngle) * Mathf.Deg2Rad) * maxRadius / Mathf.Sin(halfObserveAngle * Mathf.Deg2Rad);
            mainCamera.transform.position = Vector3.up * height;
        }
    }
}
