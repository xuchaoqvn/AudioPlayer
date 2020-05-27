using AudioPlayer.Model;
using System.Collections.Generic;
using UnityEngine;

namespace AudioPlayer.Controller
{
    internal static class UISettingControl
    {
        //可视化设置
        private const string RadiusSlider = "RadiusSlider";
        private const string OffsetXSlider = "OffsetXSlider";
        private const string OffsetYSlider = "OffsetYSlider";
        private const string AudioDataCountSlider = "AudioDataCountSlider";
        private const string LineWidthSlider = "LineWidthSlider";
        private const string MinHightSlider = "MinHightSlider";
        private const string MaxHightSlider = "MaxHightSlider";
        private const string ColorBrightnessSlider = "ColorBrightnessSlider";
        private const string MainColorInputField = "MainColorInputField";
        private const string ViceColorInputField = "ViceColorInputField";
        private const string UpdateIntervalSlider = "UpdateIntervalSlider";
        private const string AmplificationFactorSlider = "AmplificationFactorSlider";
        private const string RotateSpeedSlider = "RotateSpeedSlider";

        //歌词设置
        private const string BaseFontColorInputField = "BaseFontColorInputField";
        private const string CurrentFontColorInputField = "CurrentFontColorInputField";
        private const string FontSizeSlider = "FontSizeSlider";


        //Shader
        private const string _MainColor = "_MainColor";
        private const string _ViceColor = "_ViceColor";
        private const string _Brightness = "_Brightness";

        //可视化设置缓存
        private static float radiusSliderCache;
        private static float offsetXSliderCache;
        private static float offsetYSliderCache;
        private static int audioDataCountSliderCache;
        private static float lineWidthSliderCache;
        private static float minHightSliderCache;
        private static float maxHightSliderCache;
        private static float colorBrightnessSliderCache;
        private static Color mainColorInputFieldCache;
        private static Color viceColorInputFieldCache;
        private static float updateIntervalSliderCache;
        private static float amplificationFactorSliderCache;
        private static float rotateSpeedSliderCache;

        //歌词设置缓存
        private static Color baseFontColorInputFieldCache;
        private static Color currentFontColorInputFieldCache;
        private static int fontSizeSliderCache;

