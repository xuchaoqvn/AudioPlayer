using AudioPlayer.Controller;
using UnityEngine;
using UnityEngine.UI;

namespace AudioPlayer.View
{
    /// <summary>
    /// 歌曲列表
    /// </summary>
    internal class SongListPanel : BasePanel
    {
        /// <summary>
        /// SongItem
        /// </summary>
        private GameObject item;

        private Transform content;

        internal override void Start()
        {
            base.Start();
            this.item = Resources.Load<GameObject>("Prefabs/Item");
            this.content = this.gameObject.GetComponentInChildren<VerticalLayoutGroup>().transform;
            UIManager.Instance.ClosePanel(this.gameObject.name);
        }

        internal override void OnShow()
        {
            base.OnShow();
            this.RefreshSongList();
        }

        internal override void OnHide()
        {
            base.OnHide();
            for (int i = 0; i < this.content.childCount; i++)
                this.content.GetChild(i).gameObject.SetActive(false);
        }

        /// <summary>
        /// 刷新歌曲列表
        /// </summary>
        internal void RefreshSongList()
        {
            string[] songName = UISongListControl.GetSongList();
            if (this.content.childCount >= songName.Length)
            {
                Button button;
                Transform transform;
                for (int i = 0; i < songName.Length; i++)
                {
                    transform = this.content.GetChild(i);
                    transform.gameObject.SetActive(true);
                    transform.GetComponentInChildren<Text>().text = songName[i];
                    button = transform.GetComponent<Button>();
                    //使用新的变量，i会随着循环变动
                    button.onClick.RemoveAllListeners();
                    int index = i;
                    button.onClick.AddListener(new UnityEngine.Events.UnityAction(() => this.RegisterOnButtonClick(index)));
                }
            }
            else if (this.content.childCount < songName.Length)
            {
                Button button;
                Transform transform;
                for (int i = 0; i < this.content.childCount; i++)
                {
                    transform = this.content.GetChild(i);
                    transform.gameObject.SetActive(true);
                    transform.GetComponentInChildren<Text>().text = songName[i];
                    button = transform.GetComponent<Button>();
                    button.onClick.RemoveAllListeners();
                    //使用新的变量，i会随着循环变动
                    int index = i;
                    button.onClick.AddListener(new UnityEngine.Events.UnityAction(() => this.RegisterOnButtonClick(index)));
                }
                GameObject itemTemp;
                for (int i = this.content.childCount; i < songName.Length; i++)
                {
                    itemTemp = GameObject.Instantiate<GameObject>(this.item, this.content, false);
                    itemTemp.GetComponentInChildren<Text>().text = songName[i];
                    button = itemTemp.GetComponent<Button>();
                    //使用新的变量，i会随着循环变动
                    int index = i;
                    button.onClick.AddListener(new UnityEngine.Events.UnityAction(() => this.RegisterOnButtonClick(index)));
                }
            }
        }

        /// <summary>
        /// 当Button点击时
        /// </summary>
        private void RegisterOnButtonClick(int index)
        {
            SongControl.ChangeSong(index);
        }
    }
}