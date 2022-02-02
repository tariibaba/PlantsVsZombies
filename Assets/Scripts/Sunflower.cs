using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sunflower : MonoBehaviour
{
    void Start()
    {
        InvokeRepeating("CreateSun", 10, 10);
    }

    private void CreateSun()
    {
        var sun = Instantiate(GameController.Instance.sunPrefab, transform);
        sun.localScale = Vector3.one;
        var sunRigidBody = sun.GetComponent<Rigidbody2D>();
        sunRigidBody.gravityScale = 1;
        sunRigidBody.AddForce(Vector2.up * 300);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
