using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;  //its a must to access new UI in script


public class UIController : MonoBehaviour {

    public Text points;
    public Text humanKilled;
    public List<Transform> attracPoints;

    // Use this for initialization
    void Start () {
        resetProps();

    }

    void resetProps() {
        PlayerPrefs.SetInt("baseScore", 0);
        PlayerPrefs.SetInt("points", 20);
        PlayerPrefs.SetInt("Power_spawnTime", 5);
        PlayerPrefs.SetInt("Power_attractions", 0);
        PlayerPrefs.SetInt("Power_waveForce", 4);
        PlayerPrefs.SetInt("Power_humanMult", 1);
    }

    // Update is called once per frame
    void Update () {

        
        int attracPointsCount = PlayerPrefs.GetInt("Power_attractions");
        Debug.Log(attracPointsCount);

        foreach (Transform key in attracPoints)
        {
            if (attracPointsCount > 0) {
                attracPointsCount--;
                key.gameObject.SetActive(true);
            }
        }

        humanKilled.text =     "Afogamentos\n"+ PlayerPrefs.GetInt("baseScore").ToString();
        points.text = "WavePoints\n" + PlayerPrefs.GetInt("points").ToString();
    }
}
