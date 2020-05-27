using AudioPlayer.Controller;
using UnityEngine.UI;

namespace AudioPlayer.View
{
    internal class SetingPanel : BasePanel
    {
        /// <summary>
        /// 是否应用
        /// </summary>
        private bool isApply = false;

        internal override void Start()
        {
            base.Start();
            UIManager.Instance.ClosePanel(this.gameObject.name);
        }

        internal override void OnShow()
        {
            this.isApply = false;
            UISettingControl.Synchronous(this.uiControls);
            UISettingControl.Cache();
            base.OnShow();
        }

        internal override void OnHide()
        {
            if (this.uiControls.ContainsKey("MainColorInputField"))
                this.uiControls["MainColorInputField"].GetInputField().text = string.Empty;
            if (this.uiControls.ContainsKey("ViceColorInputField"))
                this.uiControls["ViceColorInputField"].GetInputField().text = string.Empty;
            if (this.uiControls.ContainsKey("BaseFontColorInputField"))
                this.uiControls["BaseFontColorInputField"].GetInputField().text = string.Empty;
            if (this.uiControls.ContainsKey("CurrentFontColorInputField"))
                this.uiControls["CurrentFontColorInputField"].GetInputField().text = string.Empty;
            this.isApply = false;
            base.OnHide();
        }

        /// <summary>
        /// 注册UI控件响应或被响应事件
        /// </summary>
        /// <param name="uiControlName">UI控件名称</param>
        /// <param name="uIBehaviour">UI控件</param>
        internal override void RegisterUIControlEvent(string uiControlName, UIBehaviour uIBehaviour)
        {
            switch (uiControlName)
            {
                #region 标题
                case "ExitButton":
                    uIBehaviour.OnButtonClick(new UnityEngine.Events.UnityAction(() => {
                        if (!this.isApply)
                            UISettingControl.Cancel();
                        if (this.uiControls.ContainsKey("Scroll View"))
                            this.uiControls["Scroll View"].GetComponent<ScrollRect>().verticalNormalizedPosition = 1.0f;
                        UIManager.Instance.ClosePanel(this.gameObject.name);
                    }));
                    break;
                #endregion

                #region List
                case "VisualizationToggle":
                    uIBehaviour.OnToggleValueChange(new UnityEngine.Events.UnityAction<bool>((isOn => {
                        if (!isOn)
                            return;
                        if (!this.uiControls.ContainsKey("Scroll View"))
                            return;
                        this.uiControls["Scroll View"].GetComponent<ScrollRect>().verticalNormalizedPosition = 1.0f;
                    })));
                    break;
                case "LyricToggle":
                    uIBehaviour.OnToggleValueChange(new UnityEngine.Events.UnityAction<bool>((isOn => {
                        if (!isOn)
                            return;
                        if (!this.uiControls.ContainsKey("Scroll View"))
                            return;
                        this.uiControls["Scroll View"].GetComponent<ScrollRect>().verticalNormalizedPosition = 0.38f;
                    })));
                    break;
                case "AboutToggle":
                    uIBehaviour.OnToggleValueChange(new UnityEngine.Events.UnityAction<bool>((isOn => {
                        if (!isOn)
                            return;
                        if (!this.uiControls.ContainsKey("Scroll View"))
                            return;
                        this.uiControls["Scroll View"].GetComponent<ScrollRect>().verticalNormalizedPosition = 0.0f;
                    })));
                    break;
                #endregion

                #region 内容
                //可视化设置
                case "RadiusSlider":
                    this.RegisterUIControlEvent(uIBehaviour, 2.8f, 4.2f, "RadiusValueText", "RadiusSlider");
                    break;
                case "OffsetXSlider":
                    this.RegisterUIControlEvent(uIBehaviour, -5.6f, 5.6f, "OffsetXValueText", "OffsetXSlider");
                    break;
                case "OffsetYSlider":
                    this.RegisterUIControlEvent(uIBehaviour, -1.4f, 1.4f, "OffsetYValueText", "OffsetYSlider");
                    break;
                case "AudioDataCountSlider":
                    uIBehaviour.OnSliderValueChange(new UnityEngine.Events.UnityAction<float>((value) => {
                        value = UnityEngine.Mathf.Clamp(value, 0, 3);
                        value = UnityEngine.Mathf.Pow(2, (6 + value));
                        if (this.uiControls.ContainsKey("AudioDataCountValueText"))
                            this.uiControls["AudioDataCountValueText"].GetText().text = value.ToString();
                        //控制层更新数据
                        UISettingControl.OnSettingValueChange(uiControlName, value);
                    }));
                    break;
                case "LineWidthSlider":
                    this.RegisterUIControlEvent(uIBehaviour, 0.02f, 0.1f, "LineWidthValueText", "LineWidthSlider");
                    break;
                case "MinHightSlider":
                    this.RegisterUIControlEvent(uIBehaviour, 0.14f, 0.42f, "MinHightValueText", "MinHightSlider");
                    break;
                case "MaxHightSlider":
                    this.RegisterUIControlEvent(uIBehaviour, 1.4f, 2.1f, "MaxHightValueText", "MaxHightSlider");
                    break;
                case "ColorBrightnessSlider":
                    this.RegisterUIControlEvent(uIBehaviour, 2.1f, 5.0f, "ColorBrightnessValueText", "ColorBrightnessSlider");
                    break;
                case "MainColorInputField":
                    this.RegisterUIControlEvent(uIBehaviour, "MainColorImage", "MainColorInputField");
                    break;
                case "ViceColorInputField":
                    this.RegisterUIControlEvent(uIBehaviour, "ViceColorImage", "ViceColorInputField");
                    break;
                case "UpdateIntervalSlider":
                    this.RegisterUIControlEvent(uIBehaviour, 0.02f, 0.3f, "UpdateIntervalValueText", "UpdateIntervalSlider");
                    break;
                case "AmplificationFactorSlider":
                    this.RegisterUIControlEvent(uIBehaviour, 7f, 28f, "AmplificationFactorValueText", "AmplificationFactorSlider");
                    break;
                case "RotateSpeedSlider":
                    this.RegisterUIControlEvent(uIBehaviour, -30f, 30f, "RotateSpeedValueText", "RotateSpeedSlider");
                    break;

                //歌词设置
                case "BaseFontColorInputField":
                    this.RegisterUIControlEvent(uIBehaviour, "BaseFontColorImage", "BaseFontColorInputField");
                    break;
                case "CurrentFontColorInputField":
                    this.RegisterUIControlEvent(uIBehaviour, "CurrentFontColorImage", "CurrentFontColorInputField");
                    break;
                case "FontSizeSlider":
                    this.RegisterUIControlEvent(uIBehaviour, 14f, 30f, "FontSizeValueText","FontSizeSlider");
                    break;
                #endregion

                #region 结尾
                case "Reset":
                    uIBehaviour.OnButtonClick(new UnityEngine.Events.UnityAction(() => 
                        UISettingControl.Reset(this.uiControls)
                    ));
                    break;
                case "Confirm":
                    uIBehaviour.OnButtonClick(new UnityEngine.Events.UnityAction(() => {
                        UISettingControl.Confirm();
                        if (this.uiControls.ContainsKey("Scroll View"))
                            this.uiControls["Scroll View"].GetComponent<ScrollRect>().verticalNormalizedPosition = 1.0f;
                        UIManager.Instance.ClosePanel(this.gameObject.name);
                    }));
                    break;
                case "Cancel":
                    uIBehaviour.OnButtonClick(new UnityEngine.Events.UnityAction(() => {
                        UISettingControl.Cancel();
                        if (this.uiControls.ContainsKey("Scroll View"))
                            this.uiControls["Scroll View"].GetComponent<ScrollRect>().verticalNormalizedPosition = 1.0f;
                        UIManager.Instance.ClosePanel(this.gameObject.name);
                    }));
                    break;
                case "Apply":
                    uIBehaviour.OnButtonClick(new UnityEngine.Events.UnityAction(() => {
                        UISettingControl.Apply();
                        this.isApply = true;
                    }));
                    break;
                #endregion
                default:
                    break;
            }
        }

