using System.Collections.Generic;

namespace Saving
{
    public interface ISavable
    {
        void FillValuesToSave(List<SerializablePair> savedValues);
        void SetLoadedValues(List<SerializablePair> loadedValues);
    }
}
