using System.IO;
using UnityEngine;

namespace AudioPlayer.Controller
{
    internal static partial class Tools
    {
        /// <summary>
        /// 路径
        /// </summary>
        internal static class PathTools
        {
            /// <summary>
            /// Audio文件夹
            /// </summary>
            private const string AUDIO = "/Audios";

            /// <summary>
            /// 获取音频路径
            /// </summary>
            /// <returns></returns>
            internal static string GetAudioPath
            {
                get { return string.Concat(Application.streamingAssetsPath, AUDIO); }
            }

            /// <summary>
            /// 获取指定路径下的文件
            /// </summary>
            /// <param name="path"></param>
            /// <returns></returns>
            internal static string[] GetFileName(string path)
            {
                if (!Directory.Exists(path))
                    return null;
                return Directory.GetFiles(path);
            }
        }
    }
}