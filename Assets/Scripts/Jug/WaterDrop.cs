using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class WaterDrop : MonoBehaviour
{
    public Rigidbody2D rigidBody = default;

    public void Update()
    {
        if (transform.position.y <= -10)
            Destroy(gameObject);
    }
}
