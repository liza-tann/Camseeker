using UnityEngine;

public class UIButtonHandler : MonoBehaviour
{
    public GameObject[] uiElements; // Array of UI elements to toggle

    void Start()
    {
        // Ensure all UI elements are initially inactive
        foreach (var uiElement in uiElements)
        {
            if (uiElement != null)
            {
                uiElement.SetActive(true);
            }
        }
    }

    // Method to toggle the active state of a specific UI element
    public void ToggleUIElement(GameObject uiElement)
    {
        if (uiElement != null)
        {
            // Toggle the active state of the specified UI element
            uiElement.SetActive(!uiElement.activeSelf);
        }
        else
        {
            Debug.LogError("UI element is not assigned!");
        }
    }

    // Method to toggle any UI element by index
    public void ToggleUIElement(int index)
    {
        if (index >= 0 && index < uiElements.Length && uiElements[index] != null)
        {
            uiElements[index].SetActive(!uiElements[index].activeSelf);
        }
        else
        {
            Debug.LogError("Invalid index or UI element not assigned!");
        }
    }
}
