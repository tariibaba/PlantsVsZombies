using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    private Rigidbody2D rigidbody2d;
    public int Lane { get; set; }
    public int Life { get; set; } = 100;
    public bool HasEntered { get; set; }

    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        rigidbody2d.velocity = 0.3f * Vector2.left;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "NewZombieTrigger")
        {
            HasEntered = true;
        }
        else if (collision.CompareTag("ShooterBall"))
        {
            Destroy(collision.gameObject);
            Life -= 20;
            if (Life == 0) Destroy(gameObject);
        }
    }
}
