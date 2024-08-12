using UnityEngine;

[CreateAssetMenu(fileName = "New Quiz Collection", menuName = "Quiz/Quiz Collection")]
public class Quiz : ScriptableObject
{
    public string quizName; // �������� ��������� ��������
    public QuizSet[] quizSets; // ������ �������� (������ ��������� ����� ���� ������� � ���������� ��������)
}

[System.Serializable]
public class QuizSet
{
    public string setName; // �������� ���������� ���������
    public float timePerQuestion = 20f; // ����� �� ������ ������ � ���� ���������
    [TextArea(3, 10)]
    public string referenceMaterial; // ���������� �������� ��� ���� ���������
    public QuizQuestion[] questions; // ������ �������� � ���� ���������
}
