namespace Saving
{
    [System.Serializable]
    public struct SerializablePair
    {
        public string Key;
        public string Value;
        
        public SerializablePair(string key, string value)
        {
            Key = key;
            Value = value;
        }
    }
}