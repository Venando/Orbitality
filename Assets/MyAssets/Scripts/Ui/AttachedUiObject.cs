using UnityEngine;

namespace Ui
{
    public class AttachedUiObject : MonoBehaviour
    {
        private Transform _attachedRenderer;
        private Camera _mainCamera;
        private RectTransform _rectTransform;
        private RectTransform _canvasRectTransform;
        private float _offset;

        private void Awake()
        {
            _mainCamera = Camera.main;
            _rectTransform = transform as RectTransform;
            _canvasRectTransform = (RectTransform)GetComponentInParent<Canvas>().transform;
        }

        private void Update()
        {
            if (CheckAttachedObject()) 
                return;

            UpdatePosition();
        }

        private bool CheckAttachedObject()
        {
            if (_attachedRenderer == null)
            {
                if (gameObject != null)
                    Destroy(gameObject);
                return true;
            }

            return false;
        }

        private void UpdatePosition()
        {
            Vector3 attachWorldPosition = _attachedRenderer.transform.position;
            Vector3 viewportPosition = _mainCamera.WorldToViewportPoint(attachWorldPosition);
            Vector2 canvasSizeDelta = _canvasRectTransform.sizeDelta;
            var proportionalPosition = new Vector2(viewportPosition.x * canvasSizeDelta.x, viewportPosition.y * canvasSizeDelta.y);

            _rectTransform.localPosition = proportionalPosition - canvasSizeDelta / 2 + Vector2.up * _offset;
        }
        
        public void Attach(Transform attachRenderer, float offset)
        {
            _attachedRenderer = attachRenderer;
            _offset = offset;
        }
    }
}