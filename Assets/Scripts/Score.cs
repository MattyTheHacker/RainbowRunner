using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {

    private float score = 0.0f;

    private int difficultyLevel = 1;
    private int maxDifficultyLevel = 15;
    private int scoreTonextLevel = 10;
    private int coinsCollected = 0;

    private bool isDead = false;

    public Text scoreText;
    public Text CoinScoreText;

    public DeathMenu deathMenu;

    // Update is called once per frame
    void Update() {

        if (isDead) {
            return;
        }

        if(score >= scoreTonextLevel) {
            LevelUp();
        }

        score += Time.deltaTime * difficultyLevel;
        scoreText.text = ((int)score).ToString();

        CoinScoreText.text = ((int)coinsCollected).ToString();

    }

    void LevelUp() {

        if(difficultyLevel == maxDifficultyLevel) {
            return;
        }

        scoreTonextLevel *= 2;
        difficultyLevel++;

        GetComponent<PlayerMotor>().SetSpeed(difficultyLevel);

        Debug.Log("Difficulty Increased! Now: " + difficultyLevel);

    }

    public void OnCollect() {
        coinsCollected++;
    }

    public void OnDeath() {
        isDead = true;

        if(PlayerPrefs.GetFloat("Highscore") < score) {
            PlayerPrefs.SetFloat("Highscore", score);
        }  

        deathMenu.ToggleEndMenu(score, coinsCollected);
    }
}