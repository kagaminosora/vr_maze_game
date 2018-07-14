using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ChangePlayer : MonoBehaviour {
    public GameObject firstPerson;
    public GameObject unitychan;
	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetButton("Fire1"))
        {
            unitychan.SetActive(false);
        }
        if (Input.GetButton("Fire2"))
        {
            unitychan.transform.Translate(unitychan.transform.position - firstPerson.transform.position);
            unitychan.transform.forward = firstPerson.transform.forward;
            unitychan.SetActive(true);
        }
	}
}
