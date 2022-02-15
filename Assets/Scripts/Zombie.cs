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
        StartMoving();
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

    private void StartMoving()
    {
        rigidbody2d.velocity = 0.2f * Vector2.left;
    }

    private void Update()
    {
        var hit = Physics2D.Raycast(transform.position, Vector2.left, 0.5f, 1 << 7);
        if (hit.collider != null)
        {
            if (plantEatingCoroutine == null)
            {
                plantEatingCoroutine = StartCoroutine(StartEatingPlant(hit.collider.GetComponent<Plant>()));
            }
        }
        else
        {
            if (plantEatingCoroutine != null)
            {
                StopCoroutine(plantEatingCoroutine);
                plantEatingCoroutine = null;
            }
        }
    }

    private Coroutine plantEatingCoroutine;

    private IEnumerator StartEatingPlant(Plant plant)
    {
        rigidbody2d.velocity = Vector2.zero;
        while (true)
        {
            plant.Life -= 10;
            if (plant.Life < 0)
            {
                plant.FieldPatch.IsFilled = false;
                Destroy(plant.gameObject);
                StartMoving();
            }
            yield return new WaitForSeconds(0.5f);
        }
    }
}
