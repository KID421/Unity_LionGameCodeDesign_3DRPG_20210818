using UnityEngine;
using System.Collections;

namespace KID.Enemy
{
    /// <summary>
    /// �ĤH�欰
    /// �ĤH���A�G���ݡB�����B�l�ܡB�����B���ˡB���`
    /// </summary>
    public class Enemy : MonoBehaviour
    {
        #region ���G���}
        [Header("���ʳt��"), Range(0, 20)]
        public float speed = 2.5f;
        [Header("�����O"), Range(0, 200)]
        public float attack = 35;
        [Header("�d��G�l�ܻP����")]
        [Range(0, 7)]
        public float rangeAttack = 5;
        [Range(7, 20)]
        public float rangeTrack = 15;
        [Header("�����H������")]
        public Vector2 v2RandomWait = new Vector2(1f, 5f);
        #endregion

        #region ���G�p�H
        [SerializeField]    // �ǦC�����G��ܨp�H���
        private StateEnemy state;
        #endregion

        #region ø�s�ϧ�
        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(1, 0, 0.2f, 0.3f);
            Gizmos.DrawSphere(transform.position, rangeAttack);

            Gizmos.color = new Color(0.2f, 1, 0, 0.3f);
            Gizmos.DrawSphere(transform.position, rangeTrack);
        }
        #endregion

        #region �ƥ�
        private void Update()
        {
            StateManager();
        }
        #endregion

        #region ��k�G�p�H
        /// <summary>
        /// ���A�޲z
        /// </summary>
        private void StateManager()
        {
            switch (state)
            {
                case StateEnemy.Idle:
                    Idle();
                    break;
                case StateEnemy.Walk:
                    break;
                case StateEnemy.Track:
                    break;
                case StateEnemy.Attack:
                    break;
                case StateEnemy.Hurt:
                    break;
                case StateEnemy.Dead:
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// �O�_���ݪ��A
        /// </summary>
        private bool isIdle;

        /// <summary>
        /// ���ݡG�H�����ƫ�i���������A
        /// </summary>
        private void Idle()
        {
            #region �i�J����
            if (isIdle) return;

            isIdle = true;
            #endregion

            print("����");
            StartCoroutine(IdleEffect());
        }

        private IEnumerator IdleEffect()
        {
            float randomWait = Random.Range(v2RandomWait.x, v2RandomWait.y);
            yield return new WaitForSeconds(randomWait);

            state = StateEnemy.Walk;        // �i�J�������A

            #region �X�h����
            isIdle = false;
            #endregion
        }
        #endregion
    }
}