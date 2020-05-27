using AudioPlayer.Controller;

namespace AudioPlayer.View
{
    /// <summary>
    /// 歌曲播放类型面板
    /// </summary>
    internal class SongTypePanel : BasePanel
    {
        internal override void Start()
        {
            base.Start();
            UIManager.Instance.ClosePanel(this.gameObject.name);
        }

        /// <summary>
        /// UI控件注册事件
        /// </summary>
        /// <param name="uiControlsName">UI控件名称</param>
        /// <param name="uIBehaviour">UI控件</param>
        internal override void RegisterUIControlEvent(string uiControlsName, UIBehaviour uIBehaviour)
        {
            switch (uiControlsName)
            {
                case "SinglePlay":
                    uIBehaviour.OnToggleValueChange(new UnityEngine.Events.UnityAction<bool>((isOn) => {
                        if (isOn)
                            SongControl.ChangeSongPlayType(CycleType.SinglePlay);
                    }));
                    break;
                case "SingleCycle":
                    uIBehaviour.OnToggleValueChange(new UnityEngine.Events.UnityAction<bool>((isOn) => {
                        if (isOn)
                            SongControl.ChangeSongPlayType(CycleType.SingleCycle);
                    }));
                    break;
                case "PlayOrder":
                    uIBehaviour.OnToggleValueChange(new UnityEngine.Events.UnityAction<bool>((isOn) => {
                        if (isOn)
                            SongControl.ChangeSongPlayType(CycleType.PlayInOrder);
                    }));
                    break;
                case "LoopPlayack":
                    uIBehaviour.OnToggleValueChange(new UnityEngine.Events.UnityAction<bool>((isOn) => {
                        if (isOn)
                            SongControl.ChangeSongPlayType(CycleType.LoopPlayback);
                    }));
                    break;
                case "RandomPlay":
                    uIBehaviour.OnToggleValueChange(new UnityEngine.Events.UnityAction<bool>((isOn) => {
                        if (isOn)
                            SongControl.ChangeSongPlayType(CycleType.RandomPlay);
                    }));
                    break;
                default:
                    break;
            }
        }
    }
}