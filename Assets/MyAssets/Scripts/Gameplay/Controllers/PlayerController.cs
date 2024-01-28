using Gameplay.Player;
using Misc;
using Ui;
using UnityEngine;

namespace Gameplay.Controllers
{
    [RequireComponent(typeof(RocketFiring))]
    public class PlayerController : MonoBehaviour, IController
    {
        private RocketFiring _rocketFiring;

        private void Awake()
        {
            _rocketFiring = GetComponent<RocketFiring>();
            UiSpawner.SpawnAttachedText(transform, "ME", 20);
        }

        private void Update()
        {
            if (InputManager.IsClick())
            {
                _rocketFiring.Fire();
            }
        }
    }
}
