[System.Serializable]
public class QuizQuestion
{
    public string questionText; // Текст вопроса
    public string[] answers;    // Варианты ответов
    public int correctAnswerIndex; // Индекс правильного ответа в массиве answers
}
