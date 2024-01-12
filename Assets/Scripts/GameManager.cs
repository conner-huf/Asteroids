using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour{

    public Text scoreText;
    public Text scoreLabel;
    public Text livesText;
    public Text livesLabel;
    public Player player;
    public ParticleSystem explosion;
    public int lives = 3;
    public float respawnTimer = 3.0f;
    public float invulnerabilityTimer = 3.0f;
    public int score = 0;
    public bool gameHasEnded = false;
    private GameOverMenu gameOverMenu;

    private void Awake() {
        gameOverMenu = FindObjectOfType<GameOverMenu>();
    }

    private void Start() {
        livesText.text = lives.ToString();
        scoreText.text = score.ToString();
        scoreLabel.text = "Score:";
        livesLabel.text = "Lives:";
    }


    public void AsteroidDestroyed(Asteroid asteroid) {
        this.explosion.transform.position = asteroid.transform.position;
        this.explosion.Play();

        if (asteroid.size < 1.5f) {
            score += 100;
        } else if (asteroid.size < 2.25f) {
            score += 50;
        } else {
            score += 25;
        }
        scoreText.text = score.ToString();
    }

    public void PlayerDied() {
        this.explosion.transform.position = this.player.transform.position;
        this.explosion.Play();
        lives--;
        livesText.text = lives.ToString();

        if (lives <= 0) {
            GameOver();
        } else {
            Invoke(nameof(RespawnPlayer), respawnTimer);
        }

    }

    private void RespawnPlayer() {

        livesText.text = lives.ToString();
        scoreText.text = score.ToString();

        player.transform.position = Vector3.zero;
        player.gameObject.SetActive(true);
        this.player.gameObject.layer = LayerMask.NameToLayer("IgnoreCollisions");
        Invoke(nameof(TurnOnCollisions), invulnerabilityTimer);
    }

    private void TurnOnCollisions() {
        this.player.gameObject.layer = LayerMask.NameToLayer("Player");
    }

    private void GameOver() {
        if (gameHasEnded == false){

            gameHasEnded = true;

            Debug.Log("Game Over!");
            gameOverMenu.GameOver();
            lives = 3;
            score = 0;

        }

        Invoke(nameof(RespawnPlayer), respawnTimer);
    }

}
