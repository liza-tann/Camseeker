using UnityEngine;
using Cinemachine;

public class SwitchCamera : MonoBehaviour
{
    public CinemachineVirtualCamera cam1;
    public CinemachineVirtualCamera cam2;

    private bool isCam1Active = true; // Track the current active camera

    void Start()
    {
        // Initialize both cameras
        cam1.Priority = 10; // Make cam1 active
        cam2.Priority = 0;  // Make cam2 inactive
    }

    public void SwitchToCamera()
    {
        if (isCam1Active)
        {
            SwitchToCam2();
        }
        else
        {
            SwitchToCam1();
        }
    }

    public void SwitchToCam1()
    {
        cam1.Priority = 10; // Make cam1 active
        cam2.Priority = 0;  // Make cam2 inactive
        isCam1Active = true;
    }

    public void SwitchToCam2()
    {
        cam1.Priority = 0;  // Make cam1 inactive
        cam2.Priority = 10; // Make cam2 active
        isCam1Active = false;
    }
}