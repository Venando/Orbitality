using Misc;
using UnityEngine;
using UnityEngine.UI;

namespace Ui.Windows
{
    public class PauseButtonContainer : UiWindowObject
    {
        [SerializeField] private Button _pauseButton;

        protected override void Awake()
        {
            base.Awake();
            _pauseButton.onClick.AddListener(OnPauseButton);
        }

        private void OnPauseButton()
        {
            PauseController.Pause();
            UiManager.Open<PausePanelWindow>();
        }
    }
}