using UnityEngine;

namespace Saving
{
    public static class SaveManager
    {
        private const string SaveKey = nameof(SaveKey);
        private const string IsSaveLoadingKey = nameof(IsSaveLoadingKey);

        public static bool HasSave()
        {
            return PlayerPrefs.HasKey(SaveKey);
        }

        public static string GetSave()
        {
            return PlayerPrefs.GetString(SaveKey);
        }

        public static void SaveGame(string str)
        {
            PlayerPrefs.SetString(SaveKey, str);
        }

        public static void SetSaveLoad()
        {
            PlayerPrefs.SetInt(IsSaveLoadingKey, 1);
        }

        public static bool IsSaveLoading()
        {
            return PlayerPrefs.HasKey(IsSaveLoadingKey);
        }

        public static void GameLoaded()
        {
            PlayerPrefs.DeleteKey(IsSaveLoadingKey);
        }

    }
}
