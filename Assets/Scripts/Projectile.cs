using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    public Transform target;
    private Rigidbody2D rb;
    public float speed = 5000f;
    public float rotateSpeed = 200f;
    public GameObject explosionEffect;
    public int value;
    Image img;

    public void Initialize(int v, Sprite s)
    {
        img = GetComponent<Image>();
        value = v;
        img.sprite = s;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = FindObjectOfType<CountdownTimer>().transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 direction = (Vector2)target.position - rb.position;
        direction.Normalize();
        float rotateAmount = Vector3.Cross(direction, transform.up).z;

        rb.angularVelocity = -rotateAmount * rotateSpeed;
        rb.velocity = transform.up * speed;
    }

    void OnTriggerEnter2D()
    {
        Instantiate(explosionEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
