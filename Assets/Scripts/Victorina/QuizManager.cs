using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using LitMotion;
using LitMotion.Extensions;

public class QuizManager : MonoBehaviour
{
    public Quiz quizCollection; // Ссылка на ScriptableObject с коллекцией викторин
    public Canvas referenceCanvas; // Канвас для справочного материала
    public TextMeshProUGUI referenceText; // TMP текст для справочного материала
    public TextMeshProUGUI headerText; // TMP текст для заголовка викторины
    public TextMesh copyHeaderText;
    public Canvas quizCanvas; // Канвас для викторины с вопросами
    public TextMeshProUGUI questionText; // UI элемент для отображения текста вопроса
    public Button[] answerButtons; // Кнопки для выбора ответа
    public TextMeshProUGUI timerText; // TextMeshProUGUI элемент для отображения таймера
    public Button okButton; // Кнопка "ОК" для перехода от справочного материала к вопросам

    private int currentQuizSetIndex = 0; // Индекс текущей викторины в коллекции
    private int currentQuestionIndex = 0; // Индекс текущего вопроса в викторине
    private Color correctColor = Color.green;
    private Color incorrectColor = Color.red;
    private Color defaultColor;

    private float timeRemaining;
    private bool isTimerRunning;

    private readonly CompositeMotionHandle motionHandles = new();
    private Color animationColor;

    void Start()
    {
        if (quizCollection.quizSets.Length > 0)
        {
            defaultColor = answerButtons[0].GetComponent<Image>().color;
            ColorUtility.TryParseHtmlString("#FF5353", out animationColor);
            ShowReferenceMaterial(currentQuizSetIndex);
        }
        else
        {
            Debug.LogError("Нет доступных викторин в коллекции!");
        }
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
                timeRemaining = 0;
                isTimerRunning = false;
                NextQuestion();
            }
        }
    }

    void ShowReferenceMaterial(int quizSetIndex)
    {
        if (quizSetIndex >= 0 && quizSetIndex < quizCollection.quizSets.Length)
        {
            QuizSet currentQuizSet = quizCollection.quizSets[quizSetIndex];

            // Устанавливаем заголовок
            headerText.text = copyHeaderText.text = currentQuizSet.setName;

            // Запускаем анимацию текста для справочного материала
            AnimateReferenceText(currentQuizSet.referenceMaterial);

            referenceCanvas.gameObject.SetActive(true); // Включаем канвас справочного материала
            quizCanvas.gameObject.SetActive(false); // Отключаем канвас викторины

            okButton.onClick.RemoveAllListeners(); // Удаляем старые слушатели (если они есть)
            okButton.onClick.AddListener(StartQuiz); // Добавляем слушатель для кнопки "ОК"
        }
        else
        {
            Debug.LogError("Неверный индекс викторины!");
        }
    }

    void AnimateReferenceText(string textToAnimate)
    {
        motionHandles.Complete(); // Останавливаем предыдущие анимации

        referenceText.text = textToAnimate;
        referenceText.ForceMeshUpdate(true);

        for (var i = 0; i < referenceText.textInfo.characterCount; i++)
        {
            LMotion.Create(Vector3.zero, Vector3.one, 0.2f)
                .WithEase(Ease.OutSine)
                .WithDelay(i * 0.05f, skipValuesDuringDelay: false)
                .BindToTMPCharScale(referenceText, i)
                .AddTo(motionHandles);

            LMotion.Create(-50f, 0f, 0.2f)
                .WithEase(Ease.OutSine)
                .WithDelay(i * 0.05f, skipValuesDuringDelay: false)
                .BindToTMPCharPositionY(referenceText, i)
                .AddTo(motionHandles);

            LMotion.Create(Color.white, animationColor, 0.2f)
                .WithEase(Ease.OutSine)
                .WithDelay(0.3f + i * 0.05f, skipValuesDuringDelay: false)
                .BindToTMPCharColor(referenceText, i)
                .AddTo(motionHandles);
        }
    }

    void StartQuiz()
    {
        // Переключение канвасов
        referenceCanvas.gameObject.SetActive(false);
        quizCanvas.gameObject.SetActive(true);
        LoadQuizSet(currentQuizSetIndex);
    }

    void LoadQuizSet(int quizSetIndex)
    {
        if (quizSetIndex >= 0 && quizSetIndex < quizCollection.quizSets.Length)
        {
            currentQuizSetIndex = quizSetIndex;
            currentQuestionIndex = 0;
            timeRemaining = quizCollection.quizSets[currentQuizSetIndex].timePerQuestion;
            DisplayQuestion();
        }
        else
        {
            Debug.LogError("Неверный индекс викторины!");
        }
    }

    void DisplayQuestion()
    {
        QuizSet currentQuizSet = quizCollection.quizSets[currentQuizSetIndex];
        QuizQuestion currentQuestion = currentQuizSet.questions[currentQuestionIndex];
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

        timeRemaining = currentQuizSet.timePerQuestion;
        isTimerRunning = true;
        UpdateTimerDisplay();
    }

    void UpdateTimerDisplay()
    {
        timerText.text = $"{Mathf.CeilToInt(timeRemaining)}";
    }

    void CheckAnswer(int index)
    {
        isTimerRunning = false;

        QuizSet currentQuizSet = quizCollection.quizSets[currentQuizSetIndex];

        if (index == currentQuizSet.questions[currentQuestionIndex].correctAnswerIndex)
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
        QuizSet currentQuizSet = quizCollection.quizSets[currentQuizSetIndex];
        currentQuestionIndex++;

        if (currentQuestionIndex < currentQuizSet.questions.Length)
        {
            DisplayQuestion();
        }
        else
        {
            Debug.Log("Викторина завершена!");
            NextQuizSet();
        }
    }

    void NextQuizSet()
    {
        // Отключаем канвас с вопросами после завершения текущей викторины
        quizCanvas.gameObject.SetActive(false);

        currentQuizSetIndex++;

        if (currentQuizSetIndex < quizCollection.quizSets.Length)
        {
            Debug.Log($"Переход к следующей викторине через 10 секунд...");
            Invoke("StartNextQuizSet", 10f); // Задержка в 10 секунд перед началом следующей викторины
        }
        else
        {
            Debug.Log("Все викторины в коллекции завершены!");
            // Здесь можно реализовать логику завершения всех викторин, например, вернуться в меню
        }
    }

    void StartNextQuizSet()
    {
        ShowReferenceMaterial(currentQuizSetIndex); // Показываем справочный материал для следующей викторины
    }
}
