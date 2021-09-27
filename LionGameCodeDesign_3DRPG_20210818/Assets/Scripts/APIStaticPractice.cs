using UnityEngine;

/// <summary>
/// 靜態屬性與方法 API 課堂練習
/// </summary>
public class APIStaticPractice : MonoBehaviour
{
    private void Start()
    {
        print("總共有幾架攝影機：" + Camera.allCamerasCount);    // 1
        print("2D 重力：" + Physics2D.gravity);                 // 0, -9.8
        print("圓周率：" + Mathf.PI);                           // 3.14159

        Physics2D.gravity = new Vector2(0, -20);
        Time.timeScale = 0.5f;
        print("9.999 去除小點結果：" + Mathf.Round(9.999f));    // 10

        Vector3 a = new Vector3(1, 1, 1);
        Vector3 b = new Vector3(22, 22, 22);
        print("a b 兩點的距離：" + Vector3.Distance(a, b));

        Application.OpenURL("https://unity.com/");
    }

    private void Update()
    {
        print("是否輸入任意鍵：" + Input.anyKey);
        print("時間：" + Time.time);
        print("是否按下空白鍵：" + Input.GetKeyDown(KeyCode.Space));
    }
}
