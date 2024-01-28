using Misc;
using Saving;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private Button _continueButton;
        [SerializeField] private Button _newGameButton;

        private void Awake()
        {
            _continueButton.onClick.AddListener(OnContinueButton);
            _newGameButton.onClick.AddListener(OnNewGameButton);
            _continueButton.interactable = SaveManager.HasSave();
        }

        private void OnContinueButton()
        {
            SaveManager.SetSaveLoad();
            SceneController.LoadGame();
        }

        private void OnNewGameButton()
        {
            SceneController.LoadGame();
        }
    }
}
