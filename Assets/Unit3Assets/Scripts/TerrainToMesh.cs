using UnityEngine;

public class TerrainToMesh : MonoBehaviour
{
    public Terrain terrain;

    void Start()
    {
        Mesh terrainMesh = TerrainToMeshConverter.Convert(terrain);
        MeshFilter mf = gameObject.AddComponent<MeshFilter>();
        MeshRenderer mr = gameObject.AddComponent<MeshRenderer>();
        mf.mesh = terrainMesh;
    }
}

public static class TerrainToMeshConverter
{
    public static Mesh Convert(Terrain terrain)
    {
        TerrainData terrainData = terrain.terrainData;
        int width = terrainData.heightmapResolution;
        int height = terrainData.heightmapResolution;

        Vector3 terrainSize = terrainData.size;
        float[,] heights = terrainData.GetHeights(0, 0, width, height);

        Vector3[] vertices = new Vector3[width * height];
        Vector2[] uv = new Vector2[width * height];
        int[] triangles = new int[(width - 1) * (height - 1) * 6];

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                float y = heights[j, i] * terrainSize.y;
                vertices[i + j * width] = new Vector3(i * terrainSize.x / (width - 1), y, j * terrainSize.z / (height - 1));
                uv[i + j * width] = new Vector2((float)i / (width - 1), (float)j / (height - 1));
            }
        }

        int t = 0;
        for (int i = 0; i < width - 1; i++)
        {
            for (int j = 0; j < height - 1; j++)
            {
                int index = i + j * width;
                triangles[t++] = index;
                triangles[t++] = index + width;
                triangles[t++] = index + width + 1;

                triangles[t++] = index;
                triangles[t++] = index + width + 1;
                triangles[t++] = index + 1;
            }
        }

        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();

        return mesh;
    }
}
