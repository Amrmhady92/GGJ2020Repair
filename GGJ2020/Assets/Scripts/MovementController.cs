using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    [Range(1, 4)]
    public int playerNumber = 1;

    public float x, y = 0;
    public float xIn, yIn = 0;
    public float angle;
    void Start()
    {

    }

    void Update()
    {


        xIn = Input.GetAxis("LeftAxisH_P" + playerNumber);
        yIn = Input.GetAxis("LeftAxisV_P" + playerNumber);
        x = xIn == Mathf.Abs(0.001f) ? x : xIn;
        y = yIn == Mathf.Abs(0.001f) ? y : yIn;

        angle = Mathf.Atan2(x, y) * Mathf.Rad2Deg;

        this.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
}
