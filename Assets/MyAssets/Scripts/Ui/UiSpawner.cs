using UnityEngine;

namespace Ui
{
    public class UiSpawner : MonoBehaviour
    {
        [SerializeField] private AttachSlider _attachSliderPrefab;
        [SerializeField] private VerticalSlider _verticalSliderPrefab;
        [SerializeField] private AttachText _textPrefab;

        private static UiSpawner Instance { set; get; }

        private void Awake()
        {
            Instance = this;
        }

        public static void SpawnAttachedSlider(Transform attachRenderer, ISliderValueProvider valueProvider, float offset, Color tintColor)
        {
            AttachSlider slider = Instantiate(Instance._attachSliderPrefab, Instance.transform);
            slider.Attach(attachRenderer, offset);
            slider.SetValueProvider(valueProvider);
            slider.SetTintColor(tintColor);
        }

        public static void SpawnVerticalSlider(ISliderValueProvider valueProvider, float offset, Color tintColor)
        {
            VerticalSlider slider = Instantiate(Instance._verticalSliderPrefab, Instance.transform);

            var sliderRectTransform = slider.transform as RectTransform;

            sliderRectTransform.anchoredPosition += Vector2.right * offset;

            slider.SetValueProvider(valueProvider);
            slider.SetTintColor(tintColor);
        }

        public static void SpawnAttachedText(Transform attachRenderer, string text, float offset)
        {
            AttachText textObject = Instantiate(Instance._textPrefab, Instance.transform);
            textObject.SetText(text);
            textObject.Attach(attachRenderer, offset);
        }
    }
}
