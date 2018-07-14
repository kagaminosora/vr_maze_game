using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Collectable : MonoBehaviour {
    GameObject hand;
    GameObject thirdPerson;
    public GameObject gameOverCanvas;
    public Text winText;
    private bool gameOver=false;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if (hand != null)
        {
            GetComponent<Renderer>().material.color = Color.blue;
            if (OVRInput.Get(OVRInput.RawButton.LHandTrigger)|| OVRInput.Get(OVRInput.RawButton.LHandTrigger))
            {
                GetComponent<Rigidbody>().isKinematic = true;
                transform.parent = hand.transform;
                transform.localPosition = new Vector3 (0, -0.3f, 0);
                if (!gameOver)
                {
                    winText.text = "You Win!";
                    gameOverCanvas.SetActive(true);
                    gameOver = true;
                }
            }
        }

        if (thirdPerson != null)
        {
            Debug.Log(this.transform.parent);
            GetComponent<Renderer>().material.color = Color.blue;
            if (Input.GetButton("Fire1"))
            {
                thirdPerson.SendMessage("OnCollectedByThirdPerson");
                GetComponent<Rigidbody>().isKinematic = true;
                transform.SetParent(thirdPerson.transform.GetChild(0));
                transform.localPosition = new Vector3(0, 0, 0);
                if (!gameOver)
                {
                    winText.text = "You Win!";
                    gameOverCanvas.SetActive(true);
                    gameOver = true;
                }
            }
        }

        GetComponent<Rigidbody>().isKinematic = false;
        transform.SetParent(null);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Hand"))
        {
            hand = other.gameObject;
        }

        if (other.gameObject.CompareTag("ThirdPerson"))
        {
            thirdPerson = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        hand = null;
        thirdPerson = null;
        GetComponent<Renderer>().material.color = Color.red;
    }
}
