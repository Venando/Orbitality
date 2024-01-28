﻿using UnityEngine.SceneManagement;

namespace Misc
{
    public static class SceneController
    {
        public static void LoadMainMenu()
        {
            PauseController.Resume();
            SceneManager.LoadScene(0);
        }

        public static void LoadGame()
        {
            PauseController.Resume();
            SceneManager.LoadScene(1);
        }
    }
}