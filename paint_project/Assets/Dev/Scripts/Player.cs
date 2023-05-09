using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 5f;

    private Rigidbody2D rb;
    private float mx;
    private float my;

    private Vector2 mousePos;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        mx = Input.GetAxisRaw("Horizontal");
        my = Input.GetAxisRaw("Vertical");
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        float angle = Mathf.Atan2(mousePos.y - transform.position.y, mousePos.x -
        transform.position.x) * Mathf.Rad2Deg - 90f;

        transform.localRotation = Quaternion.Euler(0, 0, angle);
    }

    private void FixedUpdate() {
        rb.velocity = new Vector2(mx, my).normalized * speed;
    }
}
