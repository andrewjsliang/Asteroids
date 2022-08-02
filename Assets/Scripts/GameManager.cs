using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public Movement player;
    public ParticleSystem explosion;
    public float respawnTime = 3.0f;
    public float respawnInvulnerablilityTime = 3.0f;
    public int lives{get; private set;}
    public int score{get; private set;}
    public Text livesText;
    public Text scoreText;

    private void Start()
    {
        NewGame();
    }

    public void NewGame()
    {
        SetScore(0);
        SetLives(3);
        Respawn();
        Asteroid[] asteroids = FindObjectsOfType<Asteroid>();
    }

    public void AsteroidDestroyed(Asteroid asteroid)
    {
        this.explosion.transform.position = asteroid.transform.position;
        this.explosion.Play();

        if (asteroid.size < 2.4f) {
            SetScore(score+100);
        } else if (asteroid.size < 3.1f) {
            SetScore(score+50);
        } else {
            SetScore(score+25);
        }
    }

    public void PlayerDied()
    {
        this.explosion.transform.position = this.player.transform.position;
        this.explosion.Play();

        SetLives(lives-1);

        if (this.lives <= 0) {
            GameOver();
        } else {
            Invoke(nameof(Respawn), this.respawnTime);
        }
    }

    private void Respawn()
    {
        this.player.transform.position = Vector3.zero;
        this.player.gameObject.layer = LayerMask.NameToLayer("Ignore Collisions");
        this.player.gameObject.SetActive(true);

        Invoke(nameof(TurnOnCollsions), this.respawnInvulnerablilityTime);
    }

    private void TurnOnCollsions()
    {
        this.player.gameObject.layer = LayerMask.NameToLayer("Player");
    }

    private void GameOver()
    {
        SetLives(lives=3);
        SetScore(score=0);

        Invoke(nameof(Respawn), this.respawnTime);
    }

    private void SetScore(int score)
    {
        this.score = score;
        scoreText.text = score.ToString();
    }

    private void SetLives(int lives)
    {
        this.lives = lives;
        livesText.text = lives.ToString();
    }

}
