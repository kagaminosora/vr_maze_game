//
// Mecanimのアニメーションデータが、原点で移動しない場合の Rigidbody付きコントローラ
// サンプル
// 2014/03/13 N.Kobyasahi
//
using UnityEngine;
using Valve.VR.InteractionSystem;

// 必要なコンポーネントの列記
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Rigidbody))]

public class ConveyControllerInput : MonoBehaviour
{

    Hand rightHand;
    Hand leftHand;
    Player player;
    public GameObject unitychan;

    // Initialization
    void Start()
    {
        player = Player.instance;
    }

    //Update the mmovement of the player
    void FixedUpdate()
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

                if(hand.GuessCurrentHandType().Equals(Hand.HandType.Left))
                {
                    leftHand = hand;
                    Debug.Log("Left Hand");
                }
            }
        }

        Vector2 touchpadPosition = new Vector2(0,0);

        if (rightHand != null)
        {
            if (rightHand.controller.GetPressDown(Valve.VR.EVRButtonId.k_EButton_Axis0))
            {
                GameObject.Find("ThrowableBall").SendMessage("TouchPadPressed");
            }
            touchpadPosition = rightHand.controller.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis0);
            GameObject.Find("unitychan").SendMessage("TouchPadPosition", touchpadPosition);
        }

        if(leftHand != null)
        {
            if (leftHand.controller.GetPressDown(Valve.VR.EVRButtonId.k_EButton_Axis0))
            {
                GameObject.Find("ThrowableBall").SendMessage("TouchPadPressed");
            }
            touchpadPosition = leftHand.controller.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis0);
            GameObject.Find("Player").SendMessage("ZoomView", touchpadPosition);
        }
    }
}

