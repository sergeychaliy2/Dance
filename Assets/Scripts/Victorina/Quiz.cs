using UnityEngine;

[CreateAssetMenu(fileName = "New Quiz Collection", menuName = "Quiz/Quiz Collection")]
public class Quiz : ScriptableObject
{
    public string quizName; // Ќазвание коллекции викторин
    public QuizSet[] quizSets; // ћассив викторин (кажда€ викторина имеет свои вопросы и справочный материал)
}

[System.Serializable]
public class QuizSet
{
    public string setName; // Ќазвание конкретной викторины
    public float timePerQuestion = 20f; // ¬рем€ на каждый вопрос в этой викторине
    [TextArea(3, 10)]
    public string referenceMaterial; // —правочный материал дл€ этой викторины
    public QuizQuestion[] questions; // ћассив вопросов в этой викторине
}
