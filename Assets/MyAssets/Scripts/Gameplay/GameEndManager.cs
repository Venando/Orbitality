using System.Linq;
using Gameplay.Player;
using Ui;
using Ui.Windows;
using UnityEngine;

namespace Gameplay
{
    public class GameEndManager : MonoBehaviour
    {
        private PlanetHealth[] _planetHealths;
        
        private bool _isGameEnded;
        
        private void Start()
        {
            _planetHealths = FindObjectsOfType<PlanetHealth>();
            foreach (PlanetHealth planetHealth in _planetHealths)
            {
                planetHealth.DeathCallback += OnPlanetDeath;
            }
        }

        private void OnPlanetDeath(PlanetHealth playerHealth)
        {
            if (_isGameEnded) 
                return;
            
            playerHealth.DeathCallback -= OnPlanetDeath;
            
            _isGameEnded = CheckGameEnd(playerHealth);
            
            if (_isGameEnded)
                UnsubscribeFromPlanetsDeath();
        }

        private bool CheckGameEnd(PlanetHealth diedPlayer)
        {
            if (diedPlayer.IsPlayer())
            {
                OpenLoseWindow();
                return true;
            }

            if (_planetHealths.Count(planetHealth => planetHealth.IsAlive) == 1)
            {
                OpenWinWindow();
                return true;
            }

            return false;
        }

        private void OpenWinWindow()
        {
            GameUiManager.OpenWindow<GameResultWindow>().Init(true);
        }

        private void OpenLoseWindow()
        {
            GameUiManager.OpenWindow<GameResultWindow>().Init(false);
        }

        private void OnDestroy()
        {
            UnsubscribeFromPlanetsDeath();
        }

        private void UnsubscribeFromPlanetsDeath()
        {
            if (_planetHealths == null)
                return;

            foreach (PlanetHealth planetHealth in _planetHealths)
            {
                if (planetHealth == null)
                    continue;
                
                planetHealth.DeathCallback -= OnPlanetDeath;
            }
        }
    }
}