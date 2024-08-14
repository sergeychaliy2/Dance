using UnityEngine;
using TMPro;

public class TextSizeController : MonoBehaviour
{
    [SerializeField] private TMP_Text textMeshPro;
    [SerializeField] private float fontSizeStep = 2f;
    [SerializeField] private float minFontSize = 10f;
    [SerializeField] private float maxFontSize = 100f;
    [SerializeField] private GameObject panel;
    [SerializeField] private float epsilon = 0.01f;

    public void IncreaseFontSize()
    {
        if (textMeshPro != null)
        {
            float newSize = textMeshPro.fontSize + fontSizeStep;

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

            if (newSize < textMeshPro.fontSize && newSize >= minFontSize)
            {
                textMeshPro.fontSize = Mathf.Max(newSize, minFontSize);
            }
        }
    }

    public void TogglePanel()
    {
        if (panel != null)
        {
            panel.SetActive(!panel.activeSelf);
        }
    }
}
