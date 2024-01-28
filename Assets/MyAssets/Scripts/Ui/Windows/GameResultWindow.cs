using Misc;
using UnityEngine;
using UnityEngine.UI;

namespace Ui.Windows
{
    public class GameResultWindow : UiWindowObject
    {
        [SerializeField] private Button _mainMenuButton;
        [SerializeField] private GameObject _winTitle;
        [SerializeField] private GameObject _loseTitle;
        [SerializeField] private float _delayBeforePause = 2f;

        protected override void Awake()
        {
            base.Awake();
            _mainMenuButton.onClick.AddListener(OnMainMenuButton);
        }
    
        public void Init(bool isWin)
        {
            Invoke(nameof(PauseGame), _delayBeforePause);
        
            _winTitle.SetActive(isWin);
            _loseTitle.SetActive(!isWin);
        
            gameObject.SetActive(true);
        }

        private void PauseGame()
        {
            if (gameObject.activeSelf)
            {
                PauseController.Pause();
            }
        }

        private void OnMainMenuButton()
        {
            SceneController.LoadMainMenu();
        }
    }
}
