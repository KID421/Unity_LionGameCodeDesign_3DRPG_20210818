using UnityEngine;          // �ޥ� Unity API (�ܮw - ��ƻP�\��)
using UnityEngine.Video;    // �ޥ� �v�� API

// �׹��� ���O ���O�W�� : �~�����O
// MonoBehaviour�GUnity �����O�A�n���b����W�@�w�n�~��
// �~�ӫ�|�ɦ������O������
// �b���O�H�Φ����W��K�[�T���׽u�|�K�[�K�n
// �`�Φ����G��� Field�B�ݩ� Property (�ܼ�)�B��k Method�B�ƥ� Event
/// <summary>
/// KID 2021.0906
/// �ĤT�H�ٱ��
/// ���ʡB���D
/// </summary>
public class ThirdPersonController : MonoBehaviour
{
    #region ��� Field
    // �x�s�C����ơA�Ҧp�G���ʳt�סB���D���׵���...
    // �`�Υ|�j�����G��� int�B�B�I�� float �B�r�� string�B���L�� bool
    // ���y�k�G�׹��� ������� ���W�� (���w �w�]��) ����
    // �׹����G
    // 1. ���} public  - ���\��L���O�s�� - ��ܦb�ݩʭ��O - �ݭn�վ㪺��Ƴ]�w�����}
    // 2. �p�H private - �T���L���O�s�� - ���æb�ݩʭ��O - �w�]��
    // �� Unity �H�ݩʭ��O��Ƭ��D
    // �� ��_�{���w�]�ȽЫ� ... > Reset
    // ����ݩ� Attribute�G���U�����
    // ����ݩʻy�k�G[�ݩʦW��(�ݩʭ�)]
    // Header ���D
    // Tooltip ���ܡG�ƹ����d�b���W�٤W�|��ܼu�X����
    // Range �d��G�i�ϥΦb�ƭ�������ƤW�A�Ҧp�Gint, float
    [Header("���ʳt��"), Tooltip("�Ψӽվ㨤�Ⲿ�ʳt��"), Range(1, 500)]
    public float speed = 10.5f;
    [Header("���D����"), Range(0, 1000)]
    public int jump = 100;
    [Header("�ˬd�a�����")]
    [Tooltip("�Ψ��ˬd����O�_�b�a���W")]
    public bool isGrounded;
    public Vector3 v3CheckGroudOffset;
    [Range(0, 3)]
    public float checkGroundRadius = 0.2f;
    [Header("�����ɮ�")]
    public AudioClip soundJump;
    public AudioClip soundGround;
    [Header("�ʵe�Ѽ�")]
    public string animatorParWalk = "�����}��";
    public string animatorParRun = "�]�B�}��";
    public string animatorParHurt = "����Ĳ�o";
    public string animatorParDead = "���`�}��";

    private AudioSource aud;
    private Rigidbody rig;
    private Animator ani;

    #region Unity �������
    /** �m�� Unity �������
    // �C�� Color
    public Color color;
    public Color white = Color.white;                       // �����C��
    public Color yellow = Color.yellow;
    public Color color1 = new Color(0.5f, 0.5f, 0);         // �ۭq�C�� RGB
    public Color color2 = new Color(0, 0.5f, 0.5f, 0.5f);   // �ۭq�C�� RGBA

    // �y�� Vector 2 - 4
    public Vector2 v2;
    public Vector2 v2Right = Vector2.right;
    public Vector2 v2Left = Vector2.left;
    public Vector2 v2Up = Vector2.up;
    public Vector2 v2One = Vector2.one;
    public Vector2 v2Custom = new Vector2(7.5f, 100.9f);
    public Vector3 v3 = new Vector3(1, 2, 3);
    public Vector3 v3Forward = Vector3.forward;
    public Vector4 v4 = new Vector4(1, 2, 3, 4);

    // ���� �C�|��� enum
    public KeyCode key;
    public KeyCode move = KeyCode.W;
    public KeyCode jump = KeyCode.Space;

    // �C����������G������w�w�]��
    // �s�� Project �M�פ������
    public AudioClip sound;     // ���� mp3, ogg, wav
    public VideoClip video;     // �v�� mp4,
    public Sprite sprite;       // �Ϥ� png, jpeg - ���䴩 gif
    public Texture2D texture2D; // 2D �Ϥ� png, jpeg
    public Material material;   // ����y
    [Header("����")]
    // ���� Component�G�ݩʭ��O�W�i���|��
    public Transform tra;
    public Animation aniOld;
    public Animator aniNew;
    public Light lig;             
    public Camera cam;

    // ���L�C
    // 1. ��ĳ���n�ϥΦ��W��
    // 2. �ϥιL�ɪ� API
    /**/
    #endregion

    #endregion

    #region �ݩ� Property

    #endregion

    #region ��k Method

    #endregion

    #region �ƥ� Event
    // �S�w�ɶ��I�|���檺��k�A�{�����J�f Start ���� Console Main
    #endregion
}