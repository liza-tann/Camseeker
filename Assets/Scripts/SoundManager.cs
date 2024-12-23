// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;

// public class SoundManager : MonoBehaviour
// {
//     [SerializeField] Slider volumeSlider;
//     // Start is called before the first frame update
//     void Start()
//     {
//         if (!PlayerPrefs.HasKey("musicVoulme"))
//         {
//             PlayerPrefs.SetFloat("musicVolume", 1);
//         }
//         else
//         {
//             Load();
//         }
//     }

//     // Update is called once per frame
//     public void ChangeVolume()
//     {
//         AudioListener.volume = volumeSlider.value;
//         Save();
//     }

//     private void Load()
//     {
//         volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
//     }

//     private void Save()
//     {
//         PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
//     }
// }

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [SerializeField] Slider volumeSlider;

    void Start()
    {
        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 0.5f); // Set default volume to 0.5
            AudioListener.volume = 0.5f; // Apply the default volume
            volumeSlider.value = 0.5f; // Set the slider to 0.5
        }
        else
        {
            Load();
        }
    }

    public void ChangeVolume()
    {
        AudioListener.volume = volumeSlider.value;
        Save();
    }

    private void Load()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
        AudioListener.volume = volumeSlider.value; // Ensure the AudioListener matches the saved volume
    }

    private void Save()
    {
        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
    }
}
