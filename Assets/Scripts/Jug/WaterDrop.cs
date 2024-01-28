using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class WaterDrop : MonoBehaviour
{
    public Rigidbody2D rigidBody = default;

    private static WaterDrop Instance;

    private void Start()
    {
        Instance = this;
    }

    public void Update()
    {
        if (transform.position.y <= -10)
            Destroy(gameObject);
    }

    public static void DestroySelfs()
    {
        Destroy(Instance.gameObject);
    }
}
