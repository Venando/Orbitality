﻿using UnityEngine;
using UnityEngine.EventSystems;

namespace Misc
{
    public static class InputManager
    {
        public static bool IsClick()
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return false;

#if UNITY_EDITOR || UNITY_STANDALONE
            return Input.GetMouseButtonDown(0);
#else
            return Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began;
#endif
        }
    }
}