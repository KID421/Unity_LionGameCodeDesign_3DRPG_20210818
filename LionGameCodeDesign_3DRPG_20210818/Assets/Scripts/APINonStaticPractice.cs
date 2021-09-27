using UnityEngine;

/// <summary>
/// 練習非靜態 API
/// </summary>
public class APINonStaticPractice : MonoBehaviour
{
    public Camera cam;
    public SpriteRenderer sprSquare;
    public Camera camMain;
    public SpriteRenderer sprBird;
    public Transform bird1;
    public Rigidbody2D bird2;

    private void Start()
    {
        #region 非靜態屬性
        print("取得攝影機的深度：" + cam.depth);
        print("方形圖片的顏色：" + sprSquare.color);

        camMain.backgroundColor = Random.ColorHSV();
        sprBird.flipY = true;
        #endregion
    }

    private void Update()
    {
        #region 非靜態方法
        bird1.Rotate(0, 0, 3);
        bird2.AddForce(new Vector2(0, 10));
        #endregion
    }
}
