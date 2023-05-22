using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Color presetColor = new Color(207, 255, 0);
    Color life2Color = new Color(255, 0, 255);
    Color life1Color = new Color(0, 255, 255);

    private int health = 3;

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firingPoint;
    // [Range(0.1f, 1f)]
    // [SerializeField] private float fireRate = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        var spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = presetColor;
    }

    // Update is called once per frame
    void Update()
    {
        Shoot();
    }
    private void Shoot(){
        Instantiate(bulletPrefab, firingPoint.position, firingPoint.rotation);
        
    }
    public void HitBullet() {
        health--;
        if (health == 2) {
            var spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.color = life2Color;
        } else if (health == 1) {
            var spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.color = life1Color;
        } else if (health <= 0) {
            Application.Quit();
            Debug.Log("EndGame");
        }
    }

    public void HitShip() {
        Debug.Log("EndGame");
        Application.Quit();
    }

}
