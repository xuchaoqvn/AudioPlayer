using UnityEngine;

namespace AudioPlayer.Model
{
    /// <summary>
    /// 参数
    /// </summary>
    internal class Parameter
    {
        #region 字段
        /// <summary>
        /// 半径
        /// </summary>
        private float radius = 3.5f;

        /// <summary>
        /// x轴偏移
        /// </summary>
        private float offsetX = 0;

        /// <summary>
        /// y轴偏移
        /// </summary>
        private float offsetY = 0.7f;

        /// <summary>
        /// 可视化数据数量
        /// </summary>
        private int audioDataCount = 64;

        /// <summary>
        /// 角度间隔
        /// </summary>
        private float angleInterval = (2 * Mathf.PI) / 64;

        /// <summary>
        /// 线宽
        /// </summary>
        private float lineWidth = 0.02f;

        /// <summary>
        /// 最小高度
        /// </summary>
        private float minHight = 0.14f;

        /// <summary>
        /// 最大高度
        /// </summary>
        private float maxHight = 1.4f;

        /// <summary>
        /// 亮度
        /// </summary>
        private float colorBrightness = 3.5f;

        /// <summary>
        /// 主颜色
        /// </summary>
        private Color mainColor = Color.blue;

        /// <summary>
        /// 次要颜色
        /// </summary>
        private Color viceColor = Color.green;

        /// <summary>
        /// 更新间隔
        /// </summary>
        private float updateInterval = 0.08f;

        /// <summary>
        /// 放大因子
        /// </summary>
        private float amplificationFactor = 14f;

        /// <summary>
        /// 旋转速度
        /// </summary>
        private float rotateSpeed = 10.0f;

        /// <summary>
        /// 基础字体颜色
        /// </summary>
        private Color baseFontColor;

        /// <summary>
        /// 当前字体颜色
        /// </summary>
        private Color currentFontColor;

        /// <summary>
        /// 字体大小
        /// </summary>
        private int fontSize;
        #endregion

        #region 属性
        /// <summary>
        /// 半径
        /// </summary>
        internal float Radius
        {
            set { this.radius = value; }
            get { return this.radius; }
        }

        /// <summary>
        /// 半径
        /// </summary>
        internal float OffsetX
        {
            set { this.offsetX = value; }
            get { return this.offsetX; }
        }

        /// <summary>
        /// 半径
        /// </summary>
        internal float OffsetY
        {
            set { this.offsetY = value; }
            get { return this.offsetY; }
        }

        /// <summary>
        /// 可视化数据数量
        /// </summary>
        internal int AudioDataCount
        {
            set
            {
                this.audioDataCount = value;
                this.angleInterval = (2 * Mathf.PI) / audioDataCount;
            }
            get { return this.audioDataCount; }
        }

        /// <summary>
        /// 角度间隔
        /// </summary>
        internal float AngleInterval
        {
            get { return this.angleInterval; }
        }

        /// <summary>
        /// 线宽
        /// </summary>
        internal float LineWidth
        {
            set { this.lineWidth = value; }
            get { return this.lineWidth; }
        }

        /// <summary>
        /// 最小高度
        /// </summary>
        internal float MinHight
        {
            set { this.minHight = value; }
            get { return this.minHight; }
        }

        /// <summary>
        /// 最高高度
        /// </summary>
        internal float MaxHight
        {
            set { this.maxHight = value; }
            get { return this.maxHight; }
        }

        /// <summary>
        /// 亮度
        /// </summary>
        internal float ColorBrightness
        {
            set { this.colorBrightness = value; }
            get { return this.colorBrightness; }
        }

        /// <summary>
        /// 主颜色
        /// </summary>
        internal Color MainColor
        {
            set { this.mainColor = value; }
            get { return this.mainColor; }
        }

        /// <summary>
        /// 次要颜色
        /// </summary>
        internal Color ViceColor
        {
            set { this.viceColor = value; }
            get { return this.viceColor; }
        }

        /// <summary>
        /// 更新间隔
        /// </summary>
        internal float UpdateInterval
        {
            set { this.updateInterval = value; }
            get { return this.updateInterval; }
        }

        /// <summary>
        /// 放大因子
        /// </summary>
        internal float AmplificationFactor
        {
            set { this.amplificationFactor = value; }
            get { return this.amplificationFactor; }
        }

        /// <summary>
        /// 旋转速度
        /// </summary>
        internal float RotateSpeed
        {
            set { this.rotateSpeed = value; }
            get { return this.rotateSpeed; }
        }

        /// <summary>
        /// 基础字体颜色
        /// </summary>
        internal Color BaseFontColor
        {
            set { this.baseFontColor = value; }
            get { return this.baseFontColor; }
        }

        /// <summary>
        /// 当前字体颜色
        /// </summary>
        internal Color CurrentFontColor
        {
            set { this.currentFontColor = value; }
            get { return this.currentFontColor; }
        }
        
        /// <summary>
        /// 当前字体颜色
        /// </summary>
        internal int FontSize
        {
            set { this.fontSize = value; }
            get { return this.fontSize; }
        }
        #endregion
    }
}