using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour, IPooledObject
{
    public Transform target;
    public float speed = 3f;
    public float rotateSpeed = 0.0025f;
    private Rigidbody2D rb;

    public void OnObjectSpawn() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        if (!target) {
            GetTarget();
        } else {
            RotateTowardsTarget();
        }
    }

    private void FixedUpdate() {
        rb.velocity = transform.up * speed;
    }

    private void RotateTowardsTarget() {
        Vector2 targetDirection = target.position - transform.position;
        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg - 90f;
        Quaternion q = Quaternion.Euler(new Vector3(0, 0, angle));
        transform.localRotation = Quaternion.Slerp(transform.localRotation, q, rotateSpeed);
    }

    private void GetTarget() {
        if (GameObject.FindGameObjectWithTag("Player")) {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    public void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Player")) {
            other.gameObject.GetComponent<Player>().Hit();
            target = null;
        } else if (other.gameObject.CompareTag("Bullet")) {
            Destroy(other.gameObject);
        }
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            other.GetComponent<Player>().Hit();
            gameObject.SetActive(false); //Este destroy realmente va a ser una llamada a la funcion de object pool
        }

    }
}
