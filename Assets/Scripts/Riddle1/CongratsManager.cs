using UnityEngine;
using UnityEngine.UI;

public class CongratsManager : MonoBehaviour
{
    public Text ScoreText; // Reference to the Text component that will display the score
    public ParticleSystem confettiParticles; // Reference to the Particle System
    public AudioClip congratsSound; // Reference to the congratulatory sound clip
    private AudioSource audioSource; // Reference to AudioSource component


    void Start()
    {
        // Check if the score exists in PlayerPrefs
        if (PlayerPrefs.HasKey("Score"))
        {
            // Retrieve the score from PlayerPrefs
            int finalScore = PlayerPrefs.GetInt("Score");
            // Set the text to show the congratulatory message and the player's score
            ScoreText.text = $"{finalScore}";
        }
        else
        {
            // If no score is found, set a default message
            ScoreText.text = "0";
        }
        // Play the particle system when the congratulations screen is shown
        PlayCongratulationsParticles();
        PlayCongratsSound();

    }

    // Method to play the particle system
    void PlayCongratulationsParticles()
    {
        if (confettiParticles != null)
        {
            confettiParticles.Play(); // Play the particle system
        }
        else
        {
            Debug.LogWarning("No Particle System reference assigned.");
        }
    }
    private void PlayCongratsSound()
    {
        if (congratsSound != null)
        {
            // If the audio clip is assigned, play it
            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>(); // Add an AudioSource if not already present
            }

            audioSource.PlayOneShot(congratsSound); // Play the sound
        }
        else
        {
            Debug.LogWarning("No congratulatory sound assigned!");
        }
    }


    // Method to restart the quiz
    public void RestartQuiz()
    {
        // Load the quiz scene again
        UnityEngine.SceneManagement.SceneManager.LoadScene("RiddleScene");
    }

    // Method to quit the game
    public void QuitGame()
    {
        // Exit the game
        Application.Quit();
        Debug.Log("Quit Game");
    }
}
