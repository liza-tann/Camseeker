using UnityEngine;

public class GroundFloorSceneManager1 : MonoBehaviour
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
        activeCharacter.transform.position = new Vector3(59f, 26f, -7f);
        activeCharacter.transform.rotation = Quaternion.Euler(0, 0, 0);
        activeCharacter.transform.localScale = new Vector3(5, 5, 5);

        // Add the CharacterMover script to enable movement
        activeCharacter.AddComponent<CharacterMover>();
    }
}
