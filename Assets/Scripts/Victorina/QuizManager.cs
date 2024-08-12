using UnityEngine;
using UnityEngine.UI;
using TMPro; // Для использования TextMeshProUGUI

public class QuizManager : MonoBehaviour
{
    public Quiz quiz; // Ссылка на ScriptableObject с викториной
    public TextMeshProUGUI questionText; // UI элемент для отображения текста вопроса
    public Button[] answerButtons; // Кнопки для выбора ответа
    public TextMeshProUGUI timerText; // TextMeshProUGUI элемент для отображения таймера

    private int currentQuestionIndex = 0;
    private Color correctColor = Color.green;
    private Color incorrectColor = Color.red;
    private Color defaultColor;

    private float timePerQuestion = 20f; // Время на каждый вопрос (20 секунд)
    private float timeRemaining;
    private bool isTimerRunning;

    void Start()
    {
        defaultColor = answerButtons[0].GetComponent<Image>().color;
        DisplayQuestion();
    }

    void Update()
    {
        if (isTimerRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                UpdateTimerDisplay();
            }
            else
            {
                // Время вышло, переход к следующему вопросу
                timeRemaining = 0;
                isTimerRunning = false;
                NextQuestion();
            }
        }
    }

    void DisplayQuestion()
    {
        QuizQuestion currentQuestion = quiz.questions[currentQuestionIndex];
        questionText.text = currentQuestion.questionText;

        for (int i = 0; i < answerButtons.Length; i++)
        {
            if (i < currentQuestion.answers.Length)
            {
                answerButtons[i].gameObject.SetActive(true);
                answerButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = currentQuestion.answers[i];
                answerButtons[i].onClick.RemoveAllListeners();
                answerButtons[i].GetComponent<Image>().color = defaultColor;
                int index = i;
                answerButtons[i].onClick.AddListener(() => CheckAnswer(index));
            }
            else
            {
                answerButtons[i].gameObject.SetActive(false);
            }
        }

        // Сброс таймера при показе нового вопроса
        timeRemaining = timePerQuestion;
        isTimerRunning = true;
        UpdateTimerDisplay();
    }

    void UpdateTimerDisplay()
    {
        // Отображение оставшегося времени в целых секундах
        timerText.text = $"{Mathf.CeilToInt(timeRemaining)}";
    }

    void CheckAnswer(int index)
    {
        isTimerRunning = false; // Останавливаем таймер, когда ответ выбран

        if (index == quiz.questions[currentQuestionIndex].correctAnswerIndex)
        {
            Debug.Log("Correct!");
            answerButtons[index].GetComponent<Image>().color = correctColor;
        }
        else
        {
            Debug.Log("Incorrect!");
            answerButtons[index].GetComponent<Image>().color = incorrectColor;
        }

        Invoke("NextQuestion", 1f);
    }

    void NextQuestion()
    {
        currentQuestionIndex++;

        if (currentQuestionIndex < quiz.questions.Length)
        {
            DisplayQuestion();
        }
        else
        {
            Debug.Log("Quiz Complete!");
            // Обработка завершения викторины
        }
    }
}
