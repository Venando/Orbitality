using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Ui
{
    [RequireComponent(typeof(Slider))]
    public class ValueSlider : MonoBehaviour
    {
        [SerializeField] private Image _brightImage;
        [SerializeField] private Image _dimImage;

        private ISliderValueProvider _valueProvider;
        private Slider _slider;

        protected virtual void Awake()
        {
            _slider = GetComponent<Slider>();
        }

        protected virtual void Update()
        {
            if (_valueProvider == null)
                return;
        
            _slider.value = _valueProvider.GetSliderFillPercent();
        }

        public void SetValueProvider(ISliderValueProvider valueProvider)
        {
            _valueProvider = valueProvider;
        }

        public void SetTintColor(Color tintColor)
        {
            Color.RGBToHSV(tintColor, out var H, out var S, out var V);

            _brightImage.color = Color.HSVToRGB(H, S / 2f, V);
            _dimImage.color = Color.HSVToRGB(H, S / 20f, V);
        }
    }
}
