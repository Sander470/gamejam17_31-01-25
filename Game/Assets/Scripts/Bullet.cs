using UnityEngine;
using UnityEngine.InputSystem;

public class Bullet : MonoBehaviour
{
    public Rigidbody2D rb;
    public float bulletSpeed;

    public Vector2 shootDirection;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        rb.linearVelocity = shootDirection * bulletSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log($"Collided with {collision.gameObject.name}");
        Destroy(gameObject);
        if (collision.gameObject.tag == "Enemy")
            Destroy(collision.gameObject);
    }
}
