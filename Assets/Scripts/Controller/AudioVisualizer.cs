using UnityEngine;
using AudioPlayer.Model;

namespace AudioPlayer.Controller
{
    /// <summary>
    /// 音频可视化
    /// </summary>
    internal static class AudioVisualizer
    {
        #region 可视化和环绕
        /// <summary>
        /// 心跳刷新
        /// </summary>
        /// <param name="data">数据</param>
        internal static void RefreshHeartbeat()
        {
            LogicDatas logicDatas = ModelManager.Instance.GetLogicDatas;
            logicDatas.Timer += Time.deltaTime;
            if (logicDatas.Timer > ModelManager.Instance.GetParameter.UpdateInterval)
            {
                logicDatas.Timer = 0;
                AudioVisualizer.Surround(ModelManager.Instance.GetScenesDatas.AudioDatas, ModelManager.Instance.GetParameter);
                if (logicDatas.GameState != GameState.Playing)
                    return;
                AudioVisualizer.UpdateAudioData(AudioDataDistributionType.DoubleSymmetry);
            }
        }

        /// <summary>
        /// 更新音频数据
        /// </summary>
        /// <param name="audioDataType">音频分布类型</param>
        private static void UpdateAudioData(AudioDataDistributionType audioDataType)
        {
            ScenesDatas scenesDatas = ModelManager.Instance.GetScenesDatas;
            float[] samples = new float[scenesDatas.AudioDatas.Length];
            Tools.AudioSourceData.GetCurrentSongData(scenesDatas.AudioSource, samples);
            Transform[] audioDatas = scenesDatas.AudioDatas;
            Parameter parameter = ModelManager.Instance.GetParameter;
            switch (audioDataType)
            {
                case AudioDataDistributionType.Asymmetric:
                    AudioVisualizer.OnAsymmetric(audioDatas, samples, parameter);
                    break;
                case AudioDataDistributionType.Symmetry:
                    AudioVisualizer.OnSymmetry(audioDatas, samples, parameter);
                    break;
                case AudioDataDistributionType.DoubleSymmetry:
                    AudioVisualizer.OnDoubleSymmetry(audioDatas, samples, parameter);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 环绕
        /// </summary>
        private static void Surround(Transform[] audioDatas, Parameter parameter)
        {
            for (int i = 0; i < audioDatas.Length; i++)
                audioDatas[i].transform.RotateAround(audioDatas[i].transform.parent.position, Vector3.forward, Time.deltaTime * parameter.RotateSpeed);
        }
        #endregion

        #region 音频分布事件
        /// <summary>
        /// 当非对称时
        /// </summary>
        /// <param name="audioDatas">音频可视化实体数据</param>
        /// <param name="samples">音频数据数组</param>
        /// <param name="parameter">参数</param>
        private static void OnAsymmetric(Transform[] audioDatas, float[] samples, Parameter parameter)
        {
            for (int i = 0; i < audioDatas.Length; i++)
            {
                float x = parameter.LineWidth;
                float y = samples[i] * parameter.AmplificationFactor;
                y = Mathf.Clamp(y, parameter.MinHight, parameter.MaxHight);
                Vector3 localScale = new Vector3(x, y, 1.0f);
                audioDatas[i].transform.localScale = localScale;
            }
        }

        /// <summary>
        /// 当对称时
        /// </summary>
        /// <param name="audioDatas">音频可视化实体数据</param>
        /// <param name="samples">音频数据数组</param>
        /// <param name="parameter">参数</param>
        private static void OnSymmetry(Transform[] audioDatas, float[] samples, Parameter parameter)
        {
            int halfIndex = audioDatas.Length / 2;
            for (int i = 0; i < halfIndex; i++)
            {
                float x = parameter.LineWidth;
                float y = samples[i] * parameter.AmplificationFactor;
                y = Mathf.Clamp(y, parameter.MinHight, parameter.MaxHight);
                Vector3 localScale = new Vector3(x, y, 1.0f);
                //[0 - 31]
                audioDatas[i].transform.localScale = localScale;
                //[32 - 63]
                audioDatas[halfIndex + i].transform.localScale = localScale;
            }
        }

        /// <summary>
        /// 当双对称时
        /// </summary>
        /// <param name="audioDatas">音频可视化实体数据</param>
        /// <param name="samples">音频数据数组</param>
        /// <param name="parameter">参数</param>
        private static void OnDoubleSymmetry(Transform[] audioDatas, float[] samples, Parameter parameter)
        {
            int halfIndex = audioDatas.Length / 4;
            for (int i = 0; i < halfIndex; i++)
            {
                float x = parameter.LineWidth;
                float y = samples[i] * parameter.AmplificationFactor;
                y = Mathf.Clamp(y, parameter.MinHight, parameter.MaxHight);
                Vector3 localScale = new Vector3(x, y, 1.0f);

                //[15 - 0]
                audioDatas[halfIndex - 1 - i].transform.localScale = localScale;
                //[16 - 31]
                audioDatas[halfIndex + i].transform.localScale = localScale;
                //[47 - 32]
                audioDatas[3 * halfIndex - i - 1].transform.localScale = localScale;
                //[48 - 63]
                audioDatas[3 * halfIndex + i].transform.localScale = localScale;
            }
        }
        #endregion
    }
}