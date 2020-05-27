using AudioPlayer.Model;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace AudioPlayer.Controller
{
    /// <summary>
    /// 初始化和清空
    /// </summary>
    internal static class InitializeAndClear
    {
        /// <summary>
        /// 初始化
        /// </summary>
        internal static void Initialize()
        {
            ModelManager.Instance.GetParameter = ConfigControl.LoadConfig();

#if UNITY_EDITOR
            string[] filesName = Tools.PathTools.GetFileName(Tools.PathTools.GetAudioPath).
                Where((str) => Path.GetExtension(str) != ".meta").
                Select((audioName) => Path.GetFileName(audioName)).ToArray();
#else
                string[] filesName = Tools.PathTools.GetFileName(Tools.PathTools.GetAudioPath).
                Select(str => Path.GetFileName(str)).ToArray();
#endif
            ModelManager.Instance.GetLogicDatas = new LogicDatas
            {
                GameState = GameState.Init,
                SongsName = filesName,
                LyricInfo = null,
                CycleType = CycleType.LoopPlayback,
                Index = 0,
                Timer = 0,
                CurrentSongIndex = 0
            };
            Material audioDataMaterial = Resources.Load<Material>("Materials/AudioDataMaterial");
            ModelManager.Instance.GetScenesDatas = new ScenesDatas
            {
                AudioSource = GameObject.FindObjectOfType<AudioSource>(),
                VideoPlayer = GameObject.FindObjectOfType<UnityEngine.Video.VideoPlayer>(),
                AudioDataMaterial = audioDataMaterial,
                AudioDatas = InitializeAndClear.InitAudioDatas(audioDataMaterial),
                TrailRenderer = GameObject.FindObjectOfType<TrailRenderer>().transform,
                LyricItems = GameObject.Find("LyricLayer").transform.GetComponentsInChildren<Text>(),
                SongName = GameObject.Find("SongName").transform.GetComponent<Text>(),
                SongTotalTime = GameObject.Find("SongTotalTime").transform.GetComponent<Text>(),
                SongPlayingTime = GameObject.Find("SongPlayingTime").transform.GetComponent<Text>(),
                SongTimeSlide = GameObject.Find("SongTimeSlider").transform.GetComponent<Slider>()
            };

            ControllerManager controllerManager = ControllerManager.Instance;
            controllerManager.RegisterOnUpdate(SongControl.ListeningSongOver);
            controllerManager.RegisterOnUpdate(AudioVisualizer.RefreshHeartbeat);
            controllerManager.RegisterOnUpdate(SongLyric.ListeningSongLyric);
            controllerManager.RegisterOnUpdate(MouseTrail.RefreshTrail);
            controllerManager.RegisterOnUpdate(AssetsControl.RefreshAssets);
            controllerManager.RegisterOnUpdate(SongControl.UpdateSongPlayingTime);

            Parameter parameter = ModelManager.Instance.GetParameter;
            ScenesDatas scenesDatas = ModelManager.Instance.GetScenesDatas;
            UILyricControl.ChangeBaseLyricColor(parameter.BaseFontColor, scenesDatas.LyricItems);
            UILyricControl.ChangeCurrentLyricColor(parameter.CurrentFontColor, scenesDatas.LyricItems);
            UILyricControl.ChangeLyricFontSize(parameter.FontSize, scenesDatas.LyricItems);

            ModelManager.Instance.GetLogicDatas.GameState = GameState.StandBy;
            //Controller.SongControl.PlayOrPauseSong();
        }

        /// <summary>
        /// 初始化音频可视化数据
        /// </summary>
        private static Transform[] InitAudioDatas(Material audioDataMaterial)
        {
            Parameter parameter = ModelManager.Instance.GetParameter;
            GameObject audioDatalayer = GameObject.Find("AudioDataLayer");
            audioDatalayer.transform.position = new Vector3(parameter.OffsetX, parameter.OffsetY, audioDatalayer.transform.position.z);
            Transform[] audioDatas = new Transform[parameter.AudioDataCount];
            GameObject audioData = Resources.Load<GameObject>("Prefabs/AudioData");

            //角度间隔
            float angle = parameter.AngleInterval;
            //大小
            Vector2 localScale = new Vector3(parameter.LineWidth, parameter.MinHight, 1.0f);
            string name = "AudioData ";
            //此处更改等同于audioDatas[0]的Renderer.sharedMaterial（代码需移动到for循环下），其余脚本相同
            audioDataMaterial.SetColor("_MainColor", parameter.MainColor);
            audioDataMaterial.SetColor("_ViceColor", parameter.ViceColor);
            audioDataMaterial.SetFloat("_Brightness", parameter.ColorBrightness);
            float rotateAngle = 360.0f / parameter.AudioDataCount;
            for (int i = 0; i < parameter.AudioDataCount; i++)
            {
                audioDatas[i] = GameObject.Instantiate<GameObject>(audioData, audioDatalayer.transform, false).transform;
                audioDatas[i].localPosition = new Vector3(Mathf.Sin(angle * i) * parameter.Radius, Mathf.Cos(angle * i) * parameter.Radius, 0.0f);
                audioDatas[i].localEulerAngles = new Vector3(0.0f, 0.0f, -i * rotateAngle);
                audioDatas[i].localScale = localScale;
                audioDatas[i].gameObject.name = name + i.ToString();
            }
            return audioDatas;
        }

        /// <summary>
        /// 清空
        /// </summary>
        internal static void Clear()
        {
            ControllerManager controllerManager = ControllerManager.Instance;
            controllerManager.UnRegisterOnUpdate(SongControl.ListeningSongOver);
            controllerManager.UnRegisterOnUpdate(AudioVisualizer.RefreshHeartbeat);
            controllerManager.UnRegisterOnUpdate(SongLyric.ListeningSongLyric);
            controllerManager.UnRegisterOnUpdate(MouseTrail.RefreshTrail);
            controllerManager.UnRegisterOnUpdate(AssetsControl.RefreshAssets);
            controllerManager.UnRegisterOnUpdate(SongControl.UpdateSongPlayingTime);

            ModelManager.Instance.GetLogicDatas = null;
            ModelManager.Instance.GetParameter = null;
            ModelManager.Instance.GetScenesDatas = null;
        }
    }
}