using UnityEngine;

namespace KID
{
    /// <summary>
    /// 第三人稱攝影機系統
    /// 追蹤指定目標
    /// 並且可以左右、上下旋轉 (限制)
    /// </summary>
    public class ThirdPersonCamera : MonoBehaviour
    {
        #region 欄位
        [Header("目標物件")]
        public Transform target;
        [Header("追蹤速度"), Range(0, 500)]
        public float speedTrack = 1.5f;
        #endregion

        #region 屬性
        #endregion

        #region 事件
        #endregion

        #region 方法
        #endregion
    }
}