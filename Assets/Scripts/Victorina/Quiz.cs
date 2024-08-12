using UnityEngine;

[CreateAssetMenu(fileName = "NewQuiz", menuName = "Quiz/New Quiz")]
public class Quiz : ScriptableObject
{
    public string quizTitle;              // Название викторины
    public QuizQuestion[] questions; // Список вопросов
}
