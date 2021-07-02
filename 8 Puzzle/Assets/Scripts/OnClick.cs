using System;
using UnityEngine;
using UnityEngine.UI;

public class OnClick : MonoBehaviour {

    public Button button;
    public GameManager gameManager;

	public void OnClickEnter() {
        if (!gameManager.GetSolveMode()) {
            FindObjectOfType<MoveSquare>().Move(button);
        }
    }

}
