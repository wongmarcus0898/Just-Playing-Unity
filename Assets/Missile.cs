using UnityEngine;
using EZCameraShake;

public class Missile : MonoBehaviour
{
    public float speed = 3f;
    public float rotate_speed = 100f;
    private Rigidbody2D rb;
    private Vector2 screenBounds;
    private Transform target;
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    }
    void FixedUpdate()
    {
        // Missile homing movement until it tracks the player
        Vector2 direction = (Vector2)target.position - rb.position;
        direction.Normalize();

        float rotate_amount = Vector3.Cross(direction, transform.up).z;

        if (rotate_amount <= 0.05 && -0.05 <= rotate_amount)
        {
            rotate_speed = 0;
            speed = 10;
        }
            
        rb.angularVelocity = -rotate_amount * rotate_speed; 

        rb.velocity = transform.up * speed;

        // Remove object when out of screen
        if (transform.position.y > screenBounds.y * 1.5 || transform.position.y < -screenBounds.y * 1.5 || transform.position.x > screenBounds.x * 1.5 || transform.position.x < -screenBounds.x * 1.5)
        {
            Destroy(this.gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        CameraShaker.Instance.ShakeOnce(4f, 4f, 0.1f, 1f);
        Destroy(gameObject);
    }
}
