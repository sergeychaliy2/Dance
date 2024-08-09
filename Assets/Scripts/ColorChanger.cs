using UnityEngine;
using System.Collections;

public class ColorChanger : MonoBehaviour
{
    [Header("SETTINGS"), Space]
    [SerializeField] private float changeInterval = 2.0f;
    [SerializeField] private float minHue = 0f;
    [SerializeField] private float maxHue = 1f;
    [SerializeField] private float minSaturation = 0f;
    [SerializeField] private float maxSaturation = 1f;
    [SerializeField] private float minValue = 0f;
    [SerializeField] private float maxValue = 1f;

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
               ErrorType.UnknownError.LogCustomError("Материал не имеет свойства '_Color'");
            }

            yield return new WaitForSeconds(changeInterval);
        }
    }
}
