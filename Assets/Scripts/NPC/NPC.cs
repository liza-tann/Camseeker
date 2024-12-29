using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class NPC : MonoBehaviour
{
    [System.Serializable]
    public class InteractionUI
    {
        public Sprite characterPortrait;
        public string[] sentences;
    }

    public Transform player; // Reference to the player's transform
    public float interactionRange = 5f; // Range within which interaction is triggered
    public Canvas interactionUI; // Reference to the UI Canvas
    public TextMeshProUGUI dialogueText;
    public Image characterPortrait;
    public InteractionUI interactionUIData; // Reference to the InteractionUI data

    public Animator animator; // Reference to the animator for dialogue animations

    public float typingSpeed = 0.05f; // Adjust typing speed as needed

    private Queue<string> sentences = new Queue<string>();

    private void Update()
    {
        float distance = Vector3.Distance(player.position, transform.position);
        //Debug.Log($"Distance to player: {distance}");

        if (distance <= interactionRange)
        {
            interactionUI.gameObject.SetActive(true);
            dialogueText.text = "Press E to interact!";
            //Debug.Log("within range: showing UI");
        }
        else
        {
            interactionUI.gameObject.SetActive(false);
            Debug.Log("Out of range: Hiding UI");
        }

        Debug.Log($"Player Position: {player.position}");
        Debug.Log($"NPC Position: {transform.position}");
        Debug.Log($"Distance: {distance}");
    }

    public void StartDialogue(InteractionUI dialogueData)
    {
        animator.SetBool("IsOpen", true); // Trigger dialogue animation

        characterPortrait.sprite = dialogueData.characterPortrait;

        sentences.Clear();
        foreach (string sentence in dialogueData.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();

        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence)
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public void EndDialogue()
    {
        animator.SetBool("IsOpen", false); // Trigger end dialogue animation
        dialogueText.text = "";
    }
}