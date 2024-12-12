using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingScene : MonoBehaviour
{
    // This method will be triggered when the "Start" button is clicked
    public void OnPlayButtonClicked()
    {
        SceneManager.LoadScene("groundFloor"); // Replace with your StartGame scene name
    }

    public void OnBackToMainMenuButtonClicked()
    {
        SceneManager.LoadScene("MainMenu"); // Replace with your MainMenu scene name
    }
}