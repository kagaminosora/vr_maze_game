using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideController : MonoBehaviour {
    UnityEngine.AI.NavMeshAgent nav;
    public GameObject target;
    public GameObject player;
    Animator anim;
    private bool once = true;
    private bool findTarget = false;
	// Use this for initialization
	void Awake () {
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        nav.SetDestination(target.transform.position);
        //player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
        GameObject.Find("Record").SendMessage("ThereIsGuide");
    }
	
	// Update is called once per frame
	void Update () {
        
        float distanceFromPlayer = Vector3.Distance(player.transform.position, transform.position);
        float distanceFromTarget = Vector3.Distance(target.transform.position, transform.position);

        if(distanceFromTarget < 1.5f)
        {
                findTarget = true;
        }

        if(!findTarget)
        {
            if (distanceFromPlayer < 5f)
            {
                nav.enabled = true;
                nav.SetDestination(target.transform.position);
                anim.SetBool("IsNavigating", true);
            }
            else
            {
                nav.enabled = false;
                anim.SetBool("IsNavigating", false);
                transform.forward = player.transform.position - transform.position;
            }
        }
        else
        {
            if (once)
            {
                nav.enabled = false;
                anim.SetBool("IsNavigating", false);
                once = false;
            }
            transform.forward = player.transform.position - transform.position;
        }
	}
}
