using UnityEngine;
using UnityEngine.UI;

public class Moves : MonoBehaviour {

    public Text text;
    public GameManager gameManager;

	void Start () {
        PlayerPrefs.SetInt("Moves", 0);
	}

    public void AddMove() {
        PlayerPrefs.SetInt("Moves", PlayerPrefs.GetInt("Moves") + 1);
        text.text = "Moves: " + PlayerPrefs.GetInt("Moves");
    }

    public void Reset() {
        PlayerPrefs.SetInt("Moves", 0);
        text.text = "Moves: " + 0;
    }

}
