using AudioPlayer.Controller;

namespace AudioPlayer.View
{
    /// <summary>
    /// 音量面板
    /// </summary>
    internal class VolumePanel : BasePanel
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
                case "VolumeSlider":
                    uIBehaviour.OnSliderValueChange(new UnityEngine.Events.UnityAction<float>((value) => SongControl.ChangeSongVolume(value)));
                    break;
                default:
                    break;
            }
        }
    }
}