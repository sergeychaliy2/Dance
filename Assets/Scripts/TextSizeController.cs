using UnityEngine;
using TMPro;

public class TextSizeController : MonoBehaviour
{
    public TMP_Text textMeshPro;

    public float fontSizeStep = 2f;
    public float minFontSize = 10f;
    public float maxFontSize = 100f;

    // Минимальная допустимая разница между текущим размером шрифта и целевым (для борьбы с неточностями float).
    private float epsilon = 0.01f;

    public void IncreaseFontSize()
    {
        if (textMeshPro != null)
        {
            float newSize = textMeshPro.fontSize + fontSizeStep;

            // Проверка на возможные неточности и лимиты.
            if (newSize > textMeshPro.fontSize && newSize <= maxFontSize)
            {
                textMeshPro.fontSize = Mathf.Min(newSize, maxFontSize);
            }
        }
    }

    public void DecreaseFontSize()
    {
        if (textMeshPro != null)
        {
            float newSize = textMeshPro.fontSize - fontSizeStep;

            // Проверка на возможные неточности и лимиты.
            if (newSize < textMeshPro.fontSize && newSize >= minFontSize)
            {
                textMeshPro.fontSize = Mathf.Max(newSize, minFontSize);
            }
        }
    }
}
