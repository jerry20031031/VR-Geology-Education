using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[DisallowMultipleComponent]
public class unit1Outline : MonoBehaviour
{
    private static HashSet<Mesh> registeredMeshes = new HashSet<Mesh>();
    private static Material outlineMaskMaterial;
    private static Material outlineFillMaterial;

    public enum Mode
    {
        OutlineAll,
        OutlineVisible,
        OutlineHidden,
        OutlineAndSilhouette,
        SilhouetteOnly
    }

    public Mode OutlineMode
    {
        get => outlineMode;
        set
        {
            outlineMode = value;
            needsUpdate = true;
        }
    }

    public Color OutlineColor
    {
        get => outlineColor;
        set
        {
            outlineColor = value;
            needsUpdate = true;
        }
    }

    public float OutlineWidth
    {
        get => outlineWidth;
        set
        {
            outlineWidth = value;
            needsUpdate = true;
        }
    }

    [Serializable]
    private class ListVector3
    {
        public List<Vector3> data;
    }

    [SerializeField]
    private Mode outlineMode;

    [SerializeField]
    private Color outlineColor = Color.white;

    [SerializeField, Range(0f, 10f)]
    private float outlineWidth = 2f;

    [Header("Optional")]
    [SerializeField]
    private bool precomputeOutline;

    [SerializeField, HideInInspector]
    private List<Mesh> bakeKeys = new List<Mesh>();

    [SerializeField, HideInInspector]
    private List<ListVector3> bakeValues = new List<ListVector3>();

    private Renderer[] renderers;
    private bool needsUpdate;

    private static void InitializeMaterials()
    {
        if (outlineMaskMaterial == null)
        {
            outlineMaskMaterial = Resources.Load<Material>(@"Materials/OutlineMask");
        }
        if (outlineFillMaterial == null)
        {
            outlineFillMaterial = Resources.Load<Material>(@"Materials/OutlineFill");
        }
    }

    void Awake()
    {
        InitializeMaterials();
        renderers = GetComponentsInChildren<Renderer>(true);
        if (precomputeOutline && bakeKeys.Count == 0)
        {
            Bake();
        }
        else
        {
            LoadSmoothNormals();
        }
        needsUpdate = true;
    }

    void OnEnable()
    {
        ApplyOutlineMaterials();
    }

    void OnValidate()
    {
        needsUpdate = true;
        if (precomputeOutline && bakeKeys.Count == 0)
        {
            Bake();
        }
    }

    void Update()
    {
        if (needsUpdate)
        {
            needsUpdate = false;
            UpdateMaterialProperties();
        }
    }

    void OnDisable()
    {
        RemoveOutlineMaterials();
    }

    private void ApplyOutlineMaterials()
    {
        foreach (var renderer in renderers)
        {
            if (renderer == null) continue;

            var materials = renderer.sharedMaterials;
            if (!materials.Contains(outlineMaskMaterial))
            {
                var newMaterials = new Material[materials.Length + 2];
                Array.Copy(materials, newMaterials, materials.Length);
                newMaterials[materials.Length] = outlineMaskMaterial;
                newMaterials[materials.Length + 1] = outlineFillMaterial;
                renderer.sharedMaterials = newMaterials;
            }
        }
    }

    private void RemoveOutlineMaterials()
    {
        foreach (var renderer in renderers)
        {
            if (renderer == null) continue;

            var materials = renderer.sharedMaterials;
            if (materials.Contains(outlineMaskMaterial))
            {
                var newMaterials = materials.Where(m => m != outlineMaskMaterial && m != outlineFillMaterial).ToArray();
                renderer.sharedMaterials = newMaterials;
            }
        }
    }

    private void Bake()
    {
        foreach (var meshFilter in GetComponentsInChildren<MeshFilter>())
        {
            var mesh = meshFilter.sharedMesh;
            if (!registeredMeshes.Add(mesh)) continue;

            var smoothNormals = SmoothNormals(mesh);
            bakeKeys.Add(mesh);
            bakeValues.Add(new ListVector3 { data = smoothNormals });
        }
    }

    private void LoadSmoothNormals()
    {
        foreach (var meshFilter in GetComponentsInChildren<MeshFilter>())
        {
            var mesh = meshFilter.sharedMesh;
            if (!registeredMeshes.Add(mesh)) continue;

            var index = bakeKeys.IndexOf(mesh);
            var smoothNormals = index >= 0 ? bakeValues[index].data : SmoothNormals(mesh);
            mesh.SetUVs(3, smoothNormals);
        }
    }

    private List<Vector3> SmoothNormals(Mesh mesh)
    {
        var groups = mesh.vertices.Select((v, i) => new KeyValuePair<Vector3, int>(v, i)).GroupBy(pair => pair.Key);
        var normals = mesh.normals.ToList();

        foreach (var group in groups)
        {
            if (group.Count() == 1) continue;

            var smoothNormal = group.Aggregate(Vector3.zero, (current, pair) => current + normals[pair.Value]);
            smoothNormal.Normalize();

            foreach (var pair in group)
            {
                normals[pair.Value] = smoothNormal;
            }
        }

        return normals;
    }

    private void UpdateMaterialProperties()
    {
        outlineFillMaterial.SetColor("_OutlineColor", outlineColor);

        switch (outlineMode)
        {
            case Mode.OutlineAll:
                outlineMaskMaterial.SetFloat("_ZTest", (float)UnityEngine.Rendering.CompareFunction.Always);
                outlineFillMaterial.SetFloat("_ZTest", (float)UnityEngine.Rendering.CompareFunction.Always);
                outlineFillMaterial.SetFloat("_OutlineWidth", outlineWidth);
                break;

            case Mode.OutlineVisible:
                outlineMaskMaterial.SetFloat("_ZTest", (float)UnityEngine.Rendering.CompareFunction.Always);
                outlineFillMaterial.SetFloat("_ZTest", (float)UnityEngine.Rendering.CompareFunction.LessEqual);
                outlineFillMaterial.SetFloat("_OutlineWidth", outlineWidth);
                break;

            case Mode.OutlineHidden:
                outlineMaskMaterial.SetFloat("_ZTest", (float)UnityEngine.Rendering.CompareFunction.Always);
                outlineFillMaterial.SetFloat("_ZTest", (float)UnityEngine.Rendering.CompareFunction.Greater);
                outlineFillMaterial.SetFloat("_OutlineWidth", outlineWidth);
                break;

            case Mode.OutlineAndSilhouette:
                outlineMaskMaterial.SetFloat("_ZTest", (float)UnityEngine.Rendering.CompareFunction.LessEqual);
                outlineFillMaterial.SetFloat("_ZTest", (float)UnityEngine.Rendering.CompareFunction.Always);
                outlineFillMaterial.SetFloat("_OutlineWidth", outlineWidth);
                break;

            case Mode.SilhouetteOnly:
                outlineMaskMaterial.SetFloat("_ZTest", (float)UnityEngine.Rendering.CompareFunction.LessEqual);
                outlineFillMaterial.SetFloat("_ZTest", (float)UnityEngine.Rendering.CompareFunction.Greater);
                outlineFillMaterial.SetFloat("_OutlineWidth", 0f);
                break;
        }
    }
}
