using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour {

    private int p1;
    private int p2;
    public int size;
    private int[,] position3 = new int[3, 3];
    private int[,] position4 = new int[4, 4];
    private int[,] originalPosition3 = new int[3, 3];
    private int[,] originalPosition4 = new int[4, 4];
    private bool solveMode = false;

    void Awake() {

        PlayerPrefs.SetInt("Size", size);

        FindObjectOfType<NewMaze>().MakeNewMaze();

        if (size == 3) {
            for (int i = 0; i < size; i++) {
                for (int j = 0; j < size; j++) {
                    originalPosition3[i, j] = position3[i, j];
                    if (position3[i, j] == 0) {
                        p1 = i;
                        p2 = j;
                    }
                }
            }
        } else if (size == 4) {
            for (int i = 0; i < size; i++) {
                for (int j = 0; j < size; j++) {
                    originalPosition4[i, j] = position4[i, j];
                    if (position4[i, j] == 0) {
                        p1 = i;
                        p2 = j;
                    }
                }
            }
        }
    }

    public int GetP1() {
        return p1;
    }

    public int GetP2() {
        return p2;
    }

    public void SetP1(int p1) {
        this.p1 = p1;
    }

    public void SetP2(int p2) {
        this.p2 = p2;
    }

    public int[, ] GetPosition() {
        if (size == 3) {
            return position3;
        } else {
            return position4;
        }
    }

    public void SetPosition(int[, ] position) {
        if (size == 3) {
            position3 = position;
        } else {
            position4 = position;
        }
    }

    public int[, ] GetOriginalPosition() {
        if (size == 3) {
            return originalPosition3;
        } else {
            return originalPosition4;
        }
    }

    public void SetOriginalPosition(int[,] position) {
        if (size == 3) {
            originalPosition3 = position;
        } else {
            originalPosition4 = position;
        }
    }

    public bool GetSolveMode() {
        return solveMode;
    }

    public void SetSolveMode(bool b) {
        solveMode = b;
    }
}
