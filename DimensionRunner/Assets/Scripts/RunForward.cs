using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RunForward : MonoBehaviour
{
    public GameObject Player;
    public GameObject Camera;
    public float PlayerSpeed;
    public int number = 3;
    public Vector3 Destination;
    public Vector3 DestinationCamera;
    private float[] Locations = new float[7];
    private float[] LocationsCameraX = new float [7];
    private Vector3[] Rotations = new Vector3[7];
    public float LerpSpeed = 0.01f;
    public float Tollerance = 0.01f;
    public float x;
    public float Heigth = 50;
    public bool isJumping = false;
    public bool isComingDown = false;
    public float jumpSpeed = 0.5f;


    // Start is called before the first frame update
    void Start()
    {
        Player = this.gameObject; 

        Destination = Player.transform.position;

        Locations[0] = -5f;
        Locations[1] = -4f;
        Locations[2] = -2f;
        Locations[3] = 0;
        Locations[4] = 2f;
        Locations[5] = 4f;
        Locations[6] = 5f;

        LocationsCameraX[0] = -3.5f;
        LocationsCameraX[1] = -3f;
        LocationsCameraX[2] = -1.5f;
        LocationsCameraX[3] = 0;
        LocationsCameraX[4] = 1.5f;
        LocationsCameraX[5] = 3f;
        LocationsCameraX[6] = 3.5f;

        Rotations[0] = new Vector3(30f, -15f, 0f);
        Rotations[1] = new Vector3(30f, -10f, 0f);
        Rotations[2] = new Vector3(30f, -5f, 0f);
        Rotations[3] = new Vector3(30f, 0f, 0f);
        Rotations[4] = new Vector3(30f, 5f, 0f);
        Rotations[5] = new Vector3(30f, 10f, 0f);
        Rotations[6] = new Vector3(30f, 15f, 0f);


    }

    // Update is called once per frame
    void Update()
    {
        Run();
        Destination = new Vector3(Locations[number], Player.transform.position.y, Player.transform.position.z);
        MoveCamera();
        if(isJumping)
        {
            if(isComingDown)
            {
                Descend();
            }else
            {
                jump();
            }
        }
        LateralMovement();
    }

    void Run()
    {
        Player.transform.position += new Vector3(0, 0, PlayerSpeed) * Time.deltaTime;
    }
    void LateralMovement()
    {
        if (Vector3.Distance(Destination, Player.transform.position) > Tollerance)
        {
            
            Player.transform.position = Vector3.Lerp(Player.transform.position, Destination, LerpSpeed);
        }
        else 
        { //lo spostamento nel portale è troppo lento provare a aumentare la velocita oppure a modificare il vector3 distance
            Player.transform.position = Destination;
            if (number==6)
            {
                Player.transform.position = new Vector3(-5, Player.transform.position.y, Player.transform.position.z);
                number = 1;
            }
            else if (number == 0)
            {
                Player.transform.position = new Vector3(5, Player.transform.position.y, Player.transform.position.z);
                number = 5;
            }
        }
    }
    public void Jump()
    {
        isJumping = true;
    }

    private void jump()
    {
        if (Heigth > Player.transform.position.y)
        {
            Player.transform.position += new Vector3(0, jumpSpeed * Time.deltaTime, 0);
        }
        else
        {
            isComingDown = true;
        }
    }

    private void Descend()
    {
        Player.transform.position -= new Vector3(0, jumpSpeed * Time.deltaTime, 0);
        if (Player.transform.position.y < 0.1)
        {
            isJumping = false;
            isComingDown = false;
        }
    }

    public void MoveRight()
    {
        //Player.transform.position += new Vector3(1, 0, 0);
        if (number < 5)
        {
            number++;
            Tollerance = 0.01f;
        }
        else
        {
            number = 6;
            Tollerance = 0.05f;
            //number = 1;
        }
    }

    public void MoveLeft()
    {
        if (number > 1)
        {
            number--;
            Tollerance = 0.01f;
        }
        else
        {
            number = 0;
            Tollerance = 0.05f;
            //number = 5;
        }
    }


    public void MoveCamera()
    {
        Camera.transform.position = Vector3.Lerp(Camera.transform.position, new Vector3(LocationsCameraX[number],5,Player.transform.position.z - 5), LerpSpeed);
        Camera.transform.rotation = Quaternion.Lerp(Camera.transform.rotation,Quaternion.Euler(Rotations[number]), LerpSpeed);
    }

    private void Down()
    {

    }
}
