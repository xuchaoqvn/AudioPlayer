using AudioPlayer.Model;
using static AudioPlayer.Controller.Tools;

namespace AudioPlayer.Controller
{
    /// <summary>
    /// 歌曲控制
    /// </summary>
    internal static class SongControl
    {
        /// <summary>
        /// 监听歌曲是否播放完毕
        /// </summary>
        internal static void ListeningSongOver()
        {
            ScenesDatas scenesDatas = ModelManager.Instance.GetScenesDatas;
            if (scenesDatas.AudioSource.clip == null)
                return;

            if (SongTimeTools.IsSongOver(AudioSourceData.GetCurrentSongTime(scenesDatas.AudioSource), AudioSourceData.GetCurrentSongLength(scenesDatas.AudioSource)))
            {
                switch (ModelManager.Instance.GetLogicDatas.CycleType)
                {
                    case CycleType.SingleCycle:
                        SongControl.OnSingleCycle();
                        break;
                    case CycleType.PlayInOrder:
                        SongControl.OnPlayInOrder();
                        break;
                    case CycleType.LoopPlayback:
                        SongControl.OnLoopPlayback();
                        break;
                    case CycleType.RandomPlay:
                        SongControl.OnRandomPlay();
                        break;
                    case CycleType.SinglePlay:
                        SongControl.OnSinglePlay();
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// 更新歌曲播放时的进度
        /// </summary>
        internal static void UpdateSongPlayingTime()
        {
            if (ModelManager.Instance.GetLogicDatas.GameState != GameState.Playing)
                return;
            ScenesDatas scenesDatas = ModelManager.Instance.GetScenesDatas;
            scenesDatas.SongPlayingTime.text = SongTimeTools.SongTimeToStringTime(scenesDatas.AudioSource.time);
            scenesDatas.SongTimeSlide.value = scenesDatas.AudioSource.time / scenesDatas.AudioSource.clip.length;
        }

        #region 歌曲播放类型事件
        /// <summary>
        /// 当歌曲播放完毕时是单曲循环时
        /// </summary>
        private static void OnSingleCycle()
        {
            LogicDatas logicDatas = ModelManager.Instance.GetLogicDatas;
            ScenesDatas scenesDatas = ModelManager.Instance.GetScenesDatas;
            scenesDatas.AudioSource.time = 0.0f;
            scenesDatas.AudioSource.Play();
            scenesDatas.VideoPlayer.time = 0.0f;
            scenesDatas.VideoPlayer.Play();
            scenesDatas.SongPlayingTime.text = "00:00";
            scenesDatas.SongTimeSlide.value = 0.0f;
            UILyricControl.ResetLyric(scenesDatas.LyricItems);
            UILyricControl.InitLyric(scenesDatas.LyricItems, logicDatas.LyricInfo);
            if (!string.IsNullOrEmpty(scenesDatas.VideoPlayer.url) && scenesDatas.VideoPlayer.isPrepared)
                scenesDatas.VideoPlayer.Play();
            logicDatas.Index = 0;
        }

        /// <summary>
        /// 当歌曲播放完毕时是顺序播放时
        /// </summary>
        private static void OnPlayInOrder()
        {
            LogicDatas logicDatas = ModelManager.Instance.GetLogicDatas;
            ScenesDatas scenesDatas = ModelManager.Instance.GetScenesDatas;
            logicDatas.CurrentSongIndex++;
            logicDatas.Index = 0;
            if (logicDatas.CurrentSongIndex >= logicDatas.SongsName.Length)
            {
                scenesDatas.AudioSource.Stop();
                UILyricControl.ResetLyric(scenesDatas.LyricItems);
                if (scenesDatas.VideoPlayer.isPlaying)
                    scenesDatas.VideoPlayer.Stop();
                logicDatas.CurrentSongIndex = logicDatas.SongsName.Length - 1;
            }
            else
            {
                SongControl.ReleaseAudioRelatedResources();
                SongControl.LoadNextSong(logicDatas.SongsName[logicDatas.CurrentSongIndex]);
            }
        }

        /// <summary>
        /// 当歌曲播放完毕时是循环播放时
        /// </summary>
        private static void OnLoopPlayback()
        {
            LogicDatas logicDatas = ModelManager.Instance.GetLogicDatas;
            logicDatas.CurrentSongIndex++;
            logicDatas.CurrentSongIndex %= logicDatas.SongsName.Length;
            logicDatas.Index = 0;
            SongControl.ReleaseAudioRelatedResources();
            SongControl.LoadNextSong(logicDatas.SongsName[logicDatas.CurrentSongIndex]);
        }

        /// <summary>
        /// 当歌曲播放完毕时是随机播放时
        /// </summary>
        private static void OnRandomPlay()
        {
            LogicDatas logicDatas = ModelManager.Instance.GetLogicDatas;
            ScenesDatas scenesDatas = ModelManager.Instance.GetScenesDatas;
            int index = UnityEngine.Random.Range(0, logicDatas.SongsName.Length);
            logicDatas.Index = 0;
            if (index == logicDatas.CurrentSongIndex)
            {
                scenesDatas.AudioSource.Play();
                if (string.IsNullOrEmpty(scenesDatas.VideoPlayer.url) && scenesDatas.VideoPlayer.isPrepared)
                    scenesDatas.VideoPlayer.Play();
                UILyricControl.InitLyric(scenesDatas.LyricItems, logicDatas.LyricInfo);
            }
            else
            {
                logicDatas.CurrentSongIndex = index;
                SongControl.ReleaseAudioRelatedResources();
                SongControl.LoadNextSong(logicDatas.SongsName[logicDatas.CurrentSongIndex]);
            }
        }

        /// <summary>
        /// 当歌曲播放完毕时是单曲播放时
        /// </summary>
        private static void OnSinglePlay()
        {
            ScenesDatas scenesDatas = ModelManager.Instance.GetScenesDatas;
            scenesDatas.AudioSource.Stop();
            UILyricControl.ResetLyric(scenesDatas.LyricItems);
            scenesDatas.VideoPlayer.Stop();
            ModelManager.Instance.GetLogicDatas.Index = 0;
            SongControl.ReleaseAudioRelatedResources();
        }
        #endregion

        #region UI事件
        /// <summary>
        /// 改变歌曲
        /// </summary>
        /// <param name="index">歌曲索引</param>
        internal static void ChangeSong(int index)
        {
            if (ModelManager.Instance.GetLogicDatas.GameState == GameState.Loading)
                return;
            SongControl.StopSong();
            SongControl.ReleaseAudioRelatedResources();
            string[] songName = ModelManager.Instance.GetLogicDatas.SongsName;
            if (index < 0 || index >= songName.Length)
                return;
            ModelManager.Instance.GetLogicDatas.CurrentSongIndex = index;
            SongControl.LoadNextSong(songName[index]);
        }

        /// <summary>
        /// 播放上一首
        /// </summary>
        internal static void PlayLastSong()
        {
            LogicDatas logicDatas = ModelManager.Instance.GetLogicDatas;
            if (logicDatas.GameState == GameState.StandBy)
                SongControl.LoadNextSong(logicDatas.SongsName[0]);
            else if (logicDatas.GameState == GameState.Playing || logicDatas.GameState == GameState.Pause)
            {
                logicDatas.CurrentSongIndex--;
                if (logicDatas.CurrentSongIndex < 0)
                    logicDatas.CurrentSongIndex = logicDatas.SongsName.Length - 1;
                logicDatas.Index = 0;
                SongControl.StopSong();
                SongControl.ReleaseAudioRelatedResources();
                SongControl.LoadNextSong(logicDatas.SongsName[logicDatas.CurrentSongIndex]);
            }
        }

        /// <summary>
        /// 播放或暂停
        /// </summary>
        internal static void PlayOrPauseSong()
        {
            LogicDatas logicDatas = ModelManager.Instance.GetLogicDatas;
            ScenesDatas scenesDatas = ModelManager.Instance.GetScenesDatas;
            if (logicDatas.GameState == GameState.Pause)
            {
                logicDatas.GameState = GameState.Playing;
                SongControl.PlaySong();
            }
            else if (logicDatas.GameState == GameState.StandBy)
            {
                logicDatas.GameState = GameState.Playing;
                SongControl.ChangeSong(0);
            }
            else if (logicDatas.GameState == GameState.Playing)
            {
                logicDatas.GameState = GameState.Pause;
                SongControl.PauseSong();
            }
        }

        /// <summary>
        /// 播放歌曲
        /// </summary>
        private static void PlaySong()
        {
            LogicDatas logicDatas = ModelManager.Instance.GetLogicDatas;
            ScenesDatas scenesDatas = ModelManager.Instance.GetScenesDatas;
            scenesDatas.AudioSource.Play();
            scenesDatas.VideoPlayer.Play();
        }

        /// <summary>
        /// 暂停歌曲
        /// </summary>
        private static void PauseSong()
        {
            LogicDatas logicDatas = ModelManager.Instance.GetLogicDatas;
            ScenesDatas scenesDatas = ModelManager.Instance.GetScenesDatas;
            scenesDatas.AudioSource.Pause();
            scenesDatas.VideoPlayer.Pause();
        }

        /// <summary>
        /// 停止歌曲
        /// </summary>
        private static void StopSong()
        {
            LogicDatas logicDatas = ModelManager.Instance.GetLogicDatas;
            ScenesDatas scenesDatas = ModelManager.Instance.GetScenesDatas;
            scenesDatas.AudioSource.Stop();
            scenesDatas.VideoPlayer.Stop();
        }

        /// <summary>
        /// 播放下一首
        /// </summary>
        internal static void PlayNextSong()
        {
            LogicDatas logicDatas = ModelManager.Instance.GetLogicDatas;
            if (logicDatas.GameState == GameState.StandBy)
                SongControl.LoadNextSong(logicDatas.SongsName[0]);
            else if (logicDatas.GameState == GameState.Playing || logicDatas.GameState == GameState.Pause)
            {
                logicDatas.CurrentSongIndex++;
                logicDatas.CurrentSongIndex %= logicDatas.SongsName.Length;
                logicDatas.Index = 0;
                SongControl.StopSong();
                SongControl.ReleaseAudioRelatedResources();
                SongControl.LoadNextSong(logicDatas.SongsName[logicDatas.CurrentSongIndex]);
            }
        }

        /// <summary>
        /// 改变歌曲进度
        /// </summary>
        /// <param name="songTime">歌曲进度</param>
        internal static void ChangeSongTime(float songTime)
        {
            if (songTime <= 0.0f || songTime >= 1.0)
                return;
            LogicDatas logicDatas = ModelManager.Instance.GetLogicDatas;
            ScenesDatas scenesDatas = ModelManager.Instance.GetScenesDatas;
            if (logicDatas.GameState == GameState.Playing || logicDatas.GameState == GameState.Pause)
            {
                scenesDatas.AudioSource.time = songTime * scenesDatas.AudioSource.clip.length;
                scenesDatas.VideoPlayer.time = songTime * scenesDatas.VideoPlayer.frameCount / scenesDatas.VideoPlayer.frameRate;
                SongLyric.ChangeLyric(songTime * scenesDatas.AudioSource.clip.length);
            }
        }

        /// <summary>
        /// 改变歌曲音量
        /// </summary>
        /// <param name="value"></param>
        internal static void ChangeSongVolume(float value)
        {
            value = UnityEngine.Mathf.Clamp01(value);
            ModelManager.Instance.GetScenesDatas.AudioSource.volume = value;
        }

        /// <summary>
        /// 改变歌曲播放类型
        /// </summary>
        /// <param name="cycleType">播放类型</param>
        internal static void ChangeSongPlayType(CycleType cycleType)
        {
            ModelManager.Instance.GetLogicDatas.CycleType = cycleType;
        }
        #endregion

        /// <summary>
        /// 播放加载完毕的歌曲
        /// </summary>
        internal static void PlayLoadFinishSong()
        {
            LogicDatas logicDatas = ModelManager.Instance.GetLogicDatas;
            ScenesDatas scenesDatas = ModelManager.Instance.GetScenesDatas;
            UILyricControl.InitLyric(scenesDatas.LyricItems, logicDatas.LyricInfo);
            scenesDatas.SongName.text = System.IO.Path.GetFileNameWithoutExtension(logicDatas.SongsName[logicDatas.CurrentSongIndex]);
            scenesDatas.SongPlayingTime.text = "00:00";
            scenesDatas.SongTotalTime.text = SongTimeTools.SongTimeToStringTime(scenesDatas.AudioSource.clip.length);
            scenesDatas.SongTimeSlide.value = 0.0f;
            scenesDatas.AudioSource.time = 0.0f;
            scenesDatas.AudioSource.Play();
            logicDatas.GameState = GameState.Playing;
        }

        /// <summary>
        /// 加载下一首歌曲
        /// </summary>
        /// <param name="songName">歌曲名称</param>
        private static void LoadNextSong(string songName)
        {
            ModelManager.Instance.GetLogicDatas.GameState = GameState.Loading;
            AssetsControl.StartLoadAssets(songName);
        }

        /// <summary>
        /// 释放已播放完毕的音频相关资源
        /// </summary>
        private static void ReleaseAudioRelatedResources()
        {
            ScenesDatas scenesDatas = ModelManager.Instance.GetScenesDatas;
            scenesDatas.AudioSource.Stop();
            scenesDatas.AudioSource.clip?.UnloadAudioData();
            scenesDatas.AudioSource.clip = null;
            ModelManager.Instance.GetLogicDatas.LyricInfo = null;
            scenesDatas.VideoPlayer.Stop();
            scenesDatas.VideoPlayer.enabled = false;
            scenesDatas.VideoPlayer.clip = null;
            UILyricControl.ResetLyric(scenesDatas.LyricItems);
            scenesDatas.SongName.text = string.Empty;
            scenesDatas.SongPlayingTime.text = string.Empty;
            scenesDatas.SongTotalTime.text = string.Empty;
            scenesDatas.SongTimeSlide.value = 0.0f;
            UnityEngine.Resources.UnloadUnusedAssets();
        }
    }
}