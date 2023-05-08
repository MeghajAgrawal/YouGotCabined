using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlight : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private List<Renderer> renderers;
    [SerializeField]
    private Color color = Color.white;
    private List<Material> materials;
    void Awake()
    {
        materials = new List<Material>();
        renderers = new List<Renderer>(gameObject.GetComponentsInChildren<Renderer>());
        foreach (var renderer in renderers)
        {
            materials.AddRange(new List<Material>(renderer.materials));
        }
    }
    public void ToggleHighlight(bool val)
    {
        if(val)
        {
            foreach (var material in materials)
            {
                material.EnableKeyword("_EMISSION");
                material.SetColor("_EmissionColor", color);
            }
        }
        else
        {
            foreach (var material in materials)
            {
                material.DisableKeyword("_EMISSION");
            }
        }
    }
}
