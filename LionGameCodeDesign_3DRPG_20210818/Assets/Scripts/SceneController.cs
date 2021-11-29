using UnityEngine;
using UnityEngine.SceneManagement;

namespace KID
{
    /// <summary>
    /// ��������
    /// ���w�n�e�����ӳ���
    /// ���}��ӹC��
    /// </summary>
    public class SceneController : MonoBehaviour
    {
        /// <summary>
        /// ���J���w����
        /// </summary>
        /// <param name="nameScene">�����W��</param>
        public void LoadScene(string nameScene)
        {
            SceneManager.LoadScene(nameScene);
        }

        /// <summary>
        /// ���}�C��
        /// </summary>
        public void Quit()
        {
            Application.Quit();
        }
    }
}