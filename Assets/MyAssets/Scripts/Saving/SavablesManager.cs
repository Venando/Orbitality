using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Saving
{
    public class SavablesManager : MonoBehaviour
    {
        private static SavablesManager Instance { set; get; }

        private readonly List<(string name, GameObject gameObject)> _objectsToSave = new();

        private void Awake()
        {
            Instance = this;
        }

        public static GameObject InstantiateSavable(GameObject prefab)
        {
            var obj = Instantiate(prefab);
            Instance.AddObjectToSave(prefab.name, obj.gameObject);
            return obj;
        }

        public static T InstantiateSavable<T>(T prefab, Vector3 position, Quaternion rotation) where T : Component
        {
            T obj = Instantiate(prefab, position, rotation);
            Instance.AddObjectToSave(prefab.name, obj.gameObject);
            return obj;
        }

        private void AddObjectToSave(string name, GameObject gameObject)
        {
            _objectsToSave.RemoveAll(n => n.gameObject == null);
            _objectsToSave.Add((name, gameObject));
        }

        public static void LoadSave(string saveData)
        {
            Instance._objectsToSave.RemoveAll(n => n.gameObject == null);
            Instance._objectsToSave.ForEach(n => Destroy(n.gameObject));

            var savedObjects = JsonUtility.FromJson<SerializableList>(saveData);

            savedObjects.List.ForEach(obj =>
            {
                var objPrefab = Resources.Load<GameObject>(obj.Key);

                GameObject loadedObj = InstantiateSavable(objPrefab);

                var serializedComponentsList = JsonUtility.FromJson<SerializableList>(obj.Value);

                serializedComponentsList.List.ForEach(serializedComponent => 
                {
                    ISavable component = (ISavable)loadedObj.GetComponent(serializedComponent.Key);
                    List<SerializablePair> loadedValues = JsonUtility.FromJson<SerializableList>(serializedComponent.Value).List;
                    component.SetLoadedValues(loadedValues);
                });
            });
        }

        public static string GenerateSave()
        {
            List<(string name, GameObject gameObject)> objectsToSave = Instance._objectsToSave;

            objectsToSave.RemoveAll(n => n.gameObject == null);

            List<SerializablePair> savedObjectList = objectsToSave.Select(PrefabNameAndGameObjectToSerializablePair).ToList();

            var serializableList = new SerializableList { List = savedObjectList };

            return JsonUtility.ToJson(serializableList);
        }

        private static SerializablePair PrefabNameAndGameObjectToSerializablePair((string name, GameObject gameObject) objPair)
        {
            List<SerializablePair> savedObjectList = GameObjectToSerializablePairList(objPair.gameObject);
            var serializableList = new SerializableList { List = savedObjectList };

            string json = JsonUtility.ToJson(serializableList);
            string key = objPair.name;

            return new SerializablePair { Key = key, Value = json };
        }

        private static List<SerializablePair> GameObjectToSerializablePairList(GameObject gameObject)
        {
            return gameObject.GetComponents<ISavable>().Select(SavableToSerializablePair).ToList();
        }

        private static SerializablePair SavableToSerializablePair(ISavable savable)
        {
            List<SerializablePair> savedObjectList = UnityEngine.Pool.ListPool<SerializablePair>.Get();
            savable.FillValuesToSave(savedObjectList);

            var serializableList = new SerializableList { List = savedObjectList };

            string json = JsonUtility.ToJson(serializableList);
            var key = savable.GetType().ToString();

            UnityEngine.Pool.ListPool<SerializablePair>.Release(savedObjectList);
            
            return new SerializablePair { Key = key, Value = json };
        }
    }
}