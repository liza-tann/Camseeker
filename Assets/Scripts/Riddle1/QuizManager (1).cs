using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizManager : MonoBehaviour
{
    public List<QuestionAndAnswer> QnA; // List of questions from JSON
    public GameObject[] options; // Array of answer buttons
    public int currentQuestion;

    public Text QuestionTxt; // Text component for displaying the question
    public Image[] QuestionImages; // Array of UI Images to display question images
    public Text ScoreText; // Reference to the Score Text

    private int score = 0; // Player's score

    public AudioClip correctAudio; // Audio for correct answer
    public AudioClip incorrectAudio; // Audio for incorrect answer
    private AudioSource audioSource; // AudioSource for playing sounds

    private void Start()
    {
        // Initialize the AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Validate options array
        // if (options == null || options.Length == 0)
        //{
        // Debug.LogError("Options array is not set up correctly!");
        //return;
        //}

        UpdateScoreUI();
        LoadQuestionsFromJSON(); // Load questions from the JSON file

        if (QnA == null || QnA.Count == 0)
        {
            Debug.LogError("No questions available in QnA list!");
        }
        else
        {
            GenerateQuestion(); // Start the quiz if there are questions
        }
    }

    public void Correct()
    {
        Debug.Log("Correct Answer Selected!");
        PlayAudio(correctAudio); // Play correct audio
        IncreaseScore(10); // Add 10 points for the correct answer
        MoveToNextQuestion(); // Move to the next question
    }

    public void Incorrect()
    {
        Debug.Log("Incorrect Answer Selected!");
        PlayAudio(incorrectAudio); // Play incorrect audio
        MoveToNextQuestion(); // Move to the next question without adding points
    }

    private void MoveToNextQuestion()
    {
        if (currentQuestion >= 0 && currentQuestion < QnA.Count)
        {
            QnA.RemoveAt(currentQuestion); // Remove the current question
        }

        if (QnA.Count > 0)
        {
            StartCoroutine(LoadNextQuestion()); // Wait a second and load the next question
        }
        else
        {
            EndQuiz(); // No more questions
        }
    }

    IEnumerator LoadNextQuestion()
    {
        yield return new WaitForSeconds(1); // Wait 1 second
        GenerateQuestion();
    }

    void SetAnswers()
    {
        // Validate QnA and currentQuestion before setting answers
        if (currentQuestion < 0 || currentQuestion >= QnA.Count)
        {
            Debug.LogError($"Invalid currentQuestion index: {currentQuestion}. QnA.Count = {QnA.Count}");
            return;
        }

        QuestionAndAnswer currentQnA = QnA[currentQuestion];

        for (int i = 0; i < options.Length; i++)
        {
            if (i < currentQnA.Answers.Length)
            {
                if (options[i] != null)
                {
                    options[i].SetActive(true);
                    options[i].transform.GetChild(0).GetComponent<Text>().text = currentQnA.Answers[i];
                    options[i].GetComponent<AnswerScript>().isCorrect = (currentQnA.CorrectAnswer == i);

                    // Assign the Answer method dynamically
                    Button button = options[i].GetComponent<Button>();
                    button.onClick.RemoveAllListeners(); // Remove existing listeners
                    button.onClick.AddListener(options[i].GetComponent<AnswerScript>().Answer); // Add the Answer method
                }
                else
                {
                    Debug.LogError($"Option at index {i} is null.");
                }
            }
            else
            {
                options[i].SetActive(false);
            }
        }
    }

    public void IncreaseScore(int amount)
    {
        score += amount;
        UpdateScoreUI();
    }

    void UpdateScoreUI()
    {
        ScoreText.text = "Score: " + score.ToString();
    }

    public void GenerateQuestion()
    {
        ResetButtonColors(); // Reset button colors when a new question is generated

        if (QnA.Count > 0)
        {
            currentQuestion = Random.Range(0, QnA.Count);
            QuestionTxt.text = QnA[currentQuestion].Question;

            // Display images if available
            if (QnA[currentQuestion].Images != null && QnA[currentQuestion].Images.Length > 0)
            {
                for (int i = 0; i < QuestionImages.Length; i++)
                {
                    if (i < QnA[currentQuestion].Images.Length)
                    {
                        QuestionImages[i].gameObject.SetActive(true);

                        // Load image sprite from Resources folder
                        Sprite sprite = Resources.Load<Sprite>(QnA[currentQuestion].Images[i]);
                        if (sprite != null)
                        {
                            QuestionImages[i].sprite = sprite;
                        }
                        else
                        {
                            Debug.LogError($"Image '{QnA[currentQuestion].Images[i]}' not found in Resources!");
                            QuestionImages[i].gameObject.SetActive(false);
                        }
                    }
                    else
                    {
                        QuestionImages[i].gameObject.SetActive(false);
                    }
                }
            }
            else
            {
                // Hide all images if there are none for this question
                foreach (var image in QuestionImages)
                {
                    image.gameObject.SetActive(false);
                }
            }

            SetAnswers();
        }
        else
        {
            EndQuiz();

        }
    }

    public void EndQuiz()

    {
        //save score from riddle scene
        PlayerPrefs.SetInt("Score", score);
        PlayerPrefs.Save();

        Debug.Log("Quiz Over! All questions have been answered.");

        // Start the coroutine to delay the scene switch
        StartCoroutine(WaitBeforeSwitchScene(1f)); // Wait for 1 second before switching to the next scene
    }
    IEnumerator WaitBeforeSwitchScene(float waitTime)
    {
        // Wait for the specified amount of time
        yield return new WaitForSeconds(waitTime);

        // After the wait, load the CongratsScene
        UnityEngine.SceneManagement.SceneManager.LoadScene("CongratScene");
    }

    private void LoadQuestionsFromJSON()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("questions"); // Load "questions.json" from Resources folder
        if (jsonFile != null)
        {
            QnAWrapper qnaWrapper = JsonUtility.FromJson<QnAWrapper>(jsonFile.text);
            QnA = qnaWrapper.QnA;

            Debug.Log($"Loaded {QnA.Count} questions from JSON.");
        }
        else
        {
            Debug.LogError("No questions file found in Resources folder!");
        }

        Debug.Log("Quiz Over! All questions have been answered.");

        // Save the score to PlayerPrefs
        PlayerPrefs.SetInt("Score", score);
        PlayerPrefs.Save();


    }

    void ResetButtonColors()
    {
        foreach (var option in options)
        {
            if (option != null)
            {
                option.GetComponent<Image>().color = Color.white;
            }
        }
    }

    private void PlayAudio(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.PlayOneShot(clip); // Play the specified audio clip
        }
        else
        {
            Debug.LogWarning("Audio clip or AudioSource is missing!");
        }
    }
}
