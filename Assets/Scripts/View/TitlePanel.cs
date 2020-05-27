using System;
using System.Runtime.InteropServices;
using UnityEngine;

/*
 * 说明：关于窗体的设置为网上引用的代码；
 * 
 * 作者：https://www.jianshu.com/p/03310f18ef34
 */

namespace AudioPlayer.View
{
    /// <summary>
    /// 标题面板
    /// </summary>
    internal class TitlePanel : BasePanel
    {
        /// <summary>
        /// 设置当前窗口的显示状态
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="nCmdShow"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern bool ShowWindow(IntPtr hwnd, int nCmdShow);

        /// <summary>
        /// 获取当前激活窗口
        /// </summary>
        /// <returns></returns>
        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

        /// <summary>
        /// 设置窗口边框
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="_nIndex"></param>
        /// <param name="dwNewLong"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern IntPtr SetWindowLong(IntPtr hwnd, int _nIndex, int dwNewLong);
        
        /// <summary>
        /// 设置窗口位置，大小
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="hWndInsertAfter"></param>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <param name="cx"></param>
        /// <param name="cy"></param>
        /// <param name="uFlags"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern bool SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);
       
        /// <summary>
        /// 窗口拖动
        /// </summary>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);

        /// <summary>
        /// 最小化
        /// </summary>
        private const int SW_SHOWMINIMIZED = 2;

        /// <summary>
        /// 最大化
        /// </summary>
        private const int SW_SHOWMAXIMIZED = 3;

        /// <summary>
        /// 还原
        /// </summary>
        private const int SW_SHOWRESTORE = 1;
        
        //边框参数
        private const uint SWP_SHOWWINDOW = 0x0040;
        private const int GWL_STYLE = -16;
        //const int WS_BORDER = 1;
        private const int WS_POPUP = 0x800000;
        
        /// <summary>
        /// 是否全屏
        /// </summary>
        private bool isMax = false;

        /// <summary>
        /// 句柄
        /// </summary>
        private IntPtr currentWindow;

        /// <summary>
        /// 矩形
        /// </summary>
        private Rect rect;

        /// <summary>
        /// 设置无边框，并设置框体大小，位置
        /// </summary>
        /// <param name="rect"></param>
        internal static void SetNoFrameWindow(Rect rect)
        {
            SetWindowLong(GetForegroundWindow(), GWL_STYLE, WS_POPUP);
            bool result = SetWindowPos(GetForegroundWindow(), 0, (int)rect.x, (int)rect.y, (int)rect.width, (int)rect.height, SWP_SHOWWINDOW);
        }

        /// <summary>
        /// 拖动窗口
        /// </summary>
        /// <param name="window"></param>
        internal static void DragWindow(IntPtr window)
        {
            ReleaseCapture();
            SendMessage(window, 0xA1, 0x02, 0);
            SendMessage(window, 0x0202, 0, 0);
        }

        internal override void Start()
        {
            base.Start();
            this.currentWindow = GetForegroundWindow();

            //窗口大小  以此为例
            float windowWidth = 1200;
            float windowHeight = 740;
            //计算框体显示位置
            float posX = (Screen.currentResolution.width - windowWidth) / 2;
            float posY = (Screen.currentResolution.height - windowHeight) / 2;
            this.rect = new Rect(posX, posY, windowWidth, windowHeight);
            SetNoFrameWindow(this.rect);
        }

        internal override void RegisterUIControlEvent(string uiControlsName, UIBehaviour uIBehaviour)
        {
            switch (uiControlsName)
            {
                case "Background":
                    uIBehaviour.OnEventTrigger(UnityEngine.EventSystems.EventTriggerType.Drag,
                        new UnityEngine.Events.UnityAction<UnityEngine.EventSystems.BaseEventData>((baseEvent) => 
                            DragWindow(this.currentWindow)
                        ));
                    break;
                case "Seting":
                    uIBehaviour.OnButtonClick(new UnityEngine.Events.UnityAction(() =>
                    UIManager.Instance.OpenPanel("SetingPanel")
                    ));
                    break;
                case "Minimize":
                    uIBehaviour.OnButtonClick(new UnityEngine.Events.UnityAction(() => {
                        //最小化 
                        ShowWindow(GetForegroundWindow(), SW_SHOWMINIMIZED);
                    }));
                    break;
                case "Maximize":
                    uIBehaviour.OnButtonClick(new UnityEngine.Events.UnityAction(() => {
                        if (isMax)
                        {
                            //还原
                            ShowWindow(GetForegroundWindow(), SW_SHOWRESTORE);
                            this.isMax = false;
                        }
                        else
                        {
                            //最大化
                            ShowWindow(GetForegroundWindow(), SW_SHOWMAXIMIZED);
                            this.isMax = true;
                        }
                    }));
                    break;
                case "Close":
                    uIBehaviour.OnButtonClick(new UnityEngine.Events.UnityAction(() => {
                        if (UnityEngine.Application.isEditor)
                            return;
                        UnityEngine.Application.Quit();
                    }));
                    break;
                default:
                    break;
            }
        }
    }
}