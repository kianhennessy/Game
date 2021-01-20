using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerControllerX : MonoBehaviour
{
    public bool gameOver;

    public bool isGameActive;

    public float floatForce;
    
    private Rigidbody playerRb;

    public ParticleSystem explosionParticle;
    public ParticleSystem fireworksParticle;

    private AudioSource playerAudio;
    public AudioClip moneySound;
    public AudioClip explodeSound;

    private int score;
    public TextMeshProUGUI scoreText;

    public TextMeshProUGUI gameOverText;

    public TextMeshProUGUI playGame;

    private Button button;

    // Start is called before the first frame update
    void Start()
    {
        
        isGameActive = false;

        button = GetComponent<Button>();

    }

    // Update is called once per frame
    void Update()
    {
        // While space is pressed and player is low enough, float up
        if (Input.GetKey(KeyCode.Space) && !gameOver && transform.position.y < 5)
        {
            playerRb.AddForce(Vector3.up * floatForce);
        }

        else if(Input.GetKey(KeyCode.Space) && !gameOver && transform.position.y > 5)
        {
            
        }


    }

    private void OnCollisionEnter(Collision other)
    {
        // if player collides with bomb, explode and set gameOver to true
        if (other.gameObject.CompareTag("Bomb"))
        {
            explosionParticle.Play();
            playerAudio.PlayOneShot(explodeSound, 1.0f);
            gameOver = true;
            Debug.Log("Game Over!");
            Destroy(other.gameObject);
            gameOverText.gameObject.SetActive(true);
            isGameActive = false;
        } 

        // if player collides with money, fireworks
        else if (other.gameObject.CompareTag("Money"))
        {
            fireworksParticle.Play();
            playerAudio.PlayOneShot(moneySound, 1.0f);
            Destroy(other.gameObject);
            UpdateScore(1);
        }

    }

    void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }


    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame()
    {
        playerAudio = GetComponent<AudioSource>();
        playerRb = GetComponent<Rigidbody>();

        // Apply a small upward force at the start of the game
        playerRb.AddForce(Vector3.up * 1 * Time.deltaTime, ForceMode.Impulse);


        isGameActive = true;
        score = 0;
        UpdateScore(0);
        playGame.gameObject.SetActive(false);


    }
}
