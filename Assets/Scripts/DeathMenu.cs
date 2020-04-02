using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeathMenu : MonoBehaviour {

    public Text scoreText;
    public Text coinsText;

    public Image coinsImg;
    public Image backgroundImg;

    private bool isShowned = false;

    private float transition = 0.0f;

    // Start is called before the first frame update
    void Start() {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update() {

        if (!isShowned) {
            return;
        }
        transition += Time.deltaTime;
        backgroundImg.color = Color.Lerp(new Color(0, 0, 0, 0), Color.black, transition);
    }

    public void ToggleEndMenu(float score, int coinsCollected) {
        gameObject.SetActive(true);
        scoreText.text = ((int)score).ToString();
        coinsText.text = ((int)coinsCollected).ToString();
        isShowned = true;
        Debug.Log("Game Ended With Score: " + score + " And " + coinsCollected + " Coins.");

        FindObjectOfType<AudioManager>().Stop("WaterDrippingEffect");
        FindObjectOfType<AudioManager>().Stop("GameTheme");
        FindObjectOfType<AudioManager>().Play("DeathAmbient");
    }

    public void Restart() {
        FindObjectOfType<AudioManager>().Stop("DeathAmbient");
        FindObjectOfType<AudioManager>().Play("GameTheme");
        FindObjectOfType<AudioManager>().Play("WaterDrippingEffect");
        SceneManager.LoadScene("Game");
    }

    public void ToMenu() {
        FindObjectOfType<AudioManager>().Stop("DeathAmbient");
        FindObjectOfType<AudioManager>().Play("MenuTheme");
        SceneManager.LoadScene("Menu");
    }

}
