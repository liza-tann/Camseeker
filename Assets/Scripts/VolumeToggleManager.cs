// using UnityEngine;

// public class VolumeToggleManager : MonoBehaviour
// {
//     [SerializeField] private GameObject volumeUI; // Reference to the VolumeUI GameObject

//     void Start()
//     {
//         // Ensure VolumeUI is hidden at the start of the game
//         volumeUI.SetActive(false);
//     }

//     // This method is called when the Volume Button is clicked
//     public void ToggleVolumeUI()
//     {
//         // Toggle the active state of the VolumeUI
//         volumeUI.SetActive(!volumeUI.activeSelf);
//     }
// }

using UnityEngine;
using UnityEngine.EventSystems;

public class VolumeToggleManager : MonoBehaviour
{
    [SerializeField] private GameObject volumeUI; // Reference to the VolumeUI GameObject
    [SerializeField] private Canvas volumeCanvas; // Reference to the Canvas containing the VolumeUI

    private bool isVolumeUIOpen = false;

    void Start()
    {
        // Ensure VolumeUI is hidden at the start of the game
        volumeUI.SetActive(false);
    }

    void Update()
    {
        // Detect touch/click outside of the VolumeUI when it is open
        if (isVolumeUIOpen && Input.GetMouseButtonDown(0))
        {
            if (!IsPointerOverUIElement())
            {
                HideVolumeUI();
            }
        }

        // Uncomment the following for touch devices:
        // if (isVolumeUIOpen && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        // {
        //     if (!IsPointerOverUIElement())
        //     {
        //         HideVolumeUI();
        //     }
        // }
    }

    public void ToggleVolumeUI()
    {
        if (!isVolumeUIOpen)
        {
            ShowVolumeUI();
        }
        else
        {
            HideVolumeUI();
        }
    }

    private void ShowVolumeUI()
    {
        volumeUI.SetActive(true);
        isVolumeUIOpen = true;
    }

    private void HideVolumeUI()
    {
        volumeUI.SetActive(false);
        isVolumeUIOpen = false;
    }

    private bool IsPointerOverUIElement()
    {
        // Check if the pointer (mouse or touch) is over any UI element
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;

        var results = new System.Collections.Generic.List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);

        // Check if the hit UI elements belong to the VolumeUI or its canvas
        foreach (var result in results)
        {
            if (result.gameObject == volumeUI || result.gameObject.transform.IsChildOf(volumeUI.transform))
            {
                return true;
            }
        }

        return false;
    }
}
