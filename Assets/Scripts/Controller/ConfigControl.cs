using AudioPlayer.Model;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using UnityEngine;

namespace AudioPlayer.Controller
{
    /// <summary>
    /// 配置文件
    /// </summary>
    internal static class ConfigControl
    {
        private const string configPath = "/Configs/config.xml";

        /// <summary>
        /// 加载解析配置文件
        /// </summary>
        /// <returns></returns>
        internal static Parameter LoadConfig()
        {
            Parameter parameter = new Parameter();
            string xmlurl = string.Concat("file:///", Application.streamingAssetsPath, configPath);
            XElement xElement = XElement.Load(xmlurl);
            //item节点集合
            IEnumerable<XElement> items = xElement.Elements("item");
            int length = items.Count();
            XAttribute xAttributeName;
            XAttribute xAttributeValue;
            string name = string.Empty;
            string value = string.Empty;
            for (int i = 0; i < length; i++)
            {
                xAttributeName = items.ElementAt(i).Attribute("name");
                if (xAttributeName == null)
                    continue;
                name = xAttributeName.Value;
                xAttributeValue = items.ElementAt(i).Attribute("value");
                if (xAttributeValue == null)
                    continue;
                value = xAttributeValue.Value;
                switch (name)
                {
                    case "radius":
                        {
                            if (float.TryParse(value, out float radius))
                            {
                                radius = Mathf.Clamp(radius, 2.8f, 4.2f);
                                parameter.Radius = radius;
                            }
                            else
                                parameter.Radius = 3.5f;
                        }
                        break;
                    case "offsetX":
                        {
                            if (float.TryParse(value, out float offsetX))
                            {
                                offsetX = Mathf.Clamp(offsetX, -5.6f, 5.6f);
                                parameter.OffsetX = offsetX;
                            }
                            else
                                parameter.OffsetX = 0.0f;
                        }
                        break;
                    case "offsetY":
                        {
                            if (float.TryParse(value, out float offsetY))
                            {
                                offsetY = Mathf.Clamp(offsetY, -1.4f, 1.4f);
                                parameter.OffsetY = offsetY;
                            }
                            else
                                parameter.OffsetY = 0.7f;
                        }
                        break;
                    case "audioDataCount":
                        {
                            if (int.TryParse(value, out int audioDataCount))
                            {
                                audioDataCount = Mathf.Clamp(audioDataCount, 64, 521);
                                parameter.AudioDataCount = audioDataCount;
                            }
                            else
                                parameter.AudioDataCount = 64;
                        }
                        break;
                    case "lineWidth":
                        {
                            if (float.TryParse(value, out float lineWidth))
                            {
                                lineWidth = Mathf.Clamp(lineWidth, 0.02f, 0.1f);
                                parameter.LineWidth = lineWidth;
                            }
                            else
                                parameter.LineWidth = 0.02f;
                        }
                        break;
                    case "minHight":
                        {
                            if (float.TryParse(value, out float minHight))
                            {
                                minHight = Mathf.Clamp(minHight, 0.14f, 0.42f);
                                parameter.MinHight = minHight;
                            }
                            else
                                parameter.MinHight = 0.14f;
                        }
                        break;
                    case "maxHight":
                        {
                            if (float.TryParse(value, out float maxHight))
                            {
                                maxHight = Mathf.Clamp(maxHight, 1.4f, 2.1f);
                                parameter.MaxHight = maxHight;
                            }
                            else
                                parameter.MaxHight = 1.4f;
                        }
                        break;
                    case "colorBrightness":
                        {
                            if (float.TryParse(value, out float colorBrightness))
                            {
                                colorBrightness = Mathf.Clamp(colorBrightness, 2.1f, 5.0f);
                                parameter.ColorBrightness = colorBrightness;
                            }
                            else
                                parameter.ColorBrightness = 3.4f;
                        }
                        break;
                    case "mainColor":
                        {
                            if (ColorUtility.TryParseHtmlString(value, out Color mainColor))
                                parameter.MainColor = mainColor;
                            else
                                parameter.MainColor = Color.blue;
                        }
                        break;
                    case "viceColor":
                        {
                            if (ColorUtility.TryParseHtmlString(value, out Color viceColor))
                                parameter.ViceColor = viceColor;
                            else
                                parameter.ViceColor = Color.green;
                        }
                        break;
                    case "updateInterval":
                        {
                            if (float.TryParse(value, out float updateInterval))
                            {
                                updateInterval = Mathf.Clamp(updateInterval, 0.02f, 0.3f);
                                parameter.UpdateInterval = updateInterval;
                            }
                            else
                                parameter.UpdateInterval = 0.08f;
                        }
                        break;
                    case "amplificationFactor":
                        {
                            if (float.TryParse(value, out float amplificationFactor))
                            {
                                amplificationFactor = Mathf.Clamp(amplificationFactor, 7f, 28f);
                                parameter.AmplificationFactor = amplificationFactor;
                            }
                            else
                                parameter.AmplificationFactor = 14f;
                        }
                        break;
                    case "rotateSpeed":
                        {
                            if (float.TryParse(value, out float rotateSpeed))
                            {
                                rotateSpeed = Mathf.Clamp(rotateSpeed, -30.0f, 30.0f);
                                parameter.RotateSpeed = rotateSpeed;
                            }
                            else
                                parameter.RotateSpeed = 10.0f;
                        }
                        break;

                    case "baseFontColor":
                        {
                            if (ColorUtility.TryParseHtmlString(value, out Color baseFontColor))
                                parameter.BaseFontColor = baseFontColor;
                            else
                                parameter.BaseFontColor = new Color(50.0f / 255.0f, 50.0f / 255.0f, 50.0f / 255.0f, 1.0f);
                        }
                        break;
                    case "currentFontColor":
                        {
                            if (ColorUtility.TryParseHtmlString(value, out Color currentFontColor))
                                parameter.CurrentFontColor = currentFontColor;
                            else
                                parameter.CurrentFontColor = new Color(163.0f / 255.0f, 1.0f, 6.0f / 255.0f, 255.0f);
                        }
                        break;
                    case "fontSize":
                        {
                            if (int.TryParse(value, out int fontSize))
                            {
                                fontSize = Mathf.Clamp(fontSize, 26, 30);
                                parameter.FontSize = fontSize;
                            }
                            else
                                parameter.FontSize = 30;
                        }
                        break;
                    default:
                        break;
                }
            }
            return parameter;
        }

