using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{

    public Sprite[] sprites;
    public float size = 1.0f;
    public float minSize = 1.0f;
    public float maxSize = 3.0f;
    public float speed = 100.0f;
    public float maxLifetime = 40.0f;
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rb;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Start(){
        _spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
        transform.eulerAngles = new Vector3(0.0f, 0.0f, Random.value * 360.0f);
        transform.localScale = Vector3.one * this.size;

        _rb.mass = this.size;
    }

    public void SetTrajectory(Vector2 direction) {
        _rb.AddForce(direction * speed);
        _rb.AddTorque(Random.Range(-10.0f * (1 / this.size), 10.0f * (1 / this.size)));

        Destroy(this.gameObject, maxLifetime);
    }

    private void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.tag == "Bullet") {
            if (this.size >= this.minSize * 2) {
                CreateSplit();
                CreateSplit();
            }

            FindObjectOfType<GameManager>().AsteroidDestroyed(this);
            FindObjectOfType<AudioManager>().Play("SimpleExplosion");
            Destroy(this.gameObject);

        }
    }

    private void CreateSplit() {

        Vector2 position = this.transform.position;
        position += Random.insideUnitCircle * 0.5f;

        Asteroid half = Instantiate(this, position, this.transform.rotation);
        half.size = this.size * 0.5f;

        half.SetTrajectory(Random.insideUnitCircle.normalized);
    }
}
