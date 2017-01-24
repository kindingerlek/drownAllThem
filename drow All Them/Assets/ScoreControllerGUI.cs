using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;  //its a must to access new UI in script


public class ScoreControllerGUI : MonoBehaviour {
    private GUIText text;
	// Use this for initialization
	void Start () {
        text = GetComponent<GUIText>();

    }
	
	void Update () {
        text.text = PlayerPrefs.GetInt("score").ToString();

    }
}
