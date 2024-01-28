using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Gameplay.Player;
using UnityEngine;

namespace Gameplay.Controllers
{
    [RequireComponent(typeof(RocketFiring))]
    public class AiController : MonoBehaviour, IController
    {
        [SerializeField] private Vector2 _randomFireDelay = new(0f, 0.1f);
        [SerializeField] private float _preceptionAngle = 15f;

        private RocketFiring _rocketFiring;
        private List<Transform> _enemies;

        private void Awake()
        {
            _rocketFiring = GetComponent<RocketFiring>();
        }

        private void Start()
        {
            _enemies = FindObjectsOfType<PlanetHealth>()
                .Select(h => h.transform)
                .Where(t => t != transform)
                .ToList();

            StartCoroutine(Firing());
        }

        private IEnumerator Firing()
        {
            while (true)
            {
                yield return null;

                if (!_rocketFiring.CanFire())
                    continue;

                if (IsAnyEnemyInFront(_rocketFiring.GetFireDirection()))
                {
                    yield return new WaitForSeconds(Random.Range(_randomFireDelay.x, _randomFireDelay.y));
                    _rocketFiring.Fire();
                }
            }
        }

        private bool IsAnyEnemyInFront(Vector3 fireDirection)
        {
            for (int i = _enemies.Count - 1; i > -1; i--)
            {
                Transform enemy = _enemies[i];

                if (enemy == null)
                {
                    _enemies.RemoveAt(i);
                    continue;
                }

                Vector3 vectorToEnemy = (enemy.transform.position - transform.position);

                Vector3 directionToEnemy = vectorToEnemy.normalized;

                float angle = Mathf.Acos(Vector3.Dot(directionToEnemy, fireDirection)) * Mathf.Rad2Deg;

                if (angle < _preceptionAngle)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
