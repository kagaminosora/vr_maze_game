//
// Unityちゃん用の三人称カメラ
// 
// 2013/06/07 N.Kobyasahi
//
using UnityEngine;
using System.Collections;


public class ThirdPersonCamera : MonoBehaviour
{
	public float smooth = 3f;		// カメラモーションのスムーズ化用変数
	Transform standardPos;			// the usual position for the camera, specified by a transform in the game
	//Transform frontPos;			// Front Camera locater
    Transform nearestPos;
    Transform furthestPos;
    // スムーズに繋がない時（クイック切り替え）用のブーリアンフラグ
    bool bQuickSwitch = false;  //Change Camera Position Quickly
    bool front = false;
    bool isThirdPersonView = true;
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
        //カメラをスタートする
        this.transform.position = standardPos.position;
        this.transform.forward = standardPos.forward;
    }

	
	void FixedUpdate ()	// このカメラ切り替えはFixedUpdate()内でないと正常に動かない
	{

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
            if (Input.GetAxis("Mouse ScrollWheel") < 0 && Vector3.Dot(offset, offset1) > 0)
            {
                standardPos.position += (nearestPos.position - furthestPos.position) * 0.1f;
            }

            if (Input.GetAxis("Mouse ScrollWheel") > 0 && Vector3.Dot(offset, offset2) > 0)
            {
                standardPos.position -= (nearestPos.position - furthestPos.position) * 0.1f;
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
