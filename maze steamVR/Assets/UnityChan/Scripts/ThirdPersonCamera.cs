//
// Unityちゃん用の三人称カメラ
// 
// 2013/06/07 N.Kobyasahi
//
using UnityEngine;
using System.Collections;
using Valve.VR.InteractionSystem;

public class ThirdPersonCamera : MonoBehaviour
{
	Transform standardPos;			// the usual position for the camera, specified by a transform in the game
	//Transform frontPos;			// Front Camera locater
    Transform nearestPos;
    Transform furthestPos;

    float zoomSpeed = 1f;

    bool bQuickSwitch = false;  //Change Camera Position Quickly
    bool front = false;
    bool isThirdPersonView = true;

    //SteamVR_TrackedObject leftController;
    //SteamVR_TrackedObject rightController;
    Player player;
    Hand rightHand;
    Hand leftHand;

	void Start()
	{
        // 各参照の初期化
        standardPos = GameObject.Find("CamPos").transform;

        //if (GameObject.Find("FrontPos"))
            //frontPos = GameObject.Find("FrontPos").transform;

        if (GameObject.Find("NearestPos"))
            nearestPos = GameObject.Find("NearestPos").transform;

        if (GameObject.Find("FurthestPos"))
            furthestPos = GameObject.Find("FurthestPos").transform;
        
        //set standard position
        this.transform.position = standardPos.position;
        this.transform.forward = standardPos.forward;

        player = Player.instance;
    }

	
	void FixedUpdate ()
    {
        foreach (Hand hand in player.hands)
        {
            if (hand != null)
            {
                if (hand.GuessCurrentHandType().Equals(Hand.HandType.Right))
                {
                    rightHand = hand;
                    Debug.Log("Right Hand");
                }

                if (hand.GuessCurrentHandType().Equals(Hand.HandType.Left))
                {
                    leftHand = hand;
                    Debug.Log("Left Hand");
                }
            }
        }

        Vector2 touchpadPosition = new Vector2(0, 0);

        if (rightHand != null)
        {
            touchpadPosition = rightHand.controller.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis0);
        }
        else if (leftHand != null)
        {
            touchpadPosition = leftHand.controller.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis0);
        }

        //leftController = GameObject.FindWithTag("leftController").GetComponent<SteamVR_TrackedObject>();
        //rightController = GameObject.FindWithTag("rightController").GetComponent<SteamVR_TrackedObject>();
        //var leftInput = SteamVR_Controller.Input((int)leftController.index);
        //var rightInput = SteamVR_Controller.Input((int)rightController.index);

        if (Input.GetButton("Fire1"))	// left Ctlr
		{
            // Change Front Camera
            //setFirstPersonView();
            //isThirdPersonView = false;
		}
		else if(Input.GetButton("Fire2"))	//Alt
		{	
			// Change Jump Camera
			setThirdPersonView();
            isThirdPersonView = true;
		}
		else
		{	
			// return the camera to standard position and direction
			setCameraPositionNormalView();
		}

        if (isThirdPersonView)
        {
            Vector3 offset = nearestPos.position - furthestPos.position;
            Vector3 offset1 = nearestPos.position - standardPos.position;
            Vector3 offset2 = standardPos.position - furthestPos.position;
            if ( leftHand.controller.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis0).y > 0 && Vector3.Dot(offset, offset1) > 0)
            {
                standardPos.position += (nearestPos.position - furthestPos.position) * zoomSpeed * Time.deltaTime;
                Debug.Log("zoom");
            }

            if (leftHand.controller.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis0).y < 0 && Vector3.Dot(offset, offset2) > 0)
            {
                standardPos.position -= (nearestPos.position - furthestPos.position) * zoomSpeed*Time.deltaTime;
            }
        }

	}

	void setCameraPositionNormalView()
	{
		    /*if(bQuickSwitch == false){
		    // the camera to standard position and direction
						    transform.position = Vector3.Lerp(transform.position, currentPos.position, Time.fixedDeltaTime * smooth);	
						    transform.forward = Vector3.Lerp(transform.forward, currentPos.forward, Time.fixedDeltaTime * smooth);
		    }
		    else{*/
			// the camera to standard position and direction / Quick Change
			transform.position = standardPos.position;	
			transform.forward = standardPos.forward;
			bQuickSwitch = false;
		//}
	}
	
	/*void setFirstPersonView()
	{
        if (front == false)
        {
            // Change Front Camera
            transform.position = frontPos.position;
            transform.forward = frontPos.forward;
            standardPos.position = frontPos.position;
            standardPos.forward = frontPos.forward;
            front = true;
        }
	}*/

    void setThirdPersonView()
    {
        if (front == true)
        {
            transform.position = furthestPos.position;
            transform.forward = furthestPos.forward;
            standardPos.position = furthestPos.position;
            standardPos.forward = furthestPos.forward;
            front = false;
        }
    }

}
