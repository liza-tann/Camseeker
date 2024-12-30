using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public GameObject[] characterPrefabs; // Array of character prefabs
    private GameObject activeCharacter;   // Currently loaded character
    private Vector3 defaultPosition = new Vector3(44f, 0f, 20f); // Default spawn position
    private GameObject playerController;  // Reference to the player controller

    void Start()
    {
        // Get the selected character index from PlayerPrefs
        int selectedCharacterIndex = PlayerPrefs.GetInt("SelectedCharacterIndex", 0);

        playerController = GameObject.Find("PlayerController"); // Reference to Player controller

        if (playerController == null)
        {
            Debug.LogError("Player controller not found!");
            return;
        }

        if (characterPrefabs == null || characterPrefabs.Length == 0)
        {
            Debug.LogError("Character prefabs array is null or empty!");
            return;
        }

        if (selectedCharacterIndex < 0 || selectedCharacterIndex >= characterPrefabs.Length)
        {
            Debug.LogError("Selected character index is out of range!");
            return;
        }

        // Instantiate the selected character at the default position
        LoadCharacter(selectedCharacterIndex);
    }

    /// <summary>
    /// Replaces the currently active character with a new one.
    /// </summary>
    /// <param name="characterIndex">Index of the new character in the characterPrefabs array.</param>
    public void ReplaceCharacter(int characterIndex)
    {
        if (characterIndex < 0 || characterIndex >= characterPrefabs.Length)
        {
            Debug.LogError("Character index is out of range!");
            return;
        }

        // Destroy the current active character
        if (activeCharacter != null)
        {
            Destroy(activeCharacter);
        }

        // Load the new character
        LoadCharacter(characterIndex);

        // Save the selected character index to PlayerPrefs
        PlayerPrefs.SetInt("SelectedCharacterIndex", characterIndex);
        PlayerPrefs.Save();
    }

    /// <summary>
    /// Loads a character from the characterPrefabs array.
    /// </summary>
    /// <param name="characterIndex">Index of the character to load.</param>
    private void LoadCharacter(int characterIndex)
    {
        // Instantiate the new character prefab under the "Player controller"
        activeCharacter = Instantiate(characterPrefabs[characterIndex]);

        // Set the new character's position and parent it to the Player controller
        activeCharacter.transform.position = defaultPosition;
        activeCharacter.transform.SetParent(playerController.transform);

        // Optionally, you can add or remove any other necessary components (e.g., player scripts)
        // Add the CharacterMover script to enable movement if it's not already added
        if (activeCharacter.GetComponent<CharacterMover>() == null)
        {
            activeCharacter.AddComponent<CharacterMover>();
        }

        PlayerMovement playerMovement = playerController.GetComponent<PlayerMovement>();
        if (playerMovement != null)
        {
            playerMovement.SetAnimator(activeCharacter.GetComponent<Animator>());
        }
    }
}
