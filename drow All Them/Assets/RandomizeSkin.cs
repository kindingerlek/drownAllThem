using UnityEngine;
using System.Collections;

public class RandomizeSkin : MonoBehaviour {
    public Material[] hair;
    public Material[] skin;
    public Material[] clth;

    private Material[] n;

    private SkinnedMeshRenderer mesh;
	// Use this for initialization
	void Start () {

        n = new Material[3];

        n[0] = hair[Random.Range(0, hair.Length)];
        n[1] = skin[Random.Range(0, skin.Length)];
        n[2] = clth[Random.Range(0, clth.Length)];

        mesh = GetComponentInChildren<SkinnedMeshRenderer>();
        mesh.materials = n;
	}
}
