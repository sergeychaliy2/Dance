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
    [SerializeField] private float minIntensity = 1f;
    [SerializeField] private float maxIntensity = 5f;

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
            float intensity = Random.Range(minIntensity, maxIntensity);
            Color hdrColor = newColor * intensity;
            if (objectRenderer.material.HasProperty("_EmissionColor"))
            {
                objectRenderer.material.SetColor("_EmissionColor", hdrColor);
                objectRenderer.material.EnableKeyword("_EMISSION");
            }
            else
            {
                Debug.LogError("Материал не имеет свойства '_EmissionColor'");
            }

            yield return new WaitForSeconds(changeInterval);
        }
    }
}
