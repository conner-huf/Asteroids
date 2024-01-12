using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour{

    public float projectileSpeed = 500.0f;
    public float maxLifetime = 10.0f;
    private Rigidbody2D _rb;

    private void Awake() {
        _rb = GetComponent<Rigidbody2D>();
    }

    public void Project(Vector2 direction) {
        _rb.AddForce(direction * projectileSpeed);
        FindObjectOfType<AudioManager>().Play("LaserGun");

        Destroy(this.gameObject, maxLifetime);
    }

    private void OnCollisionEnter2D(Collision2D col) {
        Destroy(this.gameObject);
    }
}
