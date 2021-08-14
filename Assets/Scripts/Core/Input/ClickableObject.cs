using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace BestGameEver
{
    /// <summary>
    /// @author Rivenort
    /// </summary>
    public class ClickableObject : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public Action Func;
        public bool disabled;
        private GameObject m_lastObject; // for desktop
        private Vector2 m_lastPos; //for Handheld

        public UnityEvent onClick;

        public void OnClick()
        {
            if (Func != null)
                Func();
            onClick?.Invoke();
        }

        /*
        public void OnPointerClick(PointerEventData eventData)
        {
            if (Func != null && !disabled)
                Func();
        }*/

        public void OnPointerDown(PointerEventData eventData)
        {
            m_lastObject = this.gameObject;
            m_lastPos = eventData.position;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (SystemInfo.deviceType == DeviceType.Desktop)
            {
                if (m_lastObject == gameObject)
                {
                    if (!disabled)
                        OnClick();
                }
            }
            else if (SystemInfo.deviceType == DeviceType.Handheld)
            {
                if (m_lastPos == eventData.position)
                {
                    if (!disabled)
                        OnClick();
                }
            }
        }
    }
}
