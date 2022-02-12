using UnityEngine;
using System.Collections;

public class Sun : MonoBehaviour
{
    public float descendSpeed = 1;
    private Rigidbody2D rigidBody;
    public Sunflower Sunflower { get; set; }

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
        else if (collision.gameObject.CompareTag("FieldPatchGround"))
        {
            if (Sunflower != null)
            {
                var hitLocalGround = collision.transform.parent == Sunflower.transform.parent;
                if (hitLocalGround)
                {
                    rigidBody.gravityScale = 0;
                    rigidBody.velocity = Vector2.zero;
                    StartCoroutine(SunStopped());
                }
            }
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var camera = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var raycast = Physics2D.Raycast(camera, Vector2.zero);
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
