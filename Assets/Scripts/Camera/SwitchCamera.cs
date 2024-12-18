using UnityEngine;
using Cinemachine;

public class SwitchCamera : MonoBehaviour
{
    public CinemachineVirtualCamera cam1;
    public CinemachineVirtualCamera cam2;

    private bool isCam1Active = true;

    public void Update()
    {
        SwitchInput();
    }

    public void SwitchInput()
    {
        if (Input.GetKeyDown("space"))
        {
            if (isCam1Active)
            {
                CameraManager.SwitchCamera(cam2);
            }
            else
            {
                CameraManager.SwitchCamera(cam1);
            }
            isCam1Active = !isCam1Active; // Toggle active camera state
        }
    }
}