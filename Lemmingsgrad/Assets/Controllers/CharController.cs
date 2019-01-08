using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour
{
    public float MaxSpeed = 4.5f;
    public Direction IsFacing = Direction.Right;

    void Start() {
    }

    void FixedUpdate()
    {
        float move = IsFacing == Direction.Right ? 1.0f : -1.0f;

        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        if (IsGrounded())
        {
            rb.velocity = new Vector2(move * MaxSpeed, rb.velocity.y);
        }

        if (IsHittingTile())
            Flip();
    }

    void Flip()
    {
        IsFacing = IsFacing == Direction.Right ? Direction.Left : Direction.Right;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    bool IsGrounded()
    {
        return Physics2D.Raycast(transform.position - new Vector3(0, 0.55f, 0), -Vector2.up, 0.2f).collider != null;
    }

    bool IsHittingTile()
    {
        Vector2 direction = IsFacing == Direction.Right ? Vector2.right : -Vector2.right;

        RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(direction.x * 0.55f, 0, 0), direction, 0.2f);

        Debug.DrawRay(transform.position + new Vector3(direction.x * 0.55f, 0, 0), direction, Color.green, 0.005f);

        Collider2D collider = hit.collider;
        if (collider == null)
            return false;
        if (collider.name.Contains("Inter")) // Not Changing direction for interactable.
            return false;
        if (collider.GetComponent<CharController>() != null) // Not Changing direction for Characters
            return false;
        return true;
    }
}
