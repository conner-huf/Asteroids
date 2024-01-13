using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public Bullet bulletPrefab;
    public float thrustSpeed = 2.0f;
    public float turnSpeed = 0.5f;
    private bool _thrusting;
    private float _turnDirection;
    private Rigidbody2D _rb;
    private AudioManager _audioManager;

    private void Awake() {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        _thrusting = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
            _turnDirection = 1.0f;
        } else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
            _turnDirection = -1.0f;
        } else {
            _turnDirection = 0.0f;
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0)) {
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) {
            FindObjectOfType<AudioManager>().Play("Engine");
        }
    }

    private void FixedUpdate() {
        if (_thrusting) {
            _rb.AddForce(transform.up * thrustSpeed);
        }

        if (_turnDirection != 0.0f) {
            _rb.AddTorque(_turnDirection * turnSpeed);
        }
    }

    private void Shoot() {
        Bullet bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
        bullet.Project(transform.up);
    }

    private void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.tag == "Asteroid") {

            _rb.velocity = Vector2.zero;
            _rb.angularVelocity = 0.0f;

            this.gameObject.SetActive(false);

            FindObjectOfType<AudioManager>().Play("SimpleExplosion");
            FindObjectOfType<GameManager>().PlayerDied();

        }
    }
}
