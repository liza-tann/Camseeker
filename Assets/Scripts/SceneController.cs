using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public Vector3 playerPosition;
    public int score;

    public void LoadGame(string input)
    {
        SceneManager.LoadScene(input);
    }
}