// using UnityEngine;
// using TMPro;

// public class NPCInteractionTMP : MonoBehaviour
// {
//     public Transform player; // Reference to the player's transform
//     public float interactionRange = 0.5f; // Range within which interaction is triggered
//     public Canvas interactionUI; // Reference to the UI Canvas
//     public TextMeshProUGUI dialogueText; // Reference to TMP text for the NPC dialogue

//     void Start()
//     {
//         // Ensure the UI is hidden at the start
//         interactionUI.gameObject.SetActive(false);
//         Debug.Log(player.name);
//     }

//     void Update()
//     {
//         float distance = Vector3.Distance(player.position, transform.position);
//         Debug.Log($"Distance to NPC: {distance}");
//         Debug.Log(player.name);

//         if (distance <= interactionRange)
//         {
//             Debug.Log("Within range: Showing UI");
//             interactionUI.gameObject.SetActive(true);
//             dialogueText.text = "Press the button to talk!";
//         }
//         else
//         {
//             Debug.Log("Out of range: Hiding UI");
//             interactionUI.gameObject.SetActive(false);
//         }
//     }



// }


////last one
using UnityEngine;
using TMPro; // For TextMeshPro
using Cinemachine;

public class NPCInteractionTMP : MonoBehaviour
{
    public Transform player;             // Reference to the player object (bot)
    public Transform interactionUI;      // Reference to the UI interaction area
    public float interactionRange = 5f;  // Range for the interaction
    public GameObject dialogueBox;       // Dialogue UI element
    public TextMeshProUGUI dialogueText; // TextMeshPro component for dialogue
    public string dialogueMessage = "Hello! How can I help you?";

    public CinemachineVirtualCamera camNPC;
    public CinemachineVirtualCamera camPlayer;

    void Start()
    {
        // Ensure the dialogue box is hidden at the start of the game
        dialogueBox.SetActive(false);
        // Debug.Log("Dialogue box hidden at start.");
    }

    void Update()
    {
        // Calculate the distance between the player and the interaction UI
        float distance = Vector3.Distance(player.position, interactionUI.position);

        // Debug log to monitor the distance
        //Debug.Log($"Distance to Interaction UI: {distance}");

        // Check if the player is within range
        if (distance <= interactionRange)
        {
            // Debug.Log("Player is within range of the Interaction UI.");
            ShowDialogue();
            SwitchToNPCPov();
        }
        else
        {
            // Debug.Log("Player is out of range.");
            HideDialogue();
            CloseNPCPov();
        }
    }

    void ShowDialogue()
    {
        if (!dialogueBox.activeSelf)
        {
            dialogueBox.SetActive(true);  // Show the dialogue box
            dialogueText.text = dialogueMessage; // Set the dialogue message
            //Debug.Log("Dialogue box shown.");
        }
    }

    void HideDialogue()
    {
        if (dialogueBox.activeSelf)
        {
            dialogueBox.SetActive(false); // Hide the dialogue box
            //Debug.Log("Dialogue box hidden.");
        }
    }

    void SwitchToNPCPov()
    {
        CameraManager.SwitchCamera(camNPC);
    }

    void CloseNPCPov()
    {
        CameraManager.SwitchCamera(camPlayer);
    }
}
