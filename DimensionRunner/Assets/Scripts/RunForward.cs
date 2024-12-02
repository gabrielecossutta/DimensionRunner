using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class RunForward : MonoBehaviour
{
    private GameObject Player;
    public float PlayerSpeed = 1f ;
    // Start is called before the first frame update
    void Start()
    {
        Player = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        Player.transform.position += new Vector3(0, 0, PlayerSpeed) * Time.deltaTime;
    }
}
