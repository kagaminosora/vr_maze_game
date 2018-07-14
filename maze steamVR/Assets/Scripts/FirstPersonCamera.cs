
using UnityEngine;
using System.Collections;


public class FirstPersonCamera : MonoBehaviour
{
	Transform standardPos;			// the usual position for the camera, specified by a transform in the game
	Transform frontPos;			// Front Camera locater
    // スムーズに繋がない時（クイック切り替え）用のブーリアンフラグ

	void Start()
	{
        // 各参照の初期化
        if (GameObject.Find("FrontPos"))
            frontPos = GameObject.Find("FrontPos").transform;
        standardPos = frontPos;
        //カメラをスタートする
        this.transform.position = standardPos.position;
        this.transform.forward = standardPos.forward;
    }

	
	void FixedUpdate ()	// このカメラ切り替えはFixedUpdate()内でないと正常に動かない
	{
        transform.position = standardPos.position;
        transform.forward = standardPos.forward;
    }
}

