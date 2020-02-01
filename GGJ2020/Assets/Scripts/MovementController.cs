using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    [Range(1, 4)]
    public int playerNumber = 1;

    private float x, y = 0;
    public float xIn, yIn = 0;
    private float angle;
    public float speed = 1;

    public Rigidbody2D rb;
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    void Update()
    {


        xIn = Input.GetAxis("LeftAxisH_P" + playerNumber);
        yIn = Input.GetAxis("LeftAxisV_P" + playerNumber);
        x = Mathf.Abs(xIn) < 0.01f ? x : xIn;
        y = Mathf.Abs(yIn) < 0.01f ? y : yIn;

        if(xIn != 0 || yIn != 0)
        {
            angle = Mathf.Atan2(-x, y) * Mathf.Rad2Deg;
            this.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
         rb.velocity = new Vector2(xIn,yIn) * speed;

    }
}
