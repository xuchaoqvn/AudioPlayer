using System;
using UnityEngine;

namespace AudioPlayer.Controller
{
    internal class ControllerManager : MonoBehaviour
    {
        #region 简单单例
        private static ControllerManager instance;

        internal static ControllerManager Instance => ControllerManager.instance;

        void Awake()
        {
            ControllerManager.instance = this;
        }
        #endregion

        /// <summary>
        /// 刷新
        /// </summary>
        private Action OnUpdata;

        void Start()
        {
            this.Init();
        }

        void Update()
        {
            this.OnUpdata?.Invoke();
        }

        void OnDestroy()
        {
            this.Clear();
        }

        /// <summary>
        /// 注册需更新事件
        /// </summary>
        /// <param name="action"></param>
        internal void RegisterOnUpdate(Action action)
        {
            this.OnUpdata += action;
        }

        /// <summary>
        /// 取消注册更新事件
        /// </summary>
        /// <param name="action"></param>
        internal void UnRegisterOnUpdate(Action action)
        {
            this.OnUpdata -= action;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        internal void Init()
        {
            InitializeAndClear.Initialize();
        }

        /// <summary>
        /// 清空
        /// </summary>
        internal void Clear()
        {
            InitializeAndClear.Clear();
        }
    }
}