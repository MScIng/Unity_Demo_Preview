using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class ChangeMaterial : MonoBehaviour
{
    public GameObject prefab; // Reference to the prefab whose material you want to change
    public Material invisMaterial;
    public Material semitransparentMaterial;
    private Renderer renderer;

    public bool isInvis = true;

    public ARMeshManager meshManager;

    void Start()
    {
        SetInvis();
    }

    public void SwitchMaterial()
    {
        // 
        IList<MeshFilter> meshFilters = meshManager.meshes;
        foreach (var meshFilter in meshFilters)
        {
            // Access the MeshRenderer component associated with the MeshFilter
            MeshRenderer meshRenderer = meshFilter.GetComponent<MeshRenderer>();

            if (meshRenderer != null)
            {

                // Assign the new material instance to the MeshRenderer
                if (isInvis)
                {
                    meshRenderer.material = invisMaterial;
                    SetSemiTransparent();
                }
                else
                {
                    meshRenderer.material = semitransparentMaterial;
                    SetInvis();
                }
                
                // Ensure the mesh is rendered with the new material
                meshRenderer.material.EnableKeyword("_BaseColor");
            }
        }
        isInvis = !isInvis;
    }

    void SetInvis()
    {
        renderer = prefab.GetComponent<Renderer>();
        renderer.material = invisMaterial;
    }

    void SetSemiTransparent()
    {
        renderer = prefab.GetComponent<Renderer>();
        renderer.material = semitransparentMaterial;
    }
    
}
