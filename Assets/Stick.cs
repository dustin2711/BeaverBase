using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stick : MonoBehaviour
{
    static float friction = 1f;

    private bool IsBelowWaterLevel => transform.position.y < 0;

    Rigidbody2D body;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();    
        body.drag = friction;
    }

    void Update()
    {
        body.gravityScale = IsBelowWaterLevel ? -0.1f : 1;

        // Apply water friction
        body.AddForce(-friction * body.velocity);
    }
}
