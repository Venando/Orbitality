using UnityEngine;

namespace Ui
{
    public class UiWindowObject : MonoBehaviour
    {
        protected GameUiManager UiManager;
        
        protected virtual void Awake()
        {
            UiManager = GetComponentInParent<GameUiManager>();
        }
    }
}