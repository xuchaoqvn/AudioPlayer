using System.Collections.Generic;
using UnityEngine;

namespace AudioPlayer.View
{
    internal class BasePanel : MonoBehaviour
    {
        /// <summary>
        /// 基本面板
        /// </summary>
        internal virtual void Start()
        {
            UIManager.Instance.RegisterBasePanel(this.gameObject.name,this);
        }

        internal virtual void OnDestroy()
        {
            UIManager.Instance.UnRegisterBasePanel(this.gameObject.name);
        }

        /// <summary>
        /// CanvasGroup
        /// </summary>
        private CanvasGroup canvasGroup;

        /// <summary>
        /// UI控件集合
        /// </summary>
        protected Dictionary<string, UIBehaviour> uiControls;

        /// <summary>
        /// CanvasGroup
        /// </summary>
        protected CanvasGroup CanvasGroup
        {
            get
            {
                if (this.canvasGroup == null)
                    this.canvasGroup = this.gameObject.AddComponent<CanvasGroup>();
                return this.canvasGroup;
            }
        }

        /// <summary>
        /// 注册UI控件
        /// </summary>
        /// <param name="uiControlsName">UI控件名称</param>
        /// <param name="uIBehaviour">UIBehaviour</param>
        internal void RegisterUIControl(string uiControlsName, UIBehaviour uIBehaviour)
        {
            if (this.uiControls == null)
                this.uiControls = new Dictionary<string, UIBehaviour>();
            if (!this.uiControls.ContainsKey(uiControlsName))
            {
                this.uiControls.Add(uiControlsName, uIBehaviour);
                this.RegisterUIControlEvent(uiControlsName, uIBehaviour);
            }
        }

        /// <summary>
        /// 取消注册UI控件
        /// </summary>
        /// <param name="uiControlsName">UI控件名称</param>
        internal void UnRegisterUIControl(string uiControlsName)
        {
            if (this.uiControls == null)
                return;
            if (this.uiControls.ContainsKey(uiControlsName))
                this.uiControls.Remove(uiControlsName);
        }

        /// <summary>
        /// 注册UI控件响应或被响应事件
        /// </summary>
        /// <param name="uiControlsName">UI控件名称</param>
        /// <param name="uIBehaviour">UI控件</param>
        internal virtual void RegisterUIControlEvent(string uiControlsName, UIBehaviour uIBehaviour) { }

        /// <summary>
        /// 初始化
        /// </summary>
        internal virtual void OnInit() { }

        /// <summary>
        /// 显示
        /// </summary>
        internal virtual void OnShow()
        {
            this.gameObject.SetActive(true);
        }

        /// <summary>
        /// 暂停
        /// </summary>
        internal virtual void OnPause()
        {
            this.CanvasGroup.interactable = false;
            this.CanvasGroup.blocksRaycasts = false;
        }

        /// <summary>
        /// 继续
        /// </summary>
        internal virtual void OnResume()
        {
            this.CanvasGroup.interactable = true;
            this.CanvasGroup.blocksRaycasts = true;
        }

        /// <summary>
        /// 隐藏
        /// </summary>
        internal virtual void OnHide()
        {
            this.gameObject.SetActive(false);
        }
    }
}