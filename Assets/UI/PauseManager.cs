using UnityEngine;

public class PauseManager : MonoBehaviour
{
    bool _isPauseFlag = false;

    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        { 
            PauseResume();
        }

        void PauseResume()
        {
            _isPauseFlag = !_isPauseFlag;

            //IPause���p�����Ă���I�u�W�F�N�g�����ׂČĂ�
            var objects = FindObjectsOfType<GameObject>();

            foreach (var o in objects)
            { 
                IPause i = o.GetComponent<IPause>();

                if (_isPauseFlag)
                {
                    i?.Pause();
                }
                else
                {
                    i?.Resume();
                }
            }
        }
    }
}
