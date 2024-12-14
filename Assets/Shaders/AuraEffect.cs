using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuraEffect : MonoBehaviour
{
    private List<SkinnedMeshRenderer> objRenderer;
    private List<Color> defaultEmissionColor;
    public Color auraColor = Color.blue;
    public bool isAuraActive = false; 

    void Start()
    {
        objRenderer = new List<SkinnedMeshRenderer>(GetComponentsInChildren<SkinnedMeshRenderer>());
        defaultEmissionColor = new List<Color>(objRenderer.Count);
        for(int i=0; i<objRenderer.Count; i++)
            defaultEmissionColor.Add(objRenderer[i].material.GetColor("_EmissionColor"));
    }

    void Update()
    {
        if (isAuraActive)
        {
            ActivateAura();
        }
        else
        {
            DeactivateAura();
        }
    }

    public void ActivateAura()
    {
        for(int i=0; i<objRenderer.Count; i++){
            objRenderer[i].material.EnableKeyword("_EMISSION");
            objRenderer[i].material.SetColor("_EmissionColor", auraColor);
        }
    }

    public void DeactivateAura()
    {
        for(int i=0; i<objRenderer.Count; i++){
            objRenderer[i].material.DisableKeyword("_EMISSION");
            objRenderer[i].material.SetColor("_EmissionColor", defaultEmissionColor[i]);
        }
    }
}
