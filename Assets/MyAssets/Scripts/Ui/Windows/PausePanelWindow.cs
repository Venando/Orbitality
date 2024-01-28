using Misc;
using Saving;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Ui.Windows
{
    public class PausePanelWindow : UiWindowObject
    {
        [SerializeField] private Button _saveButton;
        [SerializeField] private Button _mainMenuButton;
        [SerializeField] private Button _resumeButton;
        [SerializeField] private TMP_Text _successfulSaveText;
    
        protected override void Awake()
        {
            base.Awake();
            _saveButton.onClick.AddListener(OnSaveButton);
            _mainMenuButton.onClick.AddListener(OnMainMenuButton);
            _resumeButton.onClick.AddListener(OnResumeButton);
        }

        private void OnSaveButton()
        {
            _successfulSaveText.alpha = 1f;
            this.TextFade(_successfulSaveText, 0.4f, 0.8f, 0f);
            SaveManager.SaveGame(SavablesManager.GenerateSave());
        }

        private void OnMainMenuButton()
        {
            SceneController.LoadMainMenu();
        }

        private void OnResumeButton()
        {
            PauseController.Resume();
            UiManager.ResetToDefault();
        }
    }
}
