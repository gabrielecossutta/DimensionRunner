using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Inputs : MonoBehaviour
{
    private Vector2 StartPosition;
    private Vector2 EndPosition;
    public RunForward player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount>0&&Input.GetTouch(0).phase==TouchPhase.Began)
        {
            StartPosition=Input.GetTouch(0).position;
            //Debug.Log(StartPosition);
        }  
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            EndPosition = Input.GetTouch(0).position;
            if (StartPosition.x > EndPosition.x && Mathf.Abs(StartPosition.x - EndPosition.x) > 100 )
            {
                //Debug.Log("left");
                player.MoveLeft();
                //left
            }
            if (StartPosition.x < EndPosition.x && Mathf.Abs(StartPosition.x - EndPosition.x) > 100)
            {
                //right
                //Debug.Log("right");
                player.MoveRight();
            }
            if (StartPosition.y > EndPosition.y && Mathf.Abs(StartPosition.y - EndPosition.y) > 100)
            {
                //Debug.Log("down");
                //down
            }
            if (StartPosition.y < EndPosition.y && Mathf.Abs(StartPosition.y - EndPosition.y) > 100)
            {
                player.Jump();
            }
            EndPosition = Vector2.zero;
            StartPosition = Vector2.zero;
            
        }
    }
}
