using UnityEngine;
using System.Collections;

public class OceanLevel : MonoBehaviour
{
    public float scale = 1.0f;
    public float sinSpeedX = 1.0f;
    public float sinSpeedZ = 1.0f;
    public float perlinSpeedX = 1.0f;
    public float perlinSpeedZ = 1.0f;
    private Vector3[] baseVertices;
    public bool recalculateNormals = true;

    public bool isSin = false;
    public bool isPerlin = true;

    Mesh mesh;
    Vector3[] vertices;

    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;

        if (baseVertices == null)
            baseVertices = mesh.vertices;

        vertices = new Vector3[baseVertices.Length];
    }

    void Update()
    {
        for (int i = 0; i < vertices.Length; i++)
        {
            Vector3 vertex = baseVertices[i];

            if (isSin == true && isPerlin == false)
            {
                vertex.y += Mathf.Sin(vertex.x + Time.time * sinSpeedX) *
                            Mathf.Sin(vertex.z + Time.time * sinSpeedZ) * scale;
            }
            if (isPerlin == true && isSin == false)
            {
                vertex.y += Mathf.PerlinNoise(vertex.x + Time.time * perlinSpeedX,
                                              vertex.z + Time.time * perlinSpeedZ) * scale;
            }
            if (isPerlin == true && isSin == true)
            {
                vertex.y += Mathf.PerlinNoise(vertex.x + Time.time * perlinSpeedX,
                                              vertex.z + Time.time * perlinSpeedZ) *
                            Mathf.Sin(vertex.x + Time.time * sinSpeedX) *
                            Mathf.Sin(vertex.z + Time.time * sinSpeedZ) * scale;
            }

            vertices[i] = vertex;
        }

        mesh.MarkDynamic();
        mesh.vertices = vertices;
        mesh.RecalculateBounds();

        if (recalculateNormals)
            mesh.RecalculateNormals();

    }
}
