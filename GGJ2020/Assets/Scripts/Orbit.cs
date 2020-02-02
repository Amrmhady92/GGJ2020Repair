using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour
{
    public GameObject rotate_around_object_;
    public float orbit_speed_;

    private void Update()
    {
        transform.RotateAround(rotate_around_object_.transform.position, Vector3.back, orbit_speed_ * Time.deltaTime);
    }
}
