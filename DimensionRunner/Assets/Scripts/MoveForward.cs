using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    public GameObject Portal;
    public GameObject Player;
    public GameObject Camera;
    public float speed;
    public float Distance = 0;
    private Vector3 MaxScale = new Vector3(0.2f,1.5f,0.3f);
    public bool isRight = false;
    // Start is called before the first frame update
    void Start()
    {
        Portal = this.gameObject;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (isRight)
        {
            Portal.transform.position = new Vector3(4.99f, 1.45f, Player.transform.position.z);
        }
        else
        {
            Portal.transform.position = new Vector3(-4.99f, 1.45f, Player.transform.position.z);
        }
        
        if (Vector3.Distance(Portal.transform.position, Player.transform.position) < 1.80f)
        {
            Distance = 1.70f - Mathf.Abs(Vector3.Distance(Portal.transform.position, Player.transform.position));
        }
        Portal.transform.localScale = MaxScale * (Distance * 5);
        Distance = 0;
    }
}
