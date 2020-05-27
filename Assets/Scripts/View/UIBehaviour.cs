using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace AudioPlayer.View
{
    internal class UIBehaviour : MonoBehaviour
    {
        #region MonoBehaviour函数
        private void Awake()
        {
            BasePanel basePanel = this.GetComponentInParent<BasePanel>();
            basePanel.RegisterUIControl(this.gameObject.name, this);
        }

        private void OnDestroy()
        {
            BasePanel basePanel = this.GetComponentInParent<BasePanel>();
            basePanel?.UnRegisterUIControl(this.gameObject.name);
        }
        #endregion

        #region UI注册事件函数
        /// <summary>
        /// 当按钮点击时
        /// </summary>
        /// <param name="unityAction">响应事件</param>
        internal void OnButtonClick(UnityAction unityAction)
        {
            if (unityAction != null)
            {
                Button button = GetComponent<Button>();
                if (button)
                    button.onClick.AddListener(unityAction);
                else
                    Debug.LogError("Button组件不存在");
            }
        }

        /// <summary>
        /// 当单选框勾选时
        /// </summary>
        /// <param name="unityAction">相应事件</param>
        internal void OnToggleValueChange(UnityAction<bool> unityAction)
        {
            if (unityAction != null)
            {
                Toggle toggle = GetComponent<Toggle>();
                if (toggle)
                    toggle.onValueChanged.AddListener(unityAction);
                else
                    Debug.LogError("Toggle组件不存在");
            }
        }

        /// <summary>
        /// 当滑动条滑动时
        /// </summary>
        /// <param name="unityAction">响应事件</param>
        internal void OnSliderValueChange(UnityAction<float> unityAction)
        {
            if (unityAction != null)
            {
                Slider slider = GetComponent<Slider>();
                if (slider)
                    slider.onValueChanged.AddListener(unityAction);
                else
                    Debug.LogError("Slide组件不存在");
            }
        }

        /// <summary>
        /// 当进度条更新进度时
        /// </summary>
        /// <param name="unityAction">响应事件</param>
        internal void OnScrollbarValueChange(UnityAction<float> unityAction)
        {
            if (unityAction != null)
            {
                Scrollbar scrollbar = GetComponent<Scrollbar>();
                if (scrollbar)
                    scrollbar.onValueChanged.AddListener(unityAction);
                else
                    Debug.LogError("Scrollbar组件不存在");
            }
        }

        /// <summary>
        /// 当下拉框被选择时
        /// </summary>
        /// <param name="unityAction">响应事件</param>
        internal void OnDropdownValueChange(UnityAction<int> unityAction)
        {
            if (unityAction != null)
            {
                Dropdown dropdown = GetComponent<Dropdown>();
                if (dropdown)
                    dropdown.onValueChanged.AddListener(unityAction);
                else
                    Debug.LogError("Dropdown组件不存在");
            }
        }

        /// <summary>
        /// 当输入框的内容改变时
        /// </summary>
        /// <param name="unityAction">响应事件</param>
        internal void OnInputFieldValueChange(UnityAction<string> unityAction)
        {
            if (unityAction != null)
            {
                InputField inputField = GetComponent<InputField>();
                if (inputField)
                    inputField.onValueChanged.AddListener(unityAction);
                else
                    Debug.LogError("InputField组件不存在");
            }
        }

        /// <summary>
        /// 当输入框结束编辑时
        /// </summary>
        /// <param name="unityAction">响应事件</param>
        internal void OnInputFieldEndEdit(UnityAction<string> unityAction)
        {
            if (unityAction != null)
            {
                InputField inputField = GetComponent<InputField>();
                if (inputField)
                    inputField.onEndEdit.AddListener(unityAction);
                else
                    Debug.LogError("InputField组件不存在");
            }
        }

        /// <summary>
        /// 当ScrollView滚动时
        /// </summary>
        /// <param name="unityAction">响应事件</param>
        internal void OnScrollViewValueChange(UnityAction<Vector2> unityAction)
        {
            if (unityAction != null)
            {
                ScrollRect scrollRect = GetComponent<ScrollRect>();
                if (scrollRect)
                    scrollRect.onValueChanged.AddListener(unityAction);
                else
                    Debug.LogError("ScrollView组件不存在");
            }
        }

        /// <summary>
        /// EventTrigger事件
        /// </summary>
        /// <param name="eventTriggerType">事件类型</param>
        /// <param name="unityAction">响应事件</param>
        internal void OnEventTrigger(EventTriggerType eventTriggerType, UnityAction<BaseEventData> unityAction)
        {
            if (unityAction != null)
            {
                EventTrigger eventTrigger = GetComponent<EventTrigger>();
                if (eventTrigger)
                {
                    EventTrigger.Entry entry = new EventTrigger.Entry
                    {
                        eventID = eventTriggerType
                    };
                    entry.callback.AddListener(unityAction);
                    eventTrigger.triggers.Add(entry);
                }
                else
                    Debug.LogError("EventTrigger组件不存在");
            }
        }
        #endregion

        #region 获取UI组件
        /// <summary>
        /// 获取文本组件
        /// </summary>
        /// <returns></returns>
        internal Text GetText()
        {
            Text text = GetComponent<Text>();
            if (text)
                return text;
            else
                return null;
        }

        /// <summary>
        /// 获取Image组件
        /// </summary>
        /// <returns></returns>
        internal Image GetImage()
        {
            Image image = GetComponent<Image>();
            if (image)
                return image;
            else
                return null;
        }

        /// <summary>
        /// 获取InputField组件
        /// </summary>
        /// <returns></returns>
        internal InputField GetInputField()
        {
            InputField inputField = GetComponent<InputField>();
            if (inputField)
                return inputField;
            else
                return null;
        }

        /// <summary>
        /// 获取Slider组件
        /// </summary>
        /// <returns></returns>
        internal Slider GetSlider()
        {
            Slider slider = GetComponent<Slider>();
            if (slider)
                return slider;
            else
                return null;
        }
        #endregion
    }
}