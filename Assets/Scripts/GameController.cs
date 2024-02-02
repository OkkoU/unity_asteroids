using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject asteroid;

    private int score;
    private int highScore;
    private int asteroidsRemaining;
    private int lives;
    private int wave;
    private int increaseEachWave = 4;

    public Text scoreText;
    public Text livesText;
    public Text waveText;
    public Text highScoreText;


    void Start()
    {
        highScore = PlayerPrefs.GetInt("High Score", 0);
        BeginGame();
    }


    void BeginGame()
    {
        score = 0;
        lives = 3;
        wave = 1;

        scoreText.text = "Score: " + score;
        highScoreText.text = "High Score: " + highScore;
        livesText.text = "Lives: " + lives;
        waveText.text = "Wave: " + wave;

        SpawnAsteroids();
    }


    void SpawnAsteroids()
    {
        DestroyExistingAsteroids();

        // Decide how many asteroids to spawn
        // If any asteroids left over from previous game, subtract them
        asteroidsRemaining = wave * increaseEachWave;

        for (int i = 0; i < asteroidsRemaining; i++)
        {
            // Spawn an asteroid
            Instantiate(asteroid, new Vector3(Random.Range(-9.0f, 9.0f), Random.Range(-6.0f, 6.0f), 0), Quaternion.Euler(0, 0, Random.Range(-0.0f, 359.0f)));
        }

        waveText.text = "Wave: " + wave;
    }


    public void IncrementScore()
    {
        score++;
        scoreText.text = "Score: " + score;

        if (score > highScore)
        {
            highScore = score;
            highScoreText.text = "High Score: " + highScore;

            // Save the new hiscore
            PlayerPrefs.SetInt("High Score", highScore);
        }

        // Has player destroyed all asteroids?
        if (asteroidsRemaining < 1)
        {
            // Start next wave
            wave++;
            SpawnAsteroids();
        }
    }


    public void DecrementLives()
    {
        lives--;
        livesText.text = "Lives: " + lives;

        // Has player run out of lives?
        if (lives < 1)
        {
            // Restart the game
            BeginGame();
        }
    }


    public void DecrementAsteroids()
    {
        asteroidsRemaining--;
    }


    public void SplitAsteroid()
    {
        // Two extra asteroids
        // - big one
        // + 3 little ones
        // = 2
        asteroidsRemaining += 2;
    }


    void DestroyExistingAsteroids()
    {
        GameObject[] asteroids = GameObject.FindGameObjectsWithTag("Large Asteroid");

        foreach (GameObject current in asteroids)
        {
            GameObject.Destroy(current);
        }

        GameObject[] asteroids2 = GameObject.FindGameObjectsWithTag("Small Asteroid");

        foreach (GameObject current in asteroids2)
        {
            GameObject.Destroy(current);
        }
    }
}