        /// <summary>
        /// 当设置面板数值变动时
        /// </summary>
        /// <param name="attributeName">属性名称</param>
        /// <param name="value">值</param>
        internal static void OnSettingValueChange(string attributeName, float value)
        {
            Parameter parameter = ModelManager.Instance.GetParameter;
            switch (attributeName)
            {
                case RadiusSlider:
                    parameter.Radius = value;
                    break;
                case OffsetXSlider:
                    parameter.OffsetX = value;
                    break;
                case OffsetYSlider:
                    parameter.OffsetY = value;
                    break;
                case AudioDataCountSlider:
                    parameter.AudioDataCount = (int)value;
                    break;
                case LineWidthSlider:
                    parameter.LineWidth = value;
                    break;
                case MinHightSlider:
                    parameter.MinHight = value;
                    break;
                case MaxHightSlider:
                    parameter.MaxHight = value;
                    break;
                case ColorBrightnessSlider:
                    {
                        parameter.ColorBrightness = value;
                        ModelManager.Instance.GetScenesDatas.AudioDataMaterial.SetFloat(_Brightness, value);
                    }
                    break;
                case UpdateIntervalSlider:
                    parameter.UpdateInterval = value;
                    break;
                case AmplificationFactorSlider:
                    parameter.AmplificationFactor = value;
                    break;
                case RotateSpeedSlider:
                    parameter.RotateSpeed = value;
                    break;
                case FontSizeSlider:
                    {
                        parameter.FontSize = (int)value;
                        UILyricControl.ChangeLyricFontSize((int)value, ModelManager.Instance.GetScenesDatas.LyricItems);
                    }
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 当设置面板数值变动时
        /// </summary>
        /// <param name="attributeName">属性名称</param>
        /// <param name="valueColor">颜色值</param>
        internal static void OnSettingValueChange(string attributeName, Color valueColor)
        {
            Parameter parameter = ModelManager.Instance.GetParameter;
            ScenesDatas scenesDatas = ModelManager.Instance.GetScenesDatas;
            switch (attributeName)
            {
                case MainColorInputField:
                    {
                        parameter.MainColor = valueColor;
                        scenesDatas.AudioDataMaterial.SetColor(_MainColor, valueColor);
                    }
                    break;
                case ViceColorInputField:
                    {
                        parameter.ViceColor = valueColor;
                        scenesDatas.AudioDataMaterial.SetColor(_ViceColor, valueColor);
                    }
                    break;
                case BaseFontColorInputField:
                    {
                        parameter.BaseFontColor = valueColor;
                        UILyricControl.ChangeBaseLyricColor(valueColor, scenesDatas.LyricItems);
                    }
                    break;
                case CurrentFontColorInputField:
                    {
                        parameter.CurrentFontColor = valueColor;
                        UILyricControl.ChangeCurrentLyricColor(valueColor, scenesDatas.LyricItems);
                    }
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 同步
        /// </summary>
        /// <param name="uiControls">UIBehaviour集合</param>
        internal static void Synchronous(Dictionary<string, AudioPlayer.View.UIBehaviour> uiControls)
        {
            Parameter parameter = ModelManager.Instance.GetParameter;
            if (uiControls.ContainsKey(RadiusSlider))
                uiControls[RadiusSlider].GetSlider().value = parameter.Radius;
            if (uiControls.ContainsKey(OffsetXSlider))
                uiControls[OffsetXSlider].GetSlider().value = parameter.OffsetX;
            if (uiControls.ContainsKey(OffsetYSlider))
                uiControls[OffsetYSlider].GetSlider().value = parameter.OffsetY;
            if (uiControls.ContainsKey(AudioDataCountSlider))
                //64 = 2的6次幂
                uiControls[AudioDataCountSlider].GetSlider().value = Mathf.Log(parameter.AudioDataCount, 2) - 6;
            if (uiControls.ContainsKey(LineWidthSlider))
                uiControls[LineWidthSlider].GetSlider().value = parameter.LineWidth;
            if (uiControls.ContainsKey(MinHightSlider))
                uiControls[MinHightSlider].GetSlider().value = parameter.MinHight;
            if (uiControls.ContainsKey(MaxHightSlider))
                uiControls[MaxHightSlider].GetSlider().value = parameter.MaxHight;
            if (uiControls.ContainsKey(ColorBrightnessSlider))
                uiControls[ColorBrightnessSlider].GetSlider().value = parameter.ColorBrightness;
            if (uiControls.ContainsKey(UpdateIntervalSlider))
                uiControls[UpdateIntervalSlider].GetSlider().value = parameter.UpdateInterval;
            if (uiControls.ContainsKey(AmplificationFactorSlider))
                uiControls[AmplificationFactorSlider].GetSlider().value = parameter.AmplificationFactor;
            if (uiControls.ContainsKey(RotateSpeedSlider))
                uiControls[RotateSpeedSlider].GetSlider().value = parameter.RotateSpeed;

            if (uiControls.ContainsKey(FontSizeSlider))
                uiControls[FontSizeSlider].GetSlider().value = parameter.FontSize;
        }

        /// <summary>
        /// 缓存
        /// </summary>
        internal static void Cache()
        {
            Parameter parameter = ModelManager.Instance.GetParameter;
            radiusSliderCache = parameter.Radius;
            offsetXSliderCache = parameter.OffsetX;
            offsetYSliderCache = parameter.OffsetY;
            audioDataCountSliderCache = parameter.AudioDataCount;
            lineWidthSliderCache = parameter.LineWidth;
            minHightSliderCache = parameter.MinHight;
            maxHightSliderCache = parameter.MaxHight;
            colorBrightnessSliderCache = parameter.ColorBrightness;
            mainColorInputFieldCache = parameter.MainColor;
            viceColorInputFieldCache = parameter.ViceColor;
            updateIntervalSliderCache = parameter.UpdateInterval;
            amplificationFactorSliderCache = parameter.AmplificationFactor;
            rotateSpeedSliderCache = parameter.RotateSpeed;

            baseFontColorInputFieldCache = parameter.BaseFontColor;
            currentFontColorInputFieldCache = parameter.CurrentFontColor;
            fontSizeSliderCache = parameter.FontSize;
        }

        /// <summary>
        /// 重置
        /// </summary>
        internal static void Reset(Dictionary<string, AudioPlayer.View.UIBehaviour> uiControls)
        {
            Parameter parameter = ModelManager.Instance.GetParameter;
            parameter.Radius = 3.5f;
            parameter.OffsetX = 0.0f;
            parameter.OffsetY = 0.7f;
            parameter.AudioDataCount = 64;
            parameter.LineWidth = 0.02f;
            parameter.MinHight = 0.14f;
            parameter.MaxHight = 1.4f;
            parameter.ColorBrightness = 3.4f;
            parameter.MainColor = Color.blue;
            parameter.ViceColor = Color.green;
            parameter.UpdateInterval = 0.08f;
            parameter.AmplificationFactor = 14.0f;
            parameter.RotateSpeed = 10.0f;

            parameter.BaseFontColor = new Color(50.0f / 255.0f, 50.0f / 255.0f, 50.0f / 255.0f, 1.0f);
            parameter.CurrentFontColor = new Color(163.0f / 255.0f, 1.0f, 6.0f / 255.0f, 255.0f);
            parameter.FontSize = 30;

            if (uiControls.ContainsKey(RadiusSlider))
                uiControls[RadiusSlider].GetSlider().value = parameter.Radius;
            if (uiControls.ContainsKey(OffsetXSlider))
                uiControls[OffsetXSlider].GetSlider().value = parameter.OffsetX;
            if (uiControls.ContainsKey(OffsetYSlider))
                uiControls[OffsetYSlider].GetSlider().value = parameter.OffsetY;
            if (uiControls.ContainsKey(AudioDataCountSlider))
                uiControls[AudioDataCountSlider].GetSlider().value = parameter.AudioDataCount % 64;
            if (uiControls.ContainsKey(LineWidthSlider))
                uiControls[LineWidthSlider].GetSlider().value = parameter.LineWidth;
            if (uiControls.ContainsKey(MinHightSlider))
                uiControls[MinHightSlider].GetSlider().value = parameter.MinHight;
            if (uiControls.ContainsKey(MaxHightSlider))
                uiControls[MaxHightSlider].GetSlider().value = parameter.MaxHight;
            if (uiControls.ContainsKey(ColorBrightnessSlider))
                uiControls[ColorBrightnessSlider].GetSlider().value = parameter.ColorBrightness;
            if (uiControls.ContainsKey(UpdateIntervalSlider))
                uiControls[UpdateIntervalSlider].GetSlider().value = parameter.UpdateInterval;
            if (uiControls.ContainsKey(AmplificationFactorSlider))
                uiControls[AmplificationFactorSlider].GetSlider().value = parameter.AmplificationFactor;
            if (uiControls.ContainsKey(RotateSpeedSlider))
                uiControls[RotateSpeedSlider].GetSlider().value = parameter.RotateSpeed;
            
            if (uiControls.ContainsKey(FontSizeSlider))
                uiControls[FontSizeSlider].GetSlider().value = parameter.FontSize;

            ScenesDatas scenesDatas = ModelManager.Instance.GetScenesDatas;
            scenesDatas.AudioDataMaterial.SetFloat(_Brightness, 3.4f);
            scenesDatas.AudioDataMaterial.SetColor(_MainColor, Color.blue);
            scenesDatas.AudioDataMaterial.SetColor(_ViceColor, Color.green);
            UILyricControl.ChangeBaseLyricColor(parameter.BaseFontColor, scenesDatas.LyricItems);
            UILyricControl.ChangeCurrentLyricColor(parameter.CurrentFontColor, scenesDatas.LyricItems);
            UILyricControl.ChangeLyricFontSize(parameter.FontSize, scenesDatas.LyricItems);
        }

        /// <summary>
        /// 确定
        /// </summary>
        internal static void Confirm()
        {
            ConfigControl.AlterConfig();
        }

        /// <summary>
        /// 取消
        /// </summary>
        internal static void Cancel()
        {
            Parameter parameter = ModelManager.Instance.GetParameter;
            ScenesDatas scenesDatas = ModelManager.Instance.GetScenesDatas;
            if (parameter.Radius != radiusSliderCache)
                parameter.Radius = radiusSliderCache;
            if (parameter.OffsetX != offsetXSliderCache)
                parameter.OffsetX = offsetXSliderCache;
            if (parameter.OffsetY != offsetYSliderCache)
                parameter.OffsetY = offsetYSliderCache;
            if (parameter.AudioDataCount != audioDataCountSliderCache)
                parameter.AudioDataCount = audioDataCountSliderCache;
            if (parameter.LineWidth != lineWidthSliderCache)
                parameter.LineWidth = lineWidthSliderCache;
            if (parameter.MinHight != minHightSliderCache)
                parameter.MinHight = minHightSliderCache;
            if (parameter.MaxHight != maxHightSliderCache)
                parameter.MaxHight = maxHightSliderCache;
            if (parameter.ColorBrightness != colorBrightnessSliderCache)
            {
                parameter.ColorBrightness = colorBrightnessSliderCache;
                scenesDatas.AudioDataMaterial.SetFloat(_Brightness, colorBrightnessSliderCache);
            }
            if (parameter.MainColor != mainColorInputFieldCache)
            {
                parameter.MainColor = mainColorInputFieldCache;
                scenesDatas.AudioDataMaterial.SetColor(_MainColor, mainColorInputFieldCache);
            }
            if (parameter.ViceColor != viceColorInputFieldCache)
            {
                parameter.ViceColor = viceColorInputFieldCache;
                scenesDatas.AudioDataMaterial.SetColor(_ViceColor, viceColorInputFieldCache);
            }
            if (parameter.UpdateInterval != updateIntervalSliderCache)
                parameter.UpdateInterval = updateIntervalSliderCache;
            if (parameter.AmplificationFactor != amplificationFactorSliderCache)
                parameter.AmplificationFactor = amplificationFactorSliderCache;
            if (parameter.RotateSpeed != rotateSpeedSliderCache)
                parameter.RotateSpeed = rotateSpeedSliderCache;
            
            if (parameter.BaseFontColor != baseFontColorInputFieldCache)
            {
                parameter.BaseFontColor = baseFontColorInputFieldCache;
                UILyricControl.ChangeBaseLyricColor(baseFontColorInputFieldCache, scenesDatas.LyricItems);
            }
            if (parameter.CurrentFontColor != currentFontColorInputFieldCache)
            {
                parameter.CurrentFontColor = currentFontColorInputFieldCache;
                UILyricControl.ChangeCurrentLyricColor(currentFontColorInputFieldCache, scenesDatas.LyricItems);
            }
            if (parameter.FontSize != fontSizeSliderCache)
            {
                parameter.FontSize = fontSizeSliderCache;
                UILyricControl.ChangeLyricFontSize(fontSizeSliderCache, scenesDatas.LyricItems);
            }
        }

        /// <summary>
        /// 应用
        /// </summary>
        internal static void Apply()
        {
            ConfigControl.AlterConfig();
        }
    }
}