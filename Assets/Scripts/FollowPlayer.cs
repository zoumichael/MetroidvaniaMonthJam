using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{

    [SerializeField] private GameObject player;

    // Handles horizontal panning.
    [SerializeField] private float horizontalPan;
    [SerializeField] private float horizontalCameraSpeed;

    private bool facingRight;
    private bool horizontalPanning;
    private float targetHorizontalPan;
    private float currentHorizontalPan;

    // Handles vertical panning.
    [SerializeField] private float verticalPan;
    [SerializeField] private float verticalPanLookingDown;
    [SerializeField] private float verticalCameraSpeed;

    private bool lookingDown;
    private bool verticalPanning;
    private float targetVerticalPan;
    private float currentVerticalPan;

    // Handles Room Boundaries
    public float upBound, downBound, leftBound, rightBound;


    [SerializeField] private float timeHoldForCameraPanDown;
    private float waitTimeForCameraPanDown;

    private void Start()
    {
        // horizontal panning variables 
        facingRight = true;
        horizontalPanning = false;

        targetHorizontalPan = horizontalPan;
        currentHorizontalPan = horizontalPan;

        lookingDown = false;
        verticalPanning = false;

        targetVerticalPan = verticalPan;
        currentVerticalPan = verticalPan;

        upBound = 999;
        downBound = -999;
        leftBound = -999;
        rightBound = 999;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateDirection();
        ChangeToPlayerPosition();
        LookDown();
    }

    void ChangeToPlayerPosition()
    {
        if(horizontalPanning)
        {
            if (currentHorizontalPan < targetHorizontalPan - 0.1f)
            {
                currentHorizontalPan += Time.deltaTime * horizontalCameraSpeed;
            }
            else if (currentHorizontalPan > targetHorizontalPan + 0.1f)
            {
                currentHorizontalPan -= Time.deltaTime * horizontalCameraSpeed;
            }
            else
            {
                horizontalPanning = false;
            }
        }
        if(verticalPanning)
        {
            if (currentVerticalPan < targetVerticalPan - 0.1f)
            {
                currentVerticalPan += Time.deltaTime * verticalCameraSpeed;
            }
            else if (currentVerticalPan > targetVerticalPan + 0.1f)
            {
                currentVerticalPan -= Time.deltaTime * verticalCameraSpeed;
            }
            else
            {
                verticalPanning = false;
            }
        }

        float finalX = player.transform.position.x + currentHorizontalPan;
        if (finalX > rightBound)
            finalX = rightBound;
        else if (finalX < leftBound)
            finalX = leftBound;

        float finalY = player.transform.position.y + currentVerticalPan;
        if (finalY > upBound)
            finalY = upBound;
        else if (finalY < downBound)
            finalY = downBound;

        transform.position = new Vector3(finalX, finalY, transform.position.z);
    }

    void UpdateDirection()
    {
        float dirX = Input.GetAxisRaw("Horizontal");
        if (dirX > 0.0f) 
        {
            targetHorizontalPan = Mathf.Abs(horizontalPan);
            if (!facingRight)
                horizontalPanning = true;
            facingRight = true;
        }

        else if (dirX < 0.0f)
        {
            targetHorizontalPan = -Mathf.Abs(horizontalPan);
            if (facingRight)
                horizontalPanning = true;
            facingRight = false;
        }
    }

    void LookDown()
    {
        if (Input.GetKeyUp(KeyCode.S))
        {
            waitTimeForCameraPanDown = timeHoldForCameraPanDown;
            if (lookingDown)
                verticalPanning = true;
            lookingDown = false;
            targetVerticalPan = verticalPan;
        }
        if (Input.GetKey(KeyCode.S))
        {
            if (waitTimeForCameraPanDown <= 0)
            {
                if (!lookingDown)
                    verticalPanning = true;
                lookingDown = true;
                targetVerticalPan = verticalPanLookingDown;
            }
            else
            {
                waitTimeForCameraPanDown -= Time.deltaTime;
            }
        }
    }

    // Accessors and Mutators

    public void setUpBound(float val) { upBound = val; }
    public void setDownBound(float val) { downBound = val; }
    public void setLeftBound(float val) { leftBound = val; }
    public void setRightBound(float val) { rightBound = val; }

}
