using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {
	
	public Transform wayPointParent;
	public Transform wayPointsArea;
	public Transform wayPointsAttrac;
	public GameObject preFab;
	public float radius = 5;

	[HideInInspector]
	public List<Transform> wayPointList;
	[HideInInspector]
	public List<Transform> attracPointList;
	[HideInInspector]
	public int maxInstances;

	private List<Transform> spawnPoints;
	public List<GameObject> instances;
	private float time;
	private float lastTimeInstance = 0f;

	// Use this for initialization
	void Start () {

		instances = new List<GameObject> ();
		wayPointList = new List<Transform> ();
		spawnPoints = new List<Transform> ();

		foreach (Transform key in transform) {
			spawnPoints.Add (key);
			//Destroy (GetComponent<MeshRenderer> ());
		}

		foreach (Transform key in wayPointsArea) {
			wayPointList.Add (key);
		}

		foreach (Transform key in wayPointsAttrac) {
			attracPointList.Add (key);
		}

	}
	
	// Update is called once per frame
	void Update () {
		int countActives = 0;
		foreach (var key in attracPointList) {
			if (key.gameObject.active)
				countActives++;
		}

        

		maxInstances = 5 + (countActives * countActives * 2 );
		time = Random.Range (1, PlayerPrefs.GetInt("Power_SpawTime"));

		if (Time.time >= lastTimeInstance + time) {
			lastTimeInstance = Time.time;
			Spawn ();
		}
	}

	void Spawn(){
        for (int i=0; i < instances.Count; i++)
        {
            if (instances[i] == null)
            {
                GameObject go = Instantiate(preFab);
                int j = Random.Range(0, spawnPoints.Count);
                go.transform.position = spawnPoints[j].position;

                instances[i] = go ;

            }
        }

        if (instances.Count < maxInstances){
			GameObject go = Instantiate (preFab);
			int i = Random.Range (0, spawnPoints.Count);
			go.transform.position = spawnPoints [i].position;
			instances.Add (go);
		}
	}
}
