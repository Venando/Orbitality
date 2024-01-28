using Gameplay.Controllers;
using UnityEngine;

namespace Gameplay.Player
{
    public static class PlayerHelper
    {
        public static bool IsPlayer(this MonoBehaviour monoBehaviour)
        {
            return monoBehaviour.GetComponent<PlayerController>();
        }
    }
}