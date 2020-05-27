using AudioPlayer.Model;

namespace AudioPlayer.Controller
{
    /// <summary>
    /// 歌词
    /// </summary>
    internal static class SongLyric
    {
        /// <summary>
        /// 监听歌曲歌词
        /// </summary>
        internal static void ListeningSongLyric()
        {
            LogicDatas logicDatas = ModelManager.Instance.GetLogicDatas;
            ScenesDatas scenesDatas = ModelManager.Instance.GetScenesDatas;
            if (logicDatas.LyricInfo == null)
                return;
            //最后一句歌词
            if (logicDatas.Index >= logicDatas.LyricInfo.lyrics.Count - 1)
                return;

            //需播放下一句歌词时
            if (Tools.AudioSourceData.GetCurrentSongTime(scenesDatas.AudioSource) >= logicDatas.LyricInfo.lyrics[logicDatas.Index + 1].lyricTime)
            {
                //切换当前歌词为index+1
                UILyricControl.NextLyric(scenesDatas.LyricItems, logicDatas.Index, logicDatas.LyricInfo);
                logicDatas.Index++;
            }
        }

        /// <summary>
        /// 改变歌词
        /// </summary>
        /// <param name="songtime">歌曲时间</param>
        internal static void ChangeLyric(float songtime)
        {
            LyricInfo lyricInfo = ModelManager.Instance.GetLogicDatas.LyricInfo;
            int index = -1;
            for (int i = 0; i < lyricInfo.lyrics.Count; i++)
            {
                if (lyricInfo.lyrics[i].lyricTime >= songtime)
                {
                    index = i;
                    break;
                }
            }
            if (index == -1)
                return;
            LogicDatas logicDatas = ModelManager.Instance.GetLogicDatas;
            ScenesDatas scenesDatas = ModelManager.Instance.GetScenesDatas;
            logicDatas.Index = index;
            UILyricControl.ChangeLyric(scenesDatas.LyricItems, logicDatas.Index, logicDatas.LyricInfo);
        }
    }
}