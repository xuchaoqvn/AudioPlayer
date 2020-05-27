using AudioPlayer.Model;
using NAudio.Wave;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using static AudioPlayer.Controller.Tools;

namespace AudioPlayer.Controller
{
    /// <summary>
    /// 资源控制
    /// </summary>
    internal static class AssetsControl
    {
        /// <summary>
        /// Audio文件夹
        /// </summary>
        private const string AUDIO = "/Audios/";

        /// <summary>
        /// 歌词文件夹
        /// </summary>
        private const string LYRIC = "/Lyrics/";

        /// <summary>
        /// 视频文件夹
        /// </summary>
        private const string MOVIE = "/Movies/";

        /// <summary>
        /// 歌词后缀
        /// </summary>
        private const string LYRICEXTENSION = ".lrc";

        /// <summary>
        /// 视频后缀
        /// </summary>
        private const string MOVIEEXTENSION = ".mp4";

        /// <summary>
        /// mp3转wav后缀
        /// </summary>
        private const string AUDIOMP3TOWAVEXTENSION = ".wav";

        /// <summary>
        /// 歌曲UnityWebRequest
        /// </summary>
        private static UnityWebRequest songRequest;

        /// <summary>
        /// 文本UnityWebRequest
        /// </summary>
        private static UnityWebRequest textAssetRequest;

        /// <summary>
        /// 开始加载资源
        /// </summary>
        /// <param name="fileExtensionName">带后缀的文件名</param>
        internal static void StartLoadAssets(string fileExtensionName)
        {
            //后缀名
            string extension = Path.GetExtension(fileExtensionName);
            //文件名（不带后缀）
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileExtensionName);
            string songPath = string.Concat(Application.streamingAssetsPath, AssetsControl.AUDIO, fileExtensionName);
            string lyricPath = string.Concat(Application.streamingAssetsPath, AssetsControl.LYRIC, fileNameWithoutExtension, AssetsControl.LYRICEXTENSION);
            string moviePath = string.Concat(Application.streamingAssetsPath, AssetsControl.MOVIE, fileNameWithoutExtension, AssetsControl.MOVIEEXTENSION);

            if (File.Exists(songPath))
            {
                AudioType audioType = AssetsControl.JudgeAudioType(extension);
                if (audioType == AudioType.MPEG)
                {
                    //转化为wav
                    string outSongPath = string.Concat(Application.streamingAssetsPath, AssetsControl.AUDIO, fileNameWithoutExtension, AssetsControl.AUDIOMP3TOWAVEXTENSION);
                    using (var reader = new Mp3FileReader(songPath))
                    {
                        WaveFileWriter.CreateWaveFile(outSongPath, reader);
                    }
                    audioType = AudioType.WAV;
                    //此处更新文件名fileExtensionName
                    string[] songName = ModelManager.Instance.GetLogicDatas.SongsName;
                    for (int i = 0; i < songName.Length; i++)
                    {
                        if (songName[i].Equals(fileExtensionName))
                        {
                            songName[i] = string.Concat(fileNameWithoutExtension, AssetsControl.AUDIOMP3TOWAVEXTENSION);
                            break;
                        }
                    }
                    File.Delete(songPath);
                    //重定向路径
                    songPath = outSongPath;
                }
                AssetsControl.songRequest = LoadAssetsTools.StartLoadAudioClip(songPath, audioType);
            }
            else
                Debug.LogError("歌曲文件不存在");

            if (File.Exists(lyricPath))
                AssetsControl.textAssetRequest = LoadAssetsTools.StartLoadTextAsset(lyricPath);

            if (File.Exists(moviePath))
            {
                ModelManager.Instance.GetScenesDatas.VideoPlayer.enabled = true;
                LoadAssetsTools.StartLoadMovie(moviePath, ModelManager.Instance.GetScenesDatas.VideoPlayer);
            }
        }

        /// <summary>
        /// 音频类型
        /// </summary>
        /// <param name="extension">后缀名</param>
        /// <returns></returns>
        private static AudioType JudgeAudioType(string extension)
        {
            AudioType audioType;
            switch (extension)
            {
                case ".wav":
                    audioType = AudioType.WAV;
                    break;
                case ".mp3":
                    audioType = AudioType.MPEG;
                    break;
                case ".ogg":
                    audioType = AudioType.OGGVORBIS;
                    break;
                case ".aif":
                    audioType = AudioType.AIFF;
                    break;
                default:
                    audioType = AudioType.UNKNOWN;
                    break;
            }
            return audioType;
        }

        /// <summary>
        /// 资源是否加载完成
        /// </summary>
        /// <returns></returns>
        private static bool IsLoadFinishAssets()
        {
            bool audioClip = LoadAssetsTools.LoadingAudioClip(AssetsControl.songRequest);
            bool textAsset = LoadAssetsTools.LoadingTextAsset(AssetsControl.textAssetRequest) && AssetsControl.textAssetRequest != null;
            bool movie = LoadAssetsTools.LoadingMovie(ModelManager.Instance.GetScenesDatas.VideoPlayer);

            return (audioClip && textAsset && movie);
        }

        /// <summary>
        /// 加载完成
        /// </summary>
        /// <param name="audioClip">音频</param>
        /// <param name="textContent">歌词</param>
        internal static void LoadFinish(out AudioClip audioClip, out string textContent)
        {
            audioClip = LoadAssetsTools.LoadFinishAudioClip(AssetsControl.songRequest);
            textContent = LoadAssetsTools.LoadFinishTextAsset(AssetsControl.textAssetRequest);
            LoadAssetsTools.LoadFinishMovie(ModelManager.Instance.GetScenesDatas.VideoPlayer);
            AssetsControl.songRequest?.Dispose();
            AssetsControl.songRequest = null;
            AssetsControl.textAssetRequest?.Dispose();
            AssetsControl.textAssetRequest = null;
        }

        /// <summary>
        /// 刷新资源
        /// </summary>
        internal static void RefreshAssets()
        {
            LogicDatas logicDatas = ModelManager.Instance.GetLogicDatas;
            ScenesDatas scenesDatas = ModelManager.Instance.GetScenesDatas;
            if (logicDatas.GameState != GameState.Loading)
                return;
            if (AssetsControl.IsLoadFinishAssets())
            {
                //此处应统一调用并更新UI
                AssetsControl.LoadFinish(out AudioClip audioClip, out string textContent);
                scenesDatas.AudioSource.clip = audioClip;
                logicDatas.LyricInfo = ParseLyric.ParseLyricFunc(textContent);
                SongControl.PlayLoadFinishSong();
            }
        }
    }
}