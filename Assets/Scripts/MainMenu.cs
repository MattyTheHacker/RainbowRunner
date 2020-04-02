using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

    public Text highscoreText; 

    // Start is called before the first frame update
    void Start() {
        Debug.Log(PlayerPrefs.GetFloat("Highscore"));
        highscoreText.text = "Highscore: " + ((int)PlayerPrefs.GetFloat("Highscore")).ToString();
    }

    public void ToGame() {
        FindObjectOfType<AudioManager>().Stop("MenuTheme");
        FindObjectOfType<AudioManager>().Play("GameTheme");
        FindObjectOfType<AudioManager>().Play("WaterDrippingEffect");
        SceneManager.LoadScene("Game");
    }

    public void OpenSettings() {
        SceneManager.LoadScene("Options");
    }

    public void ExitGame() {
        Application.Quit();
    }
}