using UnityEngine;

public class Ground : MonoBehaviour
{
    private bool onGround;
    private bool onSlope;
    public float friction;
    public Vector2 normal;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        EvaluateCollsion(collision);
        RetrieveFriction(collision);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        EvaluateCollsion(collision);
        RetrieveFriction(collision);
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        onGround = false;
        friction = 0.0f;
    }

    private void EvaluateCollsion(Collision2D collision)
    {
        for (int i = 0; i < collision.contactCount; i++) 
        {
            normal = collision.GetContact(i).normal;
            onGround |= normal.y >= 0.9f;
            onSlope |= normal.y >= 0.6f;
        }
    }

    private void RetrieveFriction(Collision2D collision) 
    {
        PhysicsMaterial2D mat = collision.rigidbody.sharedMaterial;
        friction = 0;
        if (mat != null) 
        {
            friction = mat.friction;
        }
    }

    public bool GetOnGround()
    {
        return onGround;
    }

    public bool GetOnSlope()
    {
        return onSlope;
    }

    public float GetFriction()
    {
        return friction;
    }
}