        /// <summary>
        /// 保存配置
        /// </summary>
        internal static void AlterConfig()
        {
            Parameter parameter = ModelManager.Instance.GetParameter;
            string outPath = string.Concat(Application.streamingAssetsPath, configPath);
            if (File.Exists(outPath))
                File.Delete(outPath);

            XmlDocument xmlDocument = new XmlDocument();
            XmlDeclaration declaration = xmlDocument.CreateXmlDeclaration("1.0", "utf-8", null);
            //根节点
            XmlNode root = xmlDocument.CreateElement("items");

            //描述说明
            XmlComment xmlComment;
            //元素
            XmlElement item;

            //写入配置
            #region 半径
            xmlComment = xmlDocument.CreateComment("半径");
            item = xmlDocument.CreateElement("item");
            item.SetAttribute("name", "radius");
            item.SetAttribute("value", parameter.Radius.ToString());
            root.AppendChild(xmlComment);
            root.AppendChild(item);
            #endregion

            #region x轴偏移
            xmlComment = xmlDocument.CreateComment("x轴偏移");
            item = xmlDocument.CreateElement("item");
            item.SetAttribute("name", "offsetX");
            item.SetAttribute("value", parameter.OffsetX.ToString());
            root.AppendChild(xmlComment);
            root.AppendChild(item);
            #endregion

            #region y轴偏移
            xmlComment = xmlDocument.CreateComment("y轴偏移");
            item = xmlDocument.CreateElement("item");
            item.SetAttribute("name", "offsetY");
            item.SetAttribute("value", parameter.OffsetY.ToString());
            root.AppendChild(xmlComment);
            root.AppendChild(item);
            #endregion

            #region 可视化数据数量
            xmlComment = xmlDocument.CreateComment("可视化数据数量");
            item = xmlDocument.CreateElement("item");
            item.SetAttribute("name", "audioDataCount");
            item.SetAttribute("value", parameter.AudioDataCount.ToString());
            root.AppendChild(xmlComment);
            root.AppendChild(item);
            #endregion

            #region 线宽
            xmlComment = xmlDocument.CreateComment("线宽");
            item = xmlDocument.CreateElement("item");
            item.SetAttribute("name", "lineWidth");
            item.SetAttribute("value", parameter.LineWidth.ToString());
            root.AppendChild(xmlComment);
            root.AppendChild(item);
            #endregion

            #region 最小高度
            xmlComment = xmlDocument.CreateComment("最小高度");
            item = xmlDocument.CreateElement("item");
            item.SetAttribute("name", "minHight");
            item.SetAttribute("value", parameter.MinHight.ToString());
            root.AppendChild(xmlComment);
            root.AppendChild(item);
            #endregion

            #region 最大高度
            xmlComment = xmlDocument.CreateComment("最大高度");
            item = xmlDocument.CreateElement("item");
            item.SetAttribute("name", "maxHight");
            item.SetAttribute("value", parameter.MaxHight.ToString());
            root.AppendChild(xmlComment);
            root.AppendChild(item);
            #endregion

            #region 亮度
            xmlComment = xmlDocument.CreateComment("亮度");
            item = xmlDocument.CreateElement("item");
            item.SetAttribute("name", "colorBrightness");
            item.SetAttribute("value", parameter.ColorBrightness.ToString());
            root.AppendChild(xmlComment);
            root.AppendChild(item);
            #endregion

            #region 主颜色
            xmlComment = xmlDocument.CreateComment("主颜色");
            item = xmlDocument.CreateElement("item");
            item.SetAttribute("name", "AlphaEdge");
            item.SetAttribute("value", string.Concat("#", ColorUtility.ToHtmlStringRGB(parameter.MainColor)));
            root.AppendChild(xmlComment);
            root.AppendChild(item);
            #endregion

            #region 次要颜色
            xmlComment = xmlDocument.CreateComment("次要颜色");
            item = xmlDocument.CreateElement("item");
            item.SetAttribute("name", "viceColor");
            item.SetAttribute("value", string.Concat("#", ColorUtility.ToHtmlStringRGB(parameter.ViceColor)));
            root.AppendChild(xmlComment);
            root.AppendChild(item);
            #endregion

            #region 更新间隔
            xmlComment = xmlDocument.CreateComment("更新间隔");
            item = xmlDocument.CreateElement("item");
            item.SetAttribute("name", "updateInterval");
            item.SetAttribute("value", parameter.UpdateInterval.ToString());
            root.AppendChild(xmlComment);
            root.AppendChild(item);
            #endregion

            #region 放大因子
            xmlComment = xmlDocument.CreateComment("放大因子");
            item = xmlDocument.CreateElement("item");
            item.SetAttribute("name", "amplificationFactor");
            item.SetAttribute("value", parameter.AmplificationFactor.ToString());
            root.AppendChild(xmlComment);
            root.AppendChild(item);
            #endregion

            #region 旋转速度
            xmlComment = xmlDocument.CreateComment("旋转速度");
            item = xmlDocument.CreateElement("item");
            item.SetAttribute("name", "rotateSpeed");
            item.SetAttribute("value", parameter.RotateSpeed.ToString());
            root.AppendChild(xmlComment);
            root.AppendChild(item);
            #endregion

            #region 基础字体颜色
            xmlComment = xmlDocument.CreateComment("基础字体颜色");
            item = xmlDocument.CreateElement("item");
            item.SetAttribute("name", "baseFontColor");
            item.SetAttribute("value", string.Concat("#", ColorUtility.ToHtmlStringRGB(parameter.BaseFontColor)));
            root.AppendChild(xmlComment);
            root.AppendChild(item);
            #endregion

            #region 当前字体颜色
            xmlComment = xmlDocument.CreateComment("当前字体颜色");
            item = xmlDocument.CreateElement("item");
            item.SetAttribute("name", "currentFontColor");
            item.SetAttribute("value", string.Concat("#", ColorUtility.ToHtmlStringRGB(parameter.CurrentFontColor)));
            root.AppendChild(xmlComment);
            root.AppendChild(item);
            #endregion

            #region 字体大小
            xmlComment = xmlDocument.CreateComment("字体大小");
            item = xmlDocument.CreateElement("item");
            item.SetAttribute("name", "fontSize");
            item.SetAttribute("value", parameter.FontSize.ToString());
            root.AppendChild(xmlComment);
            root.AppendChild(item);
            #endregion

            xmlDocument.AppendChild(root);
            xmlDocument.InsertBefore(declaration, xmlDocument.DocumentElement);

            #region 生成Xml,UTF-8无BOM格式编码
            System.Text.UTF8Encoding utf8 = new System.Text.UTF8Encoding(false);
            StreamWriter sw = new StreamWriter(outPath, false, utf8);
            xmlDocument.Save(sw);
            sw.Close();
            sw.Dispose();
            #endregion
        }
    }
}