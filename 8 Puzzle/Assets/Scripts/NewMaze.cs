using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewMaze : MonoBehaviour {

    public GameManager gameManager;
    public int n;

    public void NewMazeButton() {

        if ((n == 3) && (PlayerPrefs.GetInt("Size") == 4)) {
            SceneManager.LoadScene(0);
        } else if ((n == 4) && (PlayerPrefs.GetInt("Size") == 3)) {
            SceneManager.LoadScene(1);
        } else {
            MakeNewMaze();
        }
    }

    public void MakeNewMaze() {

        int[,] position = new int[PlayerPrefs.GetInt("Size"), PlayerPrefs.GetInt("Size")];

        do {

            for (int i = 0; i < PlayerPrefs.GetInt("Size"); i++) {
                for (int j = 0; j < PlayerPrefs.GetInt("Size"); j++) {
                    position[i, j] = -1;
                }
            }

            for (int i = 0; i < PlayerPrefs.GetInt("Size"); i++) {
                for (int j = 0; j < PlayerPrefs.GetInt("Size"); j++) {
                    int a;
                    do {
                        a = UnityEngine.Random.Range(0, PlayerPrefs.GetInt("Size") * PlayerPrefs.GetInt("Size"));
                    } while (Contains(a, position));
                    position[i, j] = a;
                }
            }
        } while (!LegalMaze(position));

        gameManager.SetOriginalPosition(position);
        FindObjectOfType<Reset>().ResetButton();
        FindObjectOfType<BestMoves>().Start();

    }

    private bool Contains(int a, int[,] arr) {
        for (int i = 0; i < PlayerPrefs.GetInt("Size"); i++) {
            for (int j = 0; j < PlayerPrefs.GetInt("Size"); j++) {
                if (a == arr[i, j]) {
                    return true;
                }
            }
        }
        return false;
    }

    private bool LegalMaze(int[,] arr) {

        int invertions = 0;
        List<int> pos = new List<int>(n * n);
        for (int j = 0; j < PlayerPrefs.GetInt("Size"); j++) {
            for (int i = 0; i < PlayerPrefs.GetInt("Size"); i++) {
                pos.Add(arr[i, j]);
            }
        }

        for (int i = 0; i < 9; i++)
        {
            for (int j = i; j < 9; j++)
            {
                if (((pos[i] > pos[j]) && (pos[i] != 0)) && (pos[j] != 0))
                {
                    invertions++;
                }
            }
        }

        if ((n % 2) == 1) {

            if ((invertions % 2) == 0) {
                return true;
            } else {
                return false;
            }

        }
        else {
            for (int i = 0; i < PlayerPrefs.GetInt("Size"); i++) {
                for (int j = 0; j < PlayerPrefs.GetInt("Size"); j++) {
                    if (arr[i, j] == 0) {
                        if ((j % 2) == 1) {
                            if ((invertions % 2) == 1) {
                                return true;
                            } else {
                                return false;
                            }
                        } else {
                            if ((invertions % 2) == 0) {
                                return true;
                            } else {
                                return false;
                            }
                        }
                    }
                }
            }
        }
        return false;

    }
}
