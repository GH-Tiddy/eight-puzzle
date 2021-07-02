using UnityEngine;
using UnityEngine.UI;

public class Reset : MonoBehaviour {

    public Button[] button;
    public GameManager gameManager;

    public void ResetButton() {

        for (int i = 0; i < button.Length; i++) {
            button[i].GetComponent<Square>().Reset();
        }

        int[, ] position = gameManager.GetPosition();
        for (int i = 0; i < PlayerPrefs.GetInt("Size"); i++) {
            for (int j = 0; j < PlayerPrefs.GetInt("Size"); j++) {
                position[i, j] = gameManager.GetOriginalPosition()[i, j];
                if (position[i, j] == 0) {
                    gameManager.SetP1(i);
                    gameManager.SetP2(j);
                }
            }
        }
        gameManager.SetPosition(position);

        FindObjectOfType<Moves>().Reset();

    }
}
