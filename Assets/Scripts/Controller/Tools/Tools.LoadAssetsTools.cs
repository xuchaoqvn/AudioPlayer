using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Video;

namespace AudioPlayer.Controller
{
    internal static partial class Tools
    {
        private const string GB2312 = "gb2312";

        /// <summary>
        /// 加载资源工具
        /// </summary>
        internal static class LoadAssetsTools
        {
            /// <summary>
            /// 开始加载音频
            /// </summary>
            /// <returns></returns>
            internal static UnityWebRequest StartLoadAudioClip(string url, AudioType audioType)
            {
                UnityWebRequest unityWebRequest = UnityWebRequest.Get(url);
                DownloadHandlerAudioClip downloadHandlerAudioClip = new DownloadHandlerAudioClip(url, audioType);
                unityWebRequest.downloadHandler = downloadHandlerAudioClip;
                var s = unityWebRequest.SendWebRequest();
                return unityWebRequest;
            }

            /// <summary>
            /// AudioClip加载中
            /// </summary>
            /// <param name="unityWebRequest"></param>
            /// <returns></returns>
            internal static bool LoadingAudioClip(UnityWebRequest unityWebRequest)
            {
                if (unityWebRequest == null)
                    return false;
                return unityWebRequest.isDone;
            }

            /// <summary>
            /// 音频加载结束
            /// </summary>
            /// <param name="unityWebRequest"></param>
            /// <returns></returns>
            internal static AudioClip LoadFinishAudioClip(UnityWebRequest unityWebRequest)
            {
                if (unityWebRequest == null)
                    return null;
                if (!unityWebRequest.isDone)
                    return null;
                if (unityWebRequest.isHttpError || unityWebRequest.isNetworkError)
                {
                    Debug.LogWarning(unityWebRequest.error);
                    return null;
                }
                return DownloadHandlerAudioClip.GetContent(unityWebRequest);
            }

            /// <summary>
            /// 开始加载文本
            /// </summary>
            /// <returns></returns>
            internal static UnityWebRequest StartLoadTextAsset(string url)
            {
                UnityWebRequest unityWebRequest = UnityWebRequest.Get(url);
                unityWebRequest.SendWebRequest();
                return unityWebRequest;
            }

            /// <summary>
            /// 文本资源加载中……
            /// </summary>
            /// <param name="unityWebRequest"></param>
            /// <returns></returns>
            internal static bool LoadingTextAsset(UnityWebRequest unityWebRequest)
            {
                if (unityWebRequest == null)
                    return false;
                return (unityWebRequest.isDone && unityWebRequest.downloadProgress >= 1.0f && unityWebRequest.downloadHandler.isDone);
            }

            /// <summary>
            /// 文本资源加载结束
            /// </summary>
            /// <param name="unityWebRequest"></param>
            /// <returns></returns>
            internal static string LoadFinishTextAsset(UnityWebRequest unityWebRequest)
            {
                if (unityWebRequest == null)
                    return string.Empty;
                if (!unityWebRequest.isDone)
                    return string.Empty;
                if (unityWebRequest.isHttpError || unityWebRequest.isNetworkError)
                {
                    Debug.LogWarning(unityWebRequest.error);
                    return string.Empty;
                }
                return Encoding.GetEncoding(GB2312).GetString(unityWebRequest.downloadHandler.data);
            }

            /// <summary>
            /// 开始加载视频
            /// </summary>
            /// <param name="url">路径</param>
            /// <param name="videoPlay">VideoPlay</param>
            internal static void StartLoadMovie(string url, VideoPlayer videoPlay)
            {
                videoPlay.url = url;
                videoPlay.Prepare();
            }

            /// <summary>
            /// 加载中视频中……
            /// </summary>
            /// <param name="videoPlayer">VideoPlayer组件</param>
            internal static bool LoadingMovie(VideoPlayer videoPlayer)
            {
                if (videoPlayer == null)
                    return false;
                if (!videoPlayer.enabled)
                    return true;
                return videoPlayer.isPrepared;
            }

            /// <summary>
            /// 视频资源加载结束
            /// </summary>
            /// <param name="videoPlayer">VideoPlayer组件</param>
            internal static void LoadFinishMovie(VideoPlayer videoPlayer)
            {
                if (videoPlayer == null)
                    return;
                if (videoPlayer.enabled)
                {
                    videoPlayer.frame = 30;
                    videoPlayer.Play();
                }
            }
        }
    }
}