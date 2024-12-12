using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // This method will be triggered when the "Start" button is clicked
    public void OnStartButtonClicked()
    {
        SceneManager.LoadScene("groundFloor"); // Replace with your StartGame scene name
    }

    // This method will be triggered when the "Setting" button is clicked
    public void OnSettingButtonClicked()
    {
        SceneManager.LoadScene("SelectCharacter"); // Replace with your SelectCharacter scene name
    }

    // This method will be triggered when the "AboutUs" button is clicked
    public void OnAboutUsButtonClicked()
    {
        SceneManager.LoadScene("AboutUs"); // Replace with your AboutUs scene name
    }
}
