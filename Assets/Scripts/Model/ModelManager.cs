namespace AudioPlayer.Model
{
    /// <summary>
    /// 数据
    /// </summary>
    internal class ModelManager
    {
        private static ModelManager instance;

        internal static ModelManager Instance
        {
            get {
                if (instance == null)
                    instance = new ModelManager();
                return instance;
            }
        }

        #region 字段
        /// <summary>
        /// 逻辑数据
        /// </summary>
        private LogicDatas logicDatas;

        /// <summary>
        /// 场景数据
        /// </summary>
        private ScenesDatas scenesDatas;

        /// <summary>
        /// 参数
        /// </summary>
        private Parameter parameter;
        #endregion

        #region 属性
        /// <summary>
        /// 获取逻辑数据
        /// </summary>
        internal LogicDatas GetLogicDatas
        {
            set { this.logicDatas = value; }
            get { return this.logicDatas; }
        }

        /// <summary>
        /// 获取场景数据
        /// </summary>
        internal ScenesDatas GetScenesDatas
        {
            set { this.scenesDatas = value; }
            get { return this.scenesDatas; }
        }

        /// <summary>
        /// 获取参数
        /// </summary>
        internal Parameter GetParameter
        {
            set { this.parameter = value; }
            get { return this.parameter; }
        }
        #endregion
    }
}