        /// <summary>
        /// 注册UI事件
        /// </summary>
        /// <param name="uIBehaviour">UIBehaviour</param>
        /// <param name="minValue">最小值</param>
        /// <param name="maxValue">最大值</param>
        /// <param name="textValueName">显示文本</param>
        private void RegisterUIControlEvent(UIBehaviour uIBehaviour,float minValue,float maxValue,string textValueName,string uiControlName)
        {
            uIBehaviour.OnSliderValueChange(new UnityEngine.Events.UnityAction<float>((value) => {
                value = UnityEngine.Mathf.Clamp(value, minValue, maxValue);
                if (this.uiControls.ContainsKey(textValueName))
                    this.uiControls[textValueName].GetText().text = value.ToString();
                //控制层更新数据
                UISettingControl.OnSettingValueChange(uiControlName,value);
            }));
        }

        /// <summary>
        /// 注册UI事件
        /// </summary>
        /// <param name="uIBehaviour">UIBehaviour</param>
        /// <param name="imageName">显示图片</param>
        private void RegisterUIControlEvent(UIBehaviour uIBehaviour, string imageName, string uiControlName)
        {
            uIBehaviour.OnInputFieldEndEdit(new UnityEngine.Events.UnityAction<string>((content) => {
                if (UnityEngine.ColorUtility.TryParseHtmlString(content, out UnityEngine.Color colorValue))
                {
                    if (this.uiControls.ContainsKey(imageName))
                        this.uiControls[imageName].GetImage().color = colorValue;
                    //控制层更新数据
                    UISettingControl.OnSettingValueChange(uiControlName, colorValue);
                }
            }));
        }
    }
}