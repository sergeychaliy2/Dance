using UnityEngine;
using System.Collections;

public class ColorChanger : MonoBehaviour
{
    public float changeInterval = 2.0f;
    public float minHue = 0f;
    public float maxHue = 1f;
    public float minSaturation = 0f;
    public float maxSaturation = 1f;
    public float minValue = 0f;
    public float maxValue = 1f;

    private Renderer objectRenderer;

    void Start()
    {
        objectRenderer = GetComponent<Renderer>();

        if (objectRenderer != null)
        {
            StartCoroutine(ChangeColorOverTime());
        }
    }

    IEnumerator ChangeColorOverTime()
    {
        while (true)
        {
            Color newColor = Random.ColorHSV(minHue, maxHue, minSaturation, maxSaturation, minValue, maxValue);
            if (objectRenderer.material.HasProperty("_Color"))
            {
                objectRenderer.material.SetColor("_Color", newColor);
            }
            else
            {
                Debug.LogWarning("Материал не имеет свойства '_Color'");
            }

            yield return new WaitForSeconds(changeInterval);
        }
    }
}
