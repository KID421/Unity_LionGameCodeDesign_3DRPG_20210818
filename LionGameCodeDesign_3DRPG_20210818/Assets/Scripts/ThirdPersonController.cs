using UnityEngine;

namespace KID
{
    /// <summary>
    /// KID 2021.0906
    /// 第三人稱控制器
    /// 移動、跳躍
    /// </summary>
    public class ThirdPersonController : MonoBehaviour
    {
        #region 欄位 Field
        [Header("移動速度"), Tooltip("用來調整角色移動速度"), Range(1, 500)]
        public float speed = 10.5f;
        [Header("跳躍高度"), Range(0, 1000)]
        public int jump = 100;
        [Header("檢查地面資料")]
        [Tooltip("用來檢查角色是否在地面上")]
        public bool isGrounded;
        public Vector3 v3CheckGroudOffset;
        [Range(0, 3)]
        public float checkGroundRadius = 0.2f;
        [Header("音效檔案")]
        public AudioClip soundJump;
        public AudioClip soundGround;
        [Header("動畫參數")]
        public string animatorParWalk = "走路開關";
        public string animatorParRun = "跑步開關";
        public string animatorParHurt = "受傷觸發";
        public string animatorParDead = "死亡開關";
        public string animatorParJump = "跳躍觸發";
        public string animatorParIsGrounded = "是否在地板上";
        [Header("玩家遊戲物件")]
        public GameObject playerObject;

        #region 欄位：私人
        private AudioSource aud;
        private Rigidbody rig;
        private Animator ani;
        #endregion
        #endregion

        #region 屬性 Property
        /// <summary>
        /// 跳躍按鍵
        /// </summary>
        private bool keyJump { get => Input.GetKeyDown(KeyCode.Space); }
        /// <summary>
        /// 隨機音量
        /// </summary>
        private float volumeRandom { get => Random.Range(0.7f, 1.2f); }
        #endregion

        #region 方法 Method
        /// <summary>
        /// 移動
        /// </summary>
        /// <param name="speedMove">移動速度</param>
        private void Move(float speedMove)
        {
            // 請取消 Animator 屬性 Apply Root Motion：勾選時使用動畫位移資訊
            // 剛體.加速度 = 三維向量 - 加速度用來控制剛體三個軸向的運動速度
            // 前方 * 輸入值 * 移動速度
            // 使用前後左右軸向運動並且保持原本的地心引力
            rig.velocity =
                Vector3.forward * MoveInput("Vertical") * speedMove +
                Vector3.right * MoveInput("Horizontal") * speedMove +
                Vector3.up * rig.velocity.y;
        }

        /// <summary>
        /// 移動按鍵輸入
        /// </summary>
        /// <param name="axisName">要取得的軸向名稱</param>
        /// <returns>移動按鍵值</returns>
        private float MoveInput(string axisName)
        {
            return Input.GetAxis(axisName);
        }

        /// <summary>
        /// 檢查地板
        /// </summary>
        /// <returns>是否碰到地板</returns>
        private bool CheckGround()
        {
            // 物理.覆蓋球體(中心點，半徑，圖層)
            Collider[] hits = Physics.OverlapSphere(
                transform.position +
                transform.right * v3CheckGroudOffset.x +
                transform.up * v3CheckGroudOffset.y +
                transform.forward * v3CheckGroudOffset.z,
                checkGroundRadius, 1 << 3);

            //print("球體碰到的第一個物件：" + hits[0].name);

            // 如果 尚未落地 並且 落地碰撞陣列大於 0 就 播放一次音效
            if (!isGrounded && hits.Length > 0) aud.PlayOneShot(soundGround, volumeRandom);

            isGrounded = hits.Length > 0;

            // 傳回 碰撞陣列數量 > 0 - 只要碰到指定圖層物件就代表在地面上
            return hits.Length > 0;
        }

        /// <summary>
        /// 跳躍
        /// </summary>
        private void Jump()
        {
            // print("是否在地面上：" + CheckGround());

            // 並且 &&
            // 如果 在地面上 並且 按下空白鍵 就 跳躍
            if (CheckGround() && keyJump)
            {
                // 剛體.添加推力(此物件的上方 * 跳躍)
                rig.AddForce(transform.up * jump);

                aud.PlayOneShot(soundJump, volumeRandom);
            }
        }

        /// <summary>
        /// 更新動畫
        /// </summary>
        private void UpdateAnimation()
        {
            ani.SetBool(animatorParWalk, MoveInput("Vertical") != 0 || MoveInput("Horizontal") != 0);
            // 設定是否在地板上 動畫參數
            ani.SetBool(animatorParIsGrounded, isGrounded);
            // 如果 按下 跳躍鍵 就 設定跳躍觸發參數
            // 判斷式 只有一行敘述(只有一個分號) 可以省略 大括號
            if (keyJump) ani.SetTrigger(animatorParJump);
        }
        #endregion

        #region 事件 Event
        private void Start()
        {
            // 要取得腳本的遊戲物件可以使用關鍵字 gameObject

            // 取得元件的方式
            // 1. 物件欄位名稱.取得元件(類型(元件類型)) 當作 元件類型;
            aud = playerObject.GetComponent(typeof(AudioSource)) as AudioSource;
            // 2. 此腳本遊戲物件.取得元件<泛型>();
            rig = gameObject.GetComponent<Rigidbody>();
            // 3. 取得元件<泛型>();
            // 類別可以使用繼承類別(父類別)的成員，公開或保護 欄位、屬性與方法
            ani = GetComponent<Animator>();
        }

        private void Update()
        {
            Jump();
            UpdateAnimation();
        }

        private void FixedUpdate()
        {
            Move(speed);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(1, 0, 0.2f, 0.3f);

            // transform 與此腳本在同階層的 Transform 元件
            Gizmos.DrawSphere(
                transform.position +
                transform.right * v3CheckGroudOffset.x +
                transform.up * v3CheckGroudOffset.y +
                transform.forward * v3CheckGroudOffset.z,
                checkGroundRadius);
        }
        #endregion
    }
}
