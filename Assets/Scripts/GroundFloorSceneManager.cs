using UnityEngine;

public class GroundFloorSceneManager : MonoBehaviour
{
    public GameObject[] characterPrefabs;  // Array of all character prefabs
    private GameObject activeCharacter;  // The currently loaded character

    void Start()
    {
        int selectedCharacterIndex = PlayerPrefs.GetInt("SelectedCharacterIndex", 0); // Default to 0 if not found
        Debug.Log("Currently loaded character index: " + selectedCharacterIndex);

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

        activeCharacter = Instantiate(characterPrefabs[selectedCharacterIndex]);
        activeCharacter.transform.position = new Vector3(50.3f, -0.1188011f, 27.4f);
        activeCharacter.transform.rotation = Quaternion.Euler(0, 88, 0);
        activeCharacter.transform.localScale = new Vector3(3, 3, 3);
    }
}
