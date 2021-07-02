using System;
using UnityEngine;
using UnityEngine.UI;

public class MoveSquare : MonoBehaviour {

    public GameManager gameManager;
    public GameObject LevelComplete;
    public Text winner;

    public void Move(Button button) {

        int o1 = gameManager.GetP1();
        int o2 = gameManager.GetP2();
        int p1 = button.GetComponent<Square>().GetP1();
        int p2 = button.GetComponent<Square>().GetP2();

        if (Math.Abs(o1 - p1) + Math.Abs(o2 - p2) == 1) {

            FindObjectOfType<Moves>().AddMove();

            button.GetComponent<Square>().SetP1(o1);
            button.GetComponent<Square>().SetP2(o2);
            gameManager.SetP1(p1);
            gameManager.SetP2(p2);
            button.GetComponent<Square>().SmoothMove(o1, o2);

            int[,] position = gameManager.GetPosition();
            int temp = position[p1, p2];
            position[p1, p2] = 0;
            position[o1, o2] = temp;
            gameManager.SetPosition(position);

            if (CheckEnd()) {
                LevelComplete.SetActive(true);
                if (gameManager.GetSolveMode() == false) {
                    winner.text = "You Win!";
                } else {
                    winner.text = "Solved!";
                    gameManager.SetSolveMode(false);
                }
            }

        }

    }

    public bool CheckEnd() {

        int[, ] position = gameManager.GetPosition();
        if (PlayerPrefs.GetInt("Size") == 3) {
            int[,] goal = { { 1, 4, 7 }, { 2, 5, 8 }, { 3, 6, 0 } };
            for (int i = 0; i < PlayerPrefs.GetInt("Size"); i++) {
                for (int j = 0; j < PlayerPrefs.GetInt("Size"); j++) {
                    if (position[i, j] != goal[i, j]) {
                        return false;
                    }
                }
            }
            return true;
        } else if (PlayerPrefs.GetInt("Size") == 4) {
            int[,] goal = { { 1, 5, 9, 13 }, { 2, 6, 10, 14 }, { 3, 7, 11, 15 }, { 4, 8, 12, 0 } };
            for (int i = 0; i < PlayerPrefs.GetInt("Size"); i++) {
                for (int j = 0; j < PlayerPrefs.GetInt("Size"); j++) {
                    if (position[i, j] != goal[i, j]) {
                        return false;
                    }
                }
            }
            return true;
        }

        return false;
    }

}
