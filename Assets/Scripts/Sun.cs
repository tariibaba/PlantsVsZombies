using UnityEngine;
using System.Collections;

public class Sun : MonoBehaviour
{
    public float descendSpeed = 1;
    private Rigidbody2D rigidBody;
    private const float DisappearTime = 5f;

    void Start()
    {
        tag = "Sun";
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.velocity = Vector2.down * descendSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("SunStopTrigger"))
        {
            rigidBody.velocity = Vector2.zero;
            StartCoroutine(SunStopped());
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var raycast = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (raycast.collider && raycast.collider.gameObject == gameObject)
            {
                GameController.Instance.Data.Sun.Value += 25;
                Destroy(gameObject);
            }
        }
    }

    private IEnumerator SunStopped()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
}
