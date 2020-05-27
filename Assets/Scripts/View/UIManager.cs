using System.Collections.Generic;

namespace AudioPlayer.View
{
    /// <summary>
    /// ui管理器
    /// </summary>
    internal class UIManager
    {
        private static UIManager instance;

        internal static UIManager Instance
        {
            get {
                if (instance == null)
                    instance = new UIManager();
                return instance;
            }
        }

        /// <summary>
        /// UI控件集合
        /// </summary>
        private Dictionary<string, BasePanel> basePanels;

        /// <summary>
        /// 注册UI面板
        /// </summary>
        /// <param name="basePanelName">UI面板名称</param>
        /// <param name="basePanel">BasePanel</param>
        internal void RegisterBasePanel(string basePanelName, BasePanel basePanel)
        {
            if (this.basePanels == null)
                this.basePanels = new Dictionary<string, BasePanel>();
            if (!this.basePanels.ContainsKey(basePanelName))
                this.basePanels.Add(basePanelName, basePanel);
        }

        /// <summary>
        /// 取消注册UI面板
        /// </summary>
        /// <param name="basePanelName">UI面板名称</param>
        internal void UnRegisterBasePanel(string basePanelName)
        {
            if (this.basePanels == null)
                return;
            if (this.basePanels.ContainsKey(basePanelName))
                this.basePanels.Remove(basePanelName);
        }

        /// <summary>
        /// 指定的面板是否打开
        /// </summary>
        /// <param name="panelName">指定的面板名称</param>
        /// <returns></returns>
        internal bool IsOpenPanel(string panelName)
        {
            if (!this.basePanels.ContainsKey(panelName))
                return false;
            return this.basePanels[panelName].gameObject.activeSelf;
        }

        /// <summary>
        /// 打开指定的面板
        /// </summary>
        /// <param name="panelName">指定的面板名称</param>
        internal void OpenPanel(string panelName)
        {
            if (this.basePanels.ContainsKey(panelName))
                this.basePanels[panelName].OnShow();
        }

        /// <summary>
        /// 关闭指定的面板
        /// </summary>
        /// <param name="panelName">指定的面板</param>
        internal void ClosePanel(string panelName)
        {
            if (this.basePanels.ContainsKey(panelName))
                this.basePanels[panelName].OnHide();
        }
    }
}