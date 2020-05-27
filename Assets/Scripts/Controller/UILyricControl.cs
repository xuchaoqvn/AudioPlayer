using AudioPlayer.Model;
using UnityEngine;
using UnityEngine.UI;

namespace AudioPlayer.Controller
{
    /// <summary>
    /// ui��ʿ���
    /// </summary>
    internal static class UILyricControl
    {
        /// <summary>
        /// ��ʼ�����
        /// </summary>
        /// <param name="lyricItems">����ı�����</param>
        /// <param name="lyricInfo">�����Ϣ</param>
        internal static void InitLyric(Text[] lyricItems, LyricInfo lyricInfo)
        {
            if (!string.IsNullOrEmpty(lyricInfo.singerName))
                lyricItems[0].text = lyricInfo.singerName;
            if (!string.IsNullOrEmpty(lyricInfo.songName))
                lyricItems[1].text = lyricInfo.songName;
            if (!string.IsNullOrEmpty(lyricInfo.album))
                lyricItems[2].text = lyricInfo.album;
            for (int i = 3; i < lyricItems.Length; i++)
            {
                //��ȫ��飺��ֹ�������3��
                int index = i - 3;
                lyricItems[i].text = lyricInfo.lyrics[index] == null ? string.Empty : lyricInfo.lyrics[index].lyricContent;
            }
        }

        /// <summary>
        /// ��һ����
        /// </summary>
        /// <param name="lyricItems">����ı�����</param>
        /// <param name="index">����</param>
        /// <param name="lyricInfo">�����Ϣ</param>
        internal static void NextLyric(Text[] lyricItems, int index, LyricInfo lyricInfo)
        {
            for (int i = 0; i < lyricItems.Length; i++)
            {
                if (i != lyricItems.Length - 1)
                    lyricItems[i].text = lyricItems[i + 1].text;
                else
                    lyricItems[i].text = index + 4 >= lyricInfo.lyrics.Count ? string.Empty : lyricInfo.lyrics[index + 4].lyricContent;
            }
        }

        /// <summary>
        /// �ı��ʵ�ָ������
        /// </summary>
        /// <param name="lyricItems">����ı�����</param>
        /// <param name="index">����</param>
        /// <param name="lyricInfo">�����Ϣ</param>
        internal static void ChangeLyric(Text[] lyricItems, int index, LyricInfo lyricInfo)
        {
            for (int i = 0; i < lyricItems.Length; i++)
            {
                int indexTemp = i - 3 + index;
                if (indexTemp < 0)
                {
                    //���������֡�ר��
                    if (indexTemp == -3)
                    {
                        if (!string.IsNullOrEmpty(lyricInfo.singerName))
                            lyricItems[i].text = lyricInfo.singerName;
                        else
                            lyricItems[i].text = string.Empty;
                    }
                    else if (indexTemp == -2)
                    {
                        if (!string.IsNullOrEmpty(lyricInfo.songName))
                            lyricItems[i].text = lyricInfo.songName;
                        else
                            lyricItems[i].text = string.Empty;
                    }
                    else if (indexTemp == -1)
                    {
                        if (!string.IsNullOrEmpty(lyricInfo.album))
                            lyricItems[i].text = lyricInfo.album;
                        else
                            lyricItems[i].text = string.Empty;
                    }
                }
                else if (indexTemp >= lyricInfo.lyrics.Count)
                    lyricItems[i].text = string.Empty;
                else
                    lyricItems[i].text = lyricInfo.lyrics[indexTemp].lyricContent;
            }
        }

        /// <summary>
        /// ���ø�����Ϣ
        /// </summary>
        /// <param name="lyricItems">����ı�����</param>
        internal static void ResetLyric(Text[] lyricItems)
        {
            for (int i = 0; i < lyricItems.Length; i++)
                lyricItems[i].text = string.Empty;
        }

        /// <summary>
        /// �ı���������ɫ
        /// </summary>
        /// <param name="color">�ı����ɫ</param>
        /// <param name="lyricItems">����ı�����</param>
        internal static void ChangeBaseLyricColor(Color color, Text[] lyricItems)
        {
            for (int i = 0; i < lyricItems.Length; i++)
                lyricItems[i].color = i == 3 ? lyricItems[i].color : new Color(color.r, color.g, color.b, lyricItems[i].color.a);
        }

        /// <summary>
        /// �ı䵱ǰ�����ɫ
        /// </summary>
        /// <param name="color">�ı����ɫ</param>
        /// <param name="lyricItems">����ı�</param>
        internal static void ChangeCurrentLyricColor(Color color, Text[] lyricItems)
        {
            lyricItems[(lyricItems.Length - 1) / 2].color = color;
        }

        /// <summary>
        /// �ı䵱ǰ��������С
        /// </summary>
        /// <param name="fontSize">�����С</param>
        /// <param name="lyricItems">����ı�����</param>
        internal static void ChangeLyricFontSize(int fontSize, Text[] lyricItems)
        {
            int index = (lyricItems.Length - 1) / 2;
            lyricItems[index].fontSize = fontSize;
            int count = (lyricItems.Length - 1) / 2;
            for (int i = 1; i <= index; i++)
            {
                lyricItems[index + i].fontSize = fontSize - (2 * i);
                lyricItems[index - i].fontSize = fontSize - (2 * i);
            }
        }
    }
}
