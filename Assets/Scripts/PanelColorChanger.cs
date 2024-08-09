using System.Collections;
using UnityEngine;

public class PanelColorChanger : MonoBehaviour
{
    [SerializeField] private Renderer[] panelRenderers;
    [SerializeField] private float changeInterval = 1f;

    private Material[][] materials;

    private void Start()
    {
        materials = new Material[panelRenderers.Length][];
        for (int i = 0; i < panelRenderers.Length; i++)
        {
            materials[i] = panelRenderers[i].materials;
        }
        StartCoroutine(ChangeColors());
    }

    private IEnumerator ChangeColors()
    {
        while (true)
        {
            Color newColor;
            do
            {
                newColor = new Color(Random.value, Random.value, Random.value);
            } while (IsCloseToWhiteOrGray(newColor));

            foreach (Material[] materialSet in materials)
            {
                materialSet[0].SetColor("_Color", newColor);
                materialSet[0].SetColor("_EmissionColor", newColor * 2);
                materialSet[0].SetColor("_Emission1", newColor);
                materialSet[0].SetColor("_Emission2", newColor * 0.5f);
                materialSet[1].SetColor("_Color", newColor);
            }
            yield return new WaitForSeconds(changeInterval);
        }
    }

    private bool IsCloseToWhiteOrGray(Color color)
    {
        return Mathf.Abs(color.r - color.g) < 0.1f &&
               Mathf.Abs(color.g - color.b) < 0.1f &&
               Mathf.Abs(color.r - color.b) < 0.1f &&
               color.r > 0.8f && color.g > 0.8f && color.b > 0.8f;
    }
}
