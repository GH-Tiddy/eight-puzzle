using UnityEngine;
using UnityEngine.UI;

public class BestMoves : MonoBehaviour {

    public Text text;

    public void Start () {

        if (PlayerPrefs.GetInt("Size") == 3) {
            int bestMoves = FindObjectOfType<Solution>().BestMoves();
            text.text = "Lowest Possible Moves: " + bestMoves;
        }

    }

}
