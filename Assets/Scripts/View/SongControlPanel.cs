namespace AudioPlayer.View
{
    /// <summary>
    /// 歌曲控制面板
    /// </summary>
    internal class SongControlPanel : BasePanel
    {
        /// <summary>
        /// 注册UI控件响应或被响应事件
        /// </summary>
        /// <param name="uiControlsName">UI控件名称</param>
        /// <param name="uIBehaviour">UI控件</param>
        internal override void RegisterUIControlEvent(string uiControlsName, UIBehaviour uIBehaviour)
        {
            switch (uiControlsName)
            {
                case "Last":
                    uIBehaviour.OnButtonClick(new UnityEngine.Events.UnityAction(() => Controller.SongControl.PlayLastSong()));
                    break;
                case "PlayOrPause":
                    uIBehaviour.OnButtonClick(new UnityEngine.Events.UnityAction(() => Controller.SongControl.PlayOrPauseSong()));
                    break;
                case "Next":
                    uIBehaviour.OnButtonClick(new UnityEngine.Events.UnityAction(() => Controller.SongControl.PlayNextSong()));
                    break;
                case "LyricOrList":
                    break;
                case "SongTimeSlider":
                    uIBehaviour.OnEventTrigger(UnityEngine.EventSystems.EventTriggerType.Drag,new UnityEngine.Events.UnityAction<UnityEngine.EventSystems.BaseEventData>((baseEvent) => {
                        Controller.SongControl.ChangeSongTime(uIBehaviour.GetComponent<UnityEngine.UI.Slider>().value);
                    }));
                    break;
                case "PlayType":
                    uIBehaviour.OnButtonClick(new UnityEngine.Events.UnityAction(() =>
                    {
                        if (UIManager.Instance.IsOpenPanel("SongTypePanel"))
                            UIManager.Instance.ClosePanel("SongTypePanel");
                        else
                            UIManager.Instance.OpenPanel("SongTypePanel");
                    }));
                    break;
                case "Volume":
                    uIBehaviour.OnEventTrigger(UnityEngine.EventSystems.EventTriggerType.PointerEnter, new UnityEngine.Events.UnityAction<UnityEngine.EventSystems.BaseEventData>((baseEvent) => {
                        UIManager.Instance.OpenPanel("VolumePanel");
                        if (UIManager.Instance.IsOpenPanel("SongTypePanel"))
                            UIManager.Instance.ClosePanel("SongTypePanel");
                    }));
                    uIBehaviour.OnEventTrigger(UnityEngine.EventSystems.EventTriggerType.PointerExit, new UnityEngine.Events.UnityAction<UnityEngine.EventSystems.BaseEventData>((baseEvent) => {
                        UIManager.Instance.ClosePanel("VolumePanel");
                    }));
                    break;
                case "SongList":
                    {
                        uIBehaviour.OnButtonClick(new UnityEngine.Events.UnityAction(() => {
                            if (UIManager.Instance.IsOpenPanel("SongListPanel"))
                                UIManager.Instance.ClosePanel("SongListPanel");
                            else
                                UIManager.Instance.OpenPanel("SongListPanel");
                        }));
                    }
                    break;
                default:
                    break;
            }
        }
    }
}