using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamStart : MonoBehaviour {
    public Canvas splash;
	
	// Update is called once per frame
	void Update () {
        if(Input.GetMouseButton(0))
        {
            splash.enabled = false;
        }
		
	}
}
