using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour
{
    private GameObject rotate_around_object_;
    public float orbit_speed_;

    private void Start()
    {
        rotate_around_object_ = transform.parent.gameObject;
    }

    private void Update()
    {
        transform.RotateAround(rotate_around_object_.transform.position, Vector3.back, orbit_speed_ * Time.deltaTime);
        transform.rotation = Quaternion.Euler(0, 0, 0);
      
    }
}
