using UnityEngine;

public class GroundFloorSceneManager : MonoBehaviour
{
    public GameObject[] characterPrefabs; // Array of character prefabs
    private GameObject activeCharacter;   // Currently loaded character

    void Start()
    {
        // Get the selected character index from PlayerPrefs
        int selectedCharacterIndex = PlayerPrefs.GetInt("SelectedCharacterIndex", 0);

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
        activeCharacter = Instantiate(characterPrefabs[selectedCharacterIndex]);
        activeCharacter.transform.position = new Vector3(48.23f, 0.19f, 27.33f);
        activeCharacter.transform.rotation = Quaternion.Euler(0, 88, 0);
        activeCharacter.transform.localScale = new Vector3(1, 1, 1);

        // Add the CharacterMover script to enable movement
        activeCharacter.AddComponent<CharacterMover>();
    }
}
