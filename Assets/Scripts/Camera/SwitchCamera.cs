using UnityEngine;
using Cinemachine;

public class SwitchCamera : MonoBehaviour
{
    public CinemachineVirtualCamera cam1;
    public CinemachineVirtualCamera cam2;

    private bool isCam1Active = true; // Track the current active camera

    // Method to switch to Camera 1
    public void SwitchToCam1()
    {
        CameraManager.SwitchCamera(cam1);
        isCam1Active = true;
    }

    // Method to switch to Camera 2
    public void SwitchToCam2()
    {
        CameraManager.SwitchCamera(cam2);
        isCam1Active = false;
    }
}
