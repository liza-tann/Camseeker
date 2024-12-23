using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelection : MonoBehaviour
{
    public GameObject[] characterModels;  // Array for all character models
    private GameObject currentCharacter;  // Currently displayed character
    private int selectedCharacterIndex = 0;  // Default selection

    void Start()
    {
        int savedIndex = PlayerPrefs.GetInt("SelectedCharacterIndex", 0); // Default to 0 if not found
        Debug.Log("Currently loaded character index: " + savedIndex);

        // Initialize default character
        if (characterModels.Length > 0)
        {
            currentCharacter = Instantiate(characterModels[savedIndex], new Vector3(594f, 430f, -10f), Quaternion.Euler(0f, 180f, 0f));
            currentCharacter.transform.localScale = new Vector3(540f, 500f, 20f);
        }
        else
        {
            Debug.LogWarning("No character models assigned!");
        }
    }

    public void SelectCharacter(int index)
    {
        // Check if the index is within bounds
        if (index >= 0 && index < characterModels.Length)
        {
            if (currentCharacter != null)
            {
                Destroy(currentCharacter);  // Remove the old character model
            }

            selectedCharacterIndex = index;
            currentCharacter = Instantiate(characterModels[selectedCharacterIndex], new Vector3(594f, 430f, -10f), Quaternion.Euler(0f, 180f, 0f));
            currentCharacter.transform.localScale = new Vector3(540f, 500f, 20f);

            // Save the selected index
            PlayerPrefs.SetInt("SelectedCharacterIndex", selectedCharacterIndex);
            PlayerPrefs.Save(); // Ensure it's saved immediately
            Debug.Log("Selected Character Index: " + selectedCharacterIndex); // Log the index
        }
        else
        {
            Debug.LogWarning("Selected character index is out of range!");
        }
    }

    public void OnPlayButtonClicked()
    {
        PlayerPrefs.SetInt("SelectedCharacterIndex", selectedCharacterIndex); // Save the index
        PlayerPrefs.Save(); // Ensure it’s immediately written to storage
        Debug.Log("Saving Selected Character Index: " + selectedCharacterIndex); // Log the saved index
        SceneManager.LoadScene("GroundFloor"); // Switch to the gameplay scene
    }

    public void OnBackButtonClicked()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
