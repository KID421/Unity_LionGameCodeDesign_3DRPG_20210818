using UnityEngine;

namespace KID
{
    /// <summary>
    /// KID 2021.0906
    /// �ĤT�H�ٱ��
    /// ���ʡB���D
    /// </summary>
    public class ThirdPersonController : MonoBehaviour
    {
        #region ��� Field
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
        public string animatorParJump = "���DĲ�o";
        public string animatorParIsGrounded = "�O�_�b�a�O�W";
        [Header("���a�C������")]
        public GameObject playerObject;

        #region ���G�p�H
        private AudioSource aud;
        private Rigidbody rig;
        private Animator ani;
        #endregion
        #endregion

        #region �ݩ� Property
        /// <summary>
        /// ���D����
        /// </summary>
        private bool keyJump { get => Input.GetKeyDown(KeyCode.Space); }
        /// <summary>
        /// �H�����q
        /// </summary>
        private float volumeRandom { get => Random.Range(0.7f, 1.2f); }
        #endregion

        #region ��k Method
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="speedMove">���ʳt��</param>
        private void Move(float speedMove)
        {
            // �Ш��� Animator �ݩ� Apply Root Motion�G�Ŀ�ɨϥΰʵe�첾��T
            // ����.�[�t�� = �T���V�q - �[�t�ץΨӱ������T�Ӷb�V���B�ʳt��
            // �e�� * ��J�� * ���ʳt��
            // �ϥΫe�ᥪ�k�b�V�B�ʨåB�O���쥻���a�ߤޤO
            rig.velocity =
                Vector3.forward * MoveInput("Vertical") * speedMove +
                Vector3.right * MoveInput("Horizontal") * speedMove +
                Vector3.up * rig.velocity.y;
        }

        /// <summary>
        /// ���ʫ����J
        /// </summary>
        /// <param name="axisName">�n���o���b�V�W��</param>
        /// <returns>���ʫ����</returns>
        private float MoveInput(string axisName)
        {
            return Input.GetAxis(axisName);
        }

        /// <summary>
        /// �ˬd�a�O
        /// </summary>
        /// <returns>�O�_�I��a�O</returns>
        private bool CheckGround()
        {
            // ���z.�л\�y��(�����I�A�b�|�A�ϼh)
            Collider[] hits = Physics.OverlapSphere(
                transform.position +
                transform.right * v3CheckGroudOffset.x +
                transform.up * v3CheckGroudOffset.y +
                transform.forward * v3CheckGroudOffset.z,
                checkGroundRadius, 1 << 3);

            //print("�y��I�쪺�Ĥ@�Ӫ���G" + hits[0].name);

            // �p�G �|�����a �åB ���a�I���}�C�j�� 0 �N ����@������
            if (!isGrounded && hits.Length > 0) aud.PlayOneShot(soundGround, volumeRandom);

            isGrounded = hits.Length > 0;

            // �Ǧ^ �I���}�C�ƶq > 0 - �u�n�I����w�ϼh����N�N��b�a���W
            return hits.Length > 0;
        }

        /// <summary>
        /// ���D
        /// </summary>
        private void Jump()
        {
            // print("�O�_�b�a���W�G" + CheckGround());

            // �åB &&
            // �p�G �b�a���W �åB ���U�ť��� �N ���D
            if (CheckGround() && keyJump)
            {
                // ����.�K�[���O(�����󪺤W�� * ���D)
                rig.AddForce(transform.up * jump);

                aud.PlayOneShot(soundJump, volumeRandom);
            }
        }

        /// <summary>
        /// ��s�ʵe
        /// </summary>
        private void UpdateAnimation()
        {
            ani.SetBool(animatorParWalk, MoveInput("Vertical") != 0 || MoveInput("Horizontal") != 0);
            // �]�w�O�_�b�a�O�W �ʵe�Ѽ�
            ani.SetBool(animatorParIsGrounded, isGrounded);
            // �p�G ���U ���D�� �N �]�w���DĲ�o�Ѽ�
            // �P�_�� �u���@��ԭz(�u���@�Ӥ���) �i�H�ٲ� �j�A��
            if (keyJump) ani.SetTrigger(animatorParJump);
        }
        #endregion

        #region �ƥ� Event
        private void Start()
        {
            // �n���o�}�����C������i�H�ϥ�����r gameObject

            // ���o���󪺤覡
            // 1. �������W��.���o����(����(��������)) ��@ ��������;
            aud = playerObject.GetComponent(typeof(AudioSource)) as AudioSource;
            // 2. ���}���C������.���o����<�x��>();
            rig = gameObject.GetComponent<Rigidbody>();
            // 3. ���o����<�x��>();
            // ���O�i�H�ϥ��~�����O(�����O)�������A���}�ΫO�@ ���B�ݩʻP��k
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

            // transform �P���}���b�P���h�� Transform ����
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
