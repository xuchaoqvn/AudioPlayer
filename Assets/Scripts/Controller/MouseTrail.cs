using UnityEngine;
using AudioPlayer.Model;

namespace AudioPlayer.Controller
{
    /// <summary>
    /// 鼠标尾迹
    /// </summary>
    internal static class MouseTrail
    {
        /// <summary>
        /// 刷新鼠标尾迹
        /// </summary>
        internal static void RefreshTrail()
        {
            Vector3 targetPostion = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0.5f));
            Transform trailRendererTransfrom = ModelManager.Instance.GetScenesDatas.TrailRenderer.transform;
            if (Vector3.SqrMagnitude(targetPostion - trailRendererTransfrom.position) < 0.1)
                return;
            trailRendererTransfrom.position = targetPostion;
        }
    }
}