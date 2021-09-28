using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    [Header("����t��"), Range(0, 1000)]
    public int turn = 100;

    private Transform cam;
    private Transform target;
    
    [HideInInspector]
    public Vector3 posForward;

    private void Start()
    {
        cam = transform.Find("Main Camera");
        target = GameObject.Find("���Q�i�S").transform;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, 0.3f);
        posForward = transform.position + transform.forward * 10;
        posForward.y = target.position.y;
        Gizmos.DrawSphere(posForward, 0.3f);
    }

    private void Update()
    {
        TurnCamera("Mouse X", transform.up);
        TurnCamera("Mouse Y", transform.right);

        transform.position = target.position;

        Vector3 euler = transform.eulerAngles;
        euler.z = 0;
        transform.eulerAngles = euler;

        CameraLimitUp();
    }

    /// <summary>
    /// ������v��
    /// </summary>
    private void TurnCamera(string axis, Vector3 direction)
    {
        transform.Rotate(Input.GetAxis(axis) * direction * turn * Time.deltaTime);
    }

    private void CameraLimitUp()
    {
        Quaternion q = transform.rotation;
        q.x = Mathf.Clamp(q.x, -0.3f, 0.3f);
        transform.rotation = q;
    }
}
