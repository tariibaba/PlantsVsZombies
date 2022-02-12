using UnityEngine;

public class Sunflower : MonoBehaviour
{
    void Start()
    {
        InvokeRepeating("CreateSun", 10, 10);
    }

    private void CreateSun()
    {
        var sun = Instantiate(GameController.Instance.sunPrefab);
        sun.transform.localScale = Vector3.one;
        sun.transform.position = transform.position;
        sun.Sunflower = this;
        var sunRigidBody = sun.GetComponent<Rigidbody2D>();
        sunRigidBody.gravityScale = 1;
        sunRigidBody.AddForce(Vector2.up * 300);
    }
}
