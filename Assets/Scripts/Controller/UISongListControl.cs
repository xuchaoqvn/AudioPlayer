using AudioPlayer.Model;
using System.IO;
using System.Linq;

namespace AudioPlayer.Controller
{
    internal static class UISongListControl
    {
        /// <summary>
        /// 获取歌曲列表
        /// </summary>
        /// <returns></returns>
        internal static string[] GetSongList()
        {
            return ModelManager.Instance.GetLogicDatas.SongsName.Select(item => {
                return Path.GetFileNameWithoutExtension(item);
            }).ToArray();
        }
    }
}