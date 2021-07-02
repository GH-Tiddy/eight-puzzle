using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : MonoBehaviour {

    private int p1;
    private int p2;
    public int n;
    RectTransform rectTransform;
    public GameManager gameManager;

    void Start() {

        int[,] position = gameManager.GetPosition();
        for (int i = 0; i < PlayerPrefs.GetInt("Size"); i++) {
            for (int j = 0; j < PlayerPrefs.GetInt("Size"); j++) {
                if (position[i, j] == n) {
                    p1 = i;
                    p2 = j;
                }
            }
        }
        Move(p1, p2);

    }

    public void Reset() {

        int[,] position = gameManager.GetOriginalPosition();
        for (int i = 0; i < PlayerPrefs.GetInt("Size"); i++) {
            for (int j = 0; j < PlayerPrefs.GetInt("Size"); j++) {
                if (position[i, j] == n) {
                    p1 = i;
                    p2 = j;
                }
            }
        }

        Move(p1, p2);

    }

    public void Move(int p1, int p2) {
        rectTransform = GetComponent<RectTransform>();
        Vector3 pos = rectTransform.anchoredPosition;
        if (PlayerPrefs.GetInt("Size") == 3) {
            pos.x = (p1 - 1) * 130;
            pos.y = (1 - p2) * 130;
        } else if (PlayerPrefs.GetInt("Size") == 4) {
            pos.x = (10 * p1 - 15) * 10;
            pos.y = (15 - 10 * p2) * 10;
        }
        rectTransform.anchoredPosition = pos;
    }

    public void SmoothMove(float p1, float p2) {
        StartCoroutine(MoveDelay(p1, p2));
    }

    public IEnumerator MoveDelay(float p1, float p2) {
        rectTransform = GetComponent<RectTransform>();
        Vector3 pos = rectTransform.anchoredPosition;
        if (PlayerPrefs.GetInt("Size") == 3) {
            float o1 = (pos.x / 130) + 1;
            float o2 = 1 - (pos.y / 130);
            for (int i = 0; i < 10; i++) {
                pos.x += (p1 - o1) * 13;
                pos.y += (o2 - p2) * 13;
                rectTransform.anchoredPosition = pos;
                yield return new WaitForEndOfFrame();
            }
        } else if (PlayerPrefs.GetInt("Size") == 4) {
            float o1 = (pos.x / 100) + 1.5f;
            float o2 = 1.5f - (pos.y / 100);
            for (int i = 0; i < 10; i++) {
                pos.x += (p1 - o1) * 10;
                pos.y += (o2 - p2) * 10;
                rectTransform.anchoredPosition = pos;
                yield return new WaitForEndOfFrame();
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

}