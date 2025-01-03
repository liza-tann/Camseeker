using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AnswerScript : MonoBehaviour
{
    public bool isCorrect;
    public QuizManager quizManager; // Reference to the QuizManager

    private AudioSource audioSource;
    public void Answer()
    {
        if (isCorrect)
        {
            Debug.Log("Correct Answer");
            GetComponent<Image>().color = Color.green; // Set the button color to green for the correct answer

            if (audioSource != null && quizManager.correctAudio != null)
            {
                audioSource.PlayOneShot(quizManager.correctAudio); // Play the correct audio clip
            }

            quizManager.Correct(); // Call the Correct method to increase the score and proceed
        }
        else
        {
            Debug.Log("Wrong Answer");
            GetComponent<Image>().color = Color.red; // Set the button color to red for incorrect answer

            if (audioSource != null && quizManager.incorrectAudio != null)
            {
                audioSource.PlayOneShot(quizManager.incorrectAudio); // Play the incorrect audio clip
            }

            quizManager.Incorrect(); // Call the Incorrect method to proceed without adding points
        }
    }
}
