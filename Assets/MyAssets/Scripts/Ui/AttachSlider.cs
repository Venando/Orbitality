using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
    [RequireComponent(typeof(Slider), typeof(AttachedUiObject))]
    public class AttachSlider : ValueSlider
    {
        public void Attach(Transform attachRenderer, float offset)
        {
            GetComponent<AttachedUiObject>().Attach(attachRenderer, offset);
        }
    }
}
