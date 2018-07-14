
using UnityEngine;
using Valve.VR.InteractionSystem;

[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Rigidbody))]

public class PlayerController1 : MonoBehaviour
{
    // Player's speed
    public float forwardSpeed = 3.5f;
    public float backwardSpeed = 2.0f;
    public float rotateSpeed = 1.0f;

    private Rigidbody rb;

    // Velocity added to the player
    private Vector3 velocity;

    Hand rightHand;
    Hand leftHand;
    Player player;

    // Initialization
    void Start()
    {
        player = Player.instance;
        rb = GetComponent<Rigidbody>();
        GameObject.Find("Record").SendMessage("IsFirstPersonView");
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
        float v, h;

        if (rightHand != null)
        {
            touchpadPosition = rightHand.controller.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis0);
        }
        else if(leftHand != null)
        {
            touchpadPosition = leftHand.controller.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis0);
        }

        v = touchpadPosition.y;
        h = touchpadPosition.x;

        rb.useGravity = true;

        velocity = new Vector3(0, 0, v);        //set y axis of the touch position as player's velocity in z axis

        velocity = transform.TransformDirection(velocity);

        //Check whether the conroller is connected
        //if (rightHand.controller.GetPressDown(Valve.VR.EVRButtonId.k_EButton_A))
        //{
        //     Debug.Log("Controller connected");
        //}

        //Deal with the movement
        if (v > 0.1)
        {
            velocity *= forwardSpeed;
            Debug.Log("Up");
        }

        if (v < -0.1)
        {
            velocity *= backwardSpeed;
        }

        transform.localPosition += velocity * Time.fixedDeltaTime;

        transform.Rotate(0, h * rotateSpeed, 0);
    }
}

