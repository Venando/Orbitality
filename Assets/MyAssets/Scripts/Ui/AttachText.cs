using TMPro;
using UnityEngine;

namespace Ui
{
    [RequireComponent(typeof(TMP_Text), typeof(AttachedUiObject))]
    public class AttachText : MonoBehaviour
    {
        public void Attach(Transform attachRenderer, float offset)
        {
            GetComponent<AttachedUiObject>().Attach(attachRenderer, offset);
        }

        public void SetText(string text)
        {
            GetComponent<TMP_Text>().text = text;
        }
    }
}