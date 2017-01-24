using UnityEngine;
using System.Collections;

public class rippleSharp : MonoBehaviour
{

    private float[] buffer1;
    private float[] buffer2;
    private float[] buffer3;
    private int[] vertexIndices;

    private Mesh mesh;
    private MeshCollider mc;

    private Vector3[] vertices;
    //private Vector3[] normals ;

    public float dampner = 0.9f;
    public float mult = 0.5f;
    public float maxWaveHeight = 2.0f;
    public int slowdownCount = 10;

    public int splashForce = 1000;


    private bool swapMe = true;
    bool newFrameRequest;

    public int cols = 128;
    public int rows = 128;
    private int slowdownCountIndx = 1;

    // Use this for initialization
    void Start()
    {
        MeshFilter mf = (MeshFilter)GetComponent(typeof(MeshFilter));
        mc = (MeshCollider)GetComponent(typeof(MeshCollider));
        mesh = mf.mesh;
        vertices = mesh.vertices;
        buffer1 = new float[vertices.Length];
        buffer2 = new float[vertices.Length];

        Bounds bounds = mesh.bounds;


        float xStep = (bounds.max.x - bounds.min.x) / cols;
        float zStep = (bounds.max.z - bounds.min.z) / rows;

        vertexIndices = new int[vertices.Length];
        int i = 0;
        for (i = 0; i < vertices.Length; i++)
        {
            vertexIndices[i] = -1;
            buffer1[i] = 0;
            buffer2[i] = 0;
        }

        // this will produce a list of indices that are sorted the way I need them to 
        // be for the algo to work right
        for (i = 0; i < vertices.Length; i++)
        {
            float column = ((vertices[i].x - bounds.min.x) / xStep);// + 0.5;
            float row = ((vertices[i].z - bounds.min.z) / zStep);// + 0.5;
            float position = (row * (cols + 1)) + column + 0.5f;
            if (vertexIndices[(int)position] >= 0) print("smash");
            vertexIndices[(int)position] = i;
        }
        newFrameRequest = true;
        slowdownCountIndx = 1;
    }


    void splashAtPoint(int x, int y)
    {
        int position = ((y * (cols + 1)) + x);
        if (swapMe)
        {
            buffer1[position] = splashForce;
            buffer1[position - 1] = splashForce;
            buffer1[position + 1] = splashForce;
            buffer1[position + (cols + 1)] = splashForce;
            buffer1[position + (cols + 1) + 1] = splashForce;
            buffer1[position + (cols + 1) - 1] = splashForce;
            buffer1[position - (cols + 1)] = splashForce;
            buffer1[position - (cols + 1) + 1] = splashForce;
            buffer1[position - (cols + 1) - 1] = splashForce;
        }
        else
        {
            buffer2[position] = splashForce;
            buffer2[position - 1] = splashForce;
            buffer2[position + 1] = splashForce;
            buffer2[position + (cols + 1)] = splashForce;
            buffer2[position + (cols + 1) + 1] = splashForce;
            buffer2[position + (cols + 1) - 1] = splashForce;
            buffer2[position - (cols + 1)] = splashForce;
            buffer2[position - (cols + 1) + 1] = splashForce;
            buffer2[position - (cols + 1) - 1] = splashForce;
        }

    }
    float[] currentBuffer;
    float[] lastBuffer;


    // Update is called once per frame
    void Update()
    {

        maxWaveHeight = -PlayerPrefs.GetInt("Power_waveForce");

        checkInput();
        Vector3[] theseVertices = new Vector3[vertices.Length];

        if (newFrameRequest)
        {

            newFrameRequest = false;
            if (swapMe)
            {
                // process the ripples for this frame
                processRipples(buffer1, buffer2);
                currentBuffer = buffer2;
                lastBuffer = buffer1;
            }
            else
            {
                processRipples(buffer2, buffer1);
                currentBuffer = buffer1;
                lastBuffer = buffer2;
            }
            swapMe = !swapMe;
        }


        int vertIndex;
        int i = 0;
        float diff;

        if (slowdownCountIndx < slowdownCount)
        {
            // apply the ripples to our buffer

            float perCent = (float)slowdownCountIndx / (float)slowdownCount;

            for (i = 0; i < currentBuffer.Length; i++)
            {
                vertIndex = vertexIndices[i];

                diff = (((currentBuffer[vertIndex]) * 1.0f / splashForce) * maxWaveHeight) - (((lastBuffer[vertIndex]) * 1.0f / splashForce) * maxWaveHeight);
                theseVertices[vertIndex] = vertices[vertIndex];
                theseVertices[vertIndex].y = (((lastBuffer[vertIndex]) * 1.0f / splashForce) * maxWaveHeight) + (diff * perCent);


            }
            slowdownCountIndx++;
        }
        else
        {
            slowdownCountIndx = 1;
            newFrameRequest = true;
            for (i = 0; i < currentBuffer.Length; i++)
            {
                vertIndex = vertexIndices[i];

                diff = (((currentBuffer[vertIndex]) * 1.0f / splashForce) * maxWaveHeight) - (((lastBuffer[vertIndex]) * 1.0f / splashForce) * maxWaveHeight);


                theseVertices[vertIndex] = vertices[vertIndex];
                theseVertices[vertIndex].y = (((currentBuffer[vertIndex]) * 1.0f / splashForce) * maxWaveHeight);


            }
        }
        mesh.vertices = theseVertices;
        mc.sharedMesh = mesh;
    }


    // swap buffers		


    void checkInput()
    {
        if (Input.GetMouseButton(0))
        {
            splashAtPoint((int)cols/2, (int)rows/2);
            //RaycastHit hit;
            //if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            //{
            //    Bounds bounds = mesh.bounds;
            //    float xStep = (bounds.max.x - bounds.min.x) / cols;
            //    float zStep = (bounds.max.z - bounds.min.z) / rows;
            //    float xCoord = (bounds.max.x - bounds.min.x) - ((bounds.max.x - bounds.min.x) * hit.textureCoord.x);
            //    float zCoord = (bounds.max.z - bounds.min.z) - ((bounds.max.z - bounds.min.z) * hit.textureCoord.y);
            //    float column = (xCoord / xStep);// + 0.5;
            //    float row = (zCoord / zStep);// + 0.5;
            //    splashAtPoint((int)column, (int)row);
            //}
        }
    }


    void processRipples(float[] source, float[] dest)
    {
        int x = 0;
        int y = 0;
        int position = 0;
        for (y = 1; y < rows - 1; y++)
        {
            for (x = 1; x < cols; x++)
            {
                position = (y * (cols + 1)) + x;
                dest[position] = (((source[position - 1] +
                                 source[position + 1] +
                                 source[position - (cols + 1)] +
                                 source[position + (cols + 1)]) * mult) - dest[position]);
                dest[position] = (dest[position] * dampner);
            }
        }
    }

}