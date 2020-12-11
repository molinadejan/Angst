using UnityEngine;

public class CombineMeshWithMeshCollider : MonoBehaviour
{
    public PhysicMaterial pm;

    private Vector3 originPos;
    private Vector3 originRot;

    private void Start()
    {
        CombineFunc();
    }

    private bool CheckSameMaterial(MeshRenderer[] meshRenderers)
    {
        Material material = meshRenderers[0].sharedMaterial;

        for (int i = 1; i < meshRenderers.Length; ++i)
            if (material != meshRenderers[i].sharedMaterial)
                return false;

        return true;
    }

    private void CombineFunc()
    {
        MeshFilter[] meshFilters = GetComponentsInChildren<MeshFilter>();
        MeshRenderer[] meshRenderers = GetComponentsInChildren<MeshRenderer>();

        if (!CheckSameMaterial(meshRenderers)) return;

        originPos = transform.position;
        originRot = transform.localEulerAngles;

        transform.position = Vector3.zero;
        transform.localEulerAngles = Vector3.zero;

        CombineInstance[] combine = new CombineInstance[meshFilters.Length];

        for (int i = 0; i < meshFilters.Length; ++i)
        {
            combine[i].mesh = meshFilters[i].sharedMesh;
            combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
            meshFilters[i].gameObject.SetActive(false);
        }

        MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>() as MeshFilter;
        MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>() as MeshRenderer;

        meshRenderer.sharedMaterial = meshRenderers[0].sharedMaterial;
        meshFilter.mesh = new Mesh();
        meshFilter.mesh.CombineMeshes(combine);

        MeshCollider meshCollider = gameObject.AddComponent<MeshCollider>();
        meshCollider.material = pm;

        transform.localScale = Vector3.one;
        transform.gameObject.SetActive(true);

        transform.position = originPos;
        transform.localEulerAngles = originRot;
    }
}
