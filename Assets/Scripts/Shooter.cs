using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    // Start is called before the first frame update
    public int Lane { get; set; }
    public Transform ballStartPos;
    private bool hasDetectedZombie = false;
    private Coroutine zombieShootCoroutine;

    // Update is called once per frame
    void Update()
    {
        var hit = Physics2D.Raycast(transform.position, Vector2.right, Mathf.Infinity, 1 << 9);
        hasDetectedZombie = hit.collider != null && hit.collider.GetComponent<Zombie>().HasEntered;
        if (hasDetectedZombie)
        {
            var zombie = hit.collider.GetComponent<Zombie>();
            if (zombieShootCoroutine == null && zombie.HasEntered)
            {
                zombieShootCoroutine = StartCoroutine(StartShootingZombie());
            }
        }
        else
        {
            if (zombieShootCoroutine != null)
            {
                StopCoroutine(zombieShootCoroutine);
                zombieShootCoroutine = null;
            }
        }
    }

    private IEnumerator StartShootingZombie()
    {
        while (true)
        {
            var ball = Instantiate(GameController.Instance.shooterBallPrefab);
            ball.transform.position = ballStartPos.position;
            ball.GetComponent<Rigidbody2D>().velocity = 2.5f * Vector2.right;
            yield return new WaitForSeconds(3);
        }
    }
}
