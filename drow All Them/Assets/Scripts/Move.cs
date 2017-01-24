using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {
    public LayerMask mask;
	public float speed = 0.5F;

	private Vector3 endMarker;
	private float chegadaTime = 0.0f;
	private float startTime;
	private float journeyLength;
	private SpawnManager spawn;
    private Collider _collider;
    private Animator _animator;

    private bool walking;
    private bool grounding;

	void Start() {
		startTime = Time.time;
		spawn = GameObject.Find ("SpawnManager").GetComponent<SpawnManager>() ;
        _animator = GetComponent<Animator>();

		NewPos ();
		journeyLength = Vector3.Distance(transform.position , endMarker);

        _collider = GetComponent<Collider>();
	}

	void Update() {

		float distCovered = (Time.time - startTime) * speed;
		float fracJourney = distCovered / journeyLength;

        RaycastHit hit;
        grounding = Physics.Raycast(transform.position, Vector3.down, out hit, 5f, mask);

        Vector2 thisposition = new Vector2(transform.position.x, transform.position.z);
        Vector2 endposition = new Vector2(endMarker.x, endMarker.z);

        if (Vector2.Distance(thisposition, endposition) < 0.3f) {
            if(chegadaTime == 0.0f)
			    chegadaTime = Time.time + Random.Range(0,5);

            if (Time.time > chegadaTime)
            {
                NewPos ();
				chegadaTime = 0.0f;
				startTime = Time.time;
            }
            
            walking = false;
        }
        else {
            //transform.position = Vector3.Lerp (transform.position, endMarker, fracJourney);
            endMarker.y = transform.position.y;

            transform.position = Vector3.MoveTowards(transform.position, endMarker, Time.deltaTime * speed);

            var lookPos = endMarker - transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 5f);
            

            walking = true;


        } 

        if (_animator)
        {
            _animator.SetBool("walking", walking);
            _animator.SetBool("grounding", grounding);
        }
    }
    

	void NewPos(){
		int attrac = Random.Range (0, 11);
		float radius = 0;
		Transform pos;
		if (attrac < 5) {
			int i = Random.Range (0, spawn.wayPointList.Count);
			pos = spawn.wayPointList[i];
		} else {
			int i = Random.Range (0, spawn.attracPointList.Count);
			pos = spawn.attracPointList[i];
		}

		radius = pos.gameObject.GetComponent<SphereCollider> ().radius;

		endMarker = pos.position + Random.insideUnitSphere * radius;
		endMarker.y = pos.position.y;
	}
    
}
