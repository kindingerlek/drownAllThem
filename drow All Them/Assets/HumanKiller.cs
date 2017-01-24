using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanKiller : MonoBehaviour
{
    private Collider coll;

    // Use this for initialization
    void Start()
    {
        coll = GetComponent<Collider>();
        PlayerPrefs.SetInt("baseScore",0);
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.transform.tag == "human"){
            PlayerPrefs.SetInt("baseScore", PlayerPrefs.GetInt("baseScore") + 1);
            PlayerPrefs.SetInt("points", PlayerPrefs.GetInt("points") + 1 * PlayerPrefs.GetInt("Power_humanMult"));
            Destroy(collision.collider.transform.gameObject);
        }

    }
}
