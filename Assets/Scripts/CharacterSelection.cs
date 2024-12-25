using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelection : MonoBehaviour
{
    public GameObject[] characterModels; // Array of character prefabs
    private GameObject currentCharacter; // Currently selected character instance
    private int selectedCharacterIndex = 0; // Index of the selected character

    void Start()
    {
        // Load the previously selected character index or use the default (0)
        selectedCharacterIndex = PlayerPrefs.GetInt("SelectedCharacterIndex", 0);

        // Initialize the selected character model
        if (characterModels.Length > 0)
        {
            SpawnCharacter(selectedCharacterIndex);
        }
        else
        {
            Debug.LogWarning("No character models assigned!");
        }
    }

    public void SelectCharacter(int index)
    {
        // Check if the index is valid
        if (index >= 0 && index < characterModels.Length)
        {
            if (currentCharacter != null)
            {
                Destroy(currentCharacter); // Remove the current character
            }

            selectedCharacterIndex = index; // Update selected index
            SpawnCharacter(index); // Spawn the new character

            // Save the selected character index for future sessions
            PlayerPrefs.SetInt("SelectedCharacterIndex", selectedCharacterIndex);
            PlayerPrefs.Save();

            Debug.Log($"Character {index} selected: {characterModels[index].name}");
        }
        else
        {
            Debug.LogWarning("Invalid character index selected!");
        }
    }

    private void SpawnCharacter(int index)
    {
        // Instantiate the character model at a specific position and rotation
        currentCharacter = Instantiate(characterModels[index], new Vector3(594f, 430f, -10f), Quaternion.Euler(0f, 180f, 0f));
        currentCharacter.transform.localScale = new Vector3(540f, 500f, 20f);
    }

    public void OnPlayButtonClicked()
    {
        // Save the selected character index and load Scene2
        PlayerPrefs.SetInt("SelectedCharacterIndex", selectedCharacterIndex);
        PlayerPrefs.Save();
        Debug.Log($"Loading Scene2 with Character {selectedCharacterIndex}");
        SceneManager.LoadScene("Scene2");
    }

    public void OnBackButtonClicked()
    {
        // Load the MainMenu scene
        SceneManager.LoadScene("MainMenu");
    }
}
