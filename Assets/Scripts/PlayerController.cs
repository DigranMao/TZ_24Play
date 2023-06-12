using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject playPanel;
    [SerializeField] private float speedX = 5f, speedZ = 10f, acceleration = 5f;
    
    private Rigidbody rb;
    private Stickman stickman;
    private Vector2 initialTouchPosition;

    private float minX = -2f, maxX = 2f, currentSpeedX = 0f, initialPlayerPositionX;
    private bool isPlaying = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        stickman = GetComponentInChildren<Stickman>();
    }

    void FixedUpdate()
    {
        if(!stickman.loseGame)
        {
            TrackMouseClick();

            if(isPlaying)
            {
                Vector3 forwardMovement = new Vector3(0f, 0f, speedZ) * Time.fixedDeltaTime;
                rb.MovePosition(rb.position + forwardMovement);
            }
        }
    }

    void TrackMouseClick()
    {
        if (Input.touchCount > 0)
        {
            isPlaying = true;
            playPanel.SetActive(false);
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                initialTouchPosition = touch.position;
                initialPlayerPositionX = rb.position.x;
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                float touchDelta = (touch.position.x - initialTouchPosition.x) / (Screen.width / 4f); 

                float targetX = initialPlayerPositionX + touchDelta * maxX;

                targetX = Mathf.Clamp(targetX, minX, maxX);
                rb.MovePosition(new Vector3(targetX, rb.position.y, rb.position.z));
            }
            else if (touch.phase == TouchPhase.Ended)
                initialTouchPosition = Vector2.zero; 
        }
    }
}
