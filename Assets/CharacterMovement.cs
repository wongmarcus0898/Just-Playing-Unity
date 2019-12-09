using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private LayerMask platformLayerMask;
    private Rigidbody2D rigidBody2d;
    private BoxCollider2D boxCollider2d;
    private Quaternion originalRotationValue;

    float rotationResetSpeed = 1f;
    public float jump_velocity;

    bool jump_boost;
    private void Awake()
    {
        rigidBody2d = transform.GetComponent<Rigidbody2D>();
        boxCollider2d = transform.GetComponent<BoxCollider2D>();
        originalRotationValue = transform.rotation;
    }

    private void FixedUpdate()
    {
        if (is_ground())
        {
            jump_boost = true;
            jump_velocity = 15;
            transform.rotation = Quaternion.Slerp(transform.rotation, originalRotationValue, Time.time * rotationResetSpeed);
        }

        jump();
        movement();
    }

    
    private bool is_ground()
    {
        RaycastHit2D raycasthit2d = Physics2D.BoxCast(boxCollider2d.bounds.center, boxCollider2d.bounds.size, 0f, Vector2.down, 0.1f, platformLayerMask);
        return raycasthit2d.collider != null;
    }

    private void movement()
    {
        float move_speed = 0f;
        if (Input.GetKey(KeyCode.D))
        {
            move_speed = 10f;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            move_speed = -10f;
        }

        rigidBody2d.velocity = new Vector2(move_speed, rigidBody2d.velocity.y);
    }

    private void jump()
    {
        if (is_ground() && Input.GetKeyDown(KeyCode.W))
        {
            rigidBody2d.velocity = Vector2.up * jump_velocity;
        }
        else if (is_ground() == false && jump_boost && Input.GetKeyDown(KeyCode.W))
        {
            jump_boost = false;
            jump_velocity = 11;
            rigidBody2d.velocity = Vector2.up * jump_velocity;
        }

        if (is_ground() == false && Input.GetKey(KeyCode.D)) transform.Rotate(Vector3.forward * -18);
        else if (is_ground() == false && Input.GetKey(KeyCode.A)) transform.Rotate(Vector3.forward * 18);
        else if (is_ground() == false && jump_boost == false) transform.Rotate(Vector3.forward * -18);
    }
}
