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
// using UnityEngine;
// using TMPro; // For TextMeshPro

// public class NPCInteractionTMP : MonoBehaviour
// {
//     public Transform player;             // Reference to the player object (bot)
//     public Transform interactionUI;      // Reference to the UI interaction area
//     public float interactionRange = 5f;  // Range for the interaction
//     public GameObject dialogueBox;       // Dialogue UI element
//     public TextMeshProUGUI dialogueText; // TextMeshPro component for dialogue
//     public string dialogueMessage = "Hello! How can I help you?";

//     void Start()
//     {
//         // Ensure the dialogue box is hidden at the start of the game
//         dialogueBox.SetActive(false);
//         // Debug.Log("Dialogue box hidden at start.");
//     }

//     void Update()
//     {
//         // Calculate the distance between the player and the interaction UI
//         float distance = Vector3.Distance(player.position, interactionUI.position);

//         // Debug log to monitor the distance
//         //Debug.Log($"Distance to Interaction UI: {distance}");

//         // Check if the player is within range
//         if (distance <= interactionRange)
//         {
//             // Debug.Log("Player is within range of the Interaction UI.");
//             ShowDialogue();
//         }
//         else
//         {
//             // Debug.Log("Player is out of range.");
//             HideDialogue();
//         }
//     }

//     void ShowDialogue()
//     {
//         if (!dialogueBox.activeSelf)
//         {
//             dialogueBox.SetActive(true);  // Show the dialogue box
//             dialogueText.text = dialogueMessage; // Set the dialogue message
//             //Debug.Log("Dialogue box shown.");
//         }
//     }

//     void HideDialogue()
//     {
//         if (dialogueBox.activeSelf)
//         {
//             dialogueBox.SetActive(false); // Hide the dialogue box
//             //Debug.Log("Dialogue box hidden.");
//         }
//     }
// }

using System.Collections;
using UnityEngine;
using TMPro; // For TextMeshPro

public class NPCInteractionTMP : MonoBehaviour
{
    public Transform player;             // Reference to the player object (bot)
    public Transform interactionUI;      // Reference to the UI interaction area
    public float interactionRange = 5f;  // Range for the interaction
    public GameObject dialogueBox;       // Dialogue UI element
    public TextMeshProUGUI dialogueText; // TextMeshPro component for dialogue
    public string[] dialogueLines;       // Array of dialogue lines
    public float textSpeed = 0.05f;      // Speed of text typing

    private int dialogueIndex;           // Current dialogue line index
    private bool isTyping;               // Whether the text is currently being typed

    void Start()
    {
        // Ensure the dialogue box is hidden at the start of the game
        dialogueBox.SetActive(false);
    }

    void Update()
    {
        // Calculate the distance between the player and the interaction UI
        float distance = Vector3.Distance(player.position, interactionUI.position);

        // Check if the player is within range and presses the interaction key (e.g., left mouse button)
        if (distance <= interactionRange && Input.GetMouseButtonDown(0))
        {
            if (!dialogueBox.activeSelf)
            {
                ShowDialogue();
            }
            else
            {
                if (isTyping)
                {
                    // Skip typing and show the complete line
                    StopAllCoroutines();
                    dialogueText.text = dialogueLines[dialogueIndex];
                    isTyping = false;
                }
                else
                {
                    NextLine();
                }
            }
        }
        else if (distance > interactionRange && dialogueBox.activeSelf)
        {
            HideDialogue();
        }
    }

    void ShowDialogue()
    {
        dialogueBox.SetActive(true); // Show the dialogue box
        dialogueIndex = 0;           // Reset the dialogue index
        StartCoroutine(TypeLine()); // Start typing the first line
    }

    IEnumerator TypeLine()
    {
        isTyping = true;
        dialogueText.text = "";
        foreach (char c in dialogueLines[dialogueIndex].ToCharArray())
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
        isTyping = false;
    }

    void NextLine()
    {
        if (dialogueIndex < dialogueLines.Length - 1)
        {
            dialogueIndex++;
            StartCoroutine(TypeLine());
        }
        else
        {
            HideDialogue(); // End of dialogue
        }
    }

    void HideDialogue()
    {
        dialogueBox.SetActive(false); // Hide the dialogue box
    }
}
