using UnityEngine;

public class Beaver : MonoBehaviour
{
    // Static

    private static Vector2 defaultForce => new Vector2(40, 30);
    private static float friction => 1f;
    private static double velocityLimit => 5;

    // Instance

    private bool IsBelowWaterLevel => transform.position.y < 0;

    private Vector2 acceleration;
    private Vector2 lastVelocity;
    private Rigidbody2D body;
    private new SpriteRenderer renderer;

    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
        renderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        MoveInWater();

        body.gravityScale = IsBelowWaterLevel ? -0.0003f : 1;

        body.angularVelocity = 0;

        // Control velocity so that beaver gets rotation = 0
        //body.angularVelocity -= body.angularVelocity.Sign() * 10f * transform.eulerAngles.z;

        //Debug.Log("VelocityX = " + body.velocity.x);
    }

    private void MoveInWater()
    {
        //body.angularVelocity *= 0.3f;

        body.drag = friction;
        body.angularDrag = 1;

        // Apply gravity when not in water

        // Create force from key input
        Vector2 force = new Vector2();

        if (Input.GetKey(KeyCode.A))
        {
            force.x -= defaultForce.x;
        }
        if (Input.GetKey(KeyCode.D))
        {
            force.x += defaultForce.x;
        }
        // Can only accelerate up when in water
        if (Input.GetKey(KeyCode.W) && IsBelowWaterLevel)
        {
            force.y += defaultForce.y;
        }
        if (Input.GetKey(KeyCode.S))
        {
            force.y -= defaultForce.y;
        }

        // Apply key input
        if (force != default)
        {
            body.AddForce(force);

            bool velocityDirectionChanged = lastVelocity.x.Sign() != body.velocity.x.Sign();
            bool velocityLimitTranscended =
                   (body.velocity.x.Abs() < velocityLimit && velocityLimit < lastVelocity.x.Abs())
                || (lastVelocity.x.Abs() < velocityLimit && velocityLimit < body.velocity.x.Abs());

            // Flip sprite when direction changes or limit is exceeded
            if (velocityDirectionChanged || velocityLimitTranscended)
            {
                if (body.velocity.x.Abs() < velocityLimit)
                {
                    renderer.flipX = force.x < 0;
                }
                else
                {
                    renderer.flipX = body.velocity.x < 0;
                }
            }
        }

        // Apply water friction
        //body.AddForce(-friction * body.velocity);

        lastVelocity = body.velocity;
    }


    //DateTime lastSpriteFlipTime = DateTime.Now;
    //private void MayFlipSprite(bool flip)
    //{
    //    float ms = (DateTime.Now - lastSpriteFlipTime).Milliseconds;
    //    //Debug.Log(ms);
    //    if (true)//ms > 500)
    //    {
    //        renderer.flipX = !flip;
    //        lastSpriteFlipTime = DateTime.Now;
    //    }
    //}
}
