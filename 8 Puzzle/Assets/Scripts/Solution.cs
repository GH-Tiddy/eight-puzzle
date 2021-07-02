using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using George;

public class Solution : MonoBehaviour {

    public Button[] button;
    public static int n;
    public GameManager gameManager;

    public void SolutionButton() {

        gameManager.SetSolveMode(true);

        Node node = AISolution();
        List<int> path = node.getAncestors();

        StartCoroutine(Wait(path));

    }

    public int BestMoves() {
        Node node = AISolution();
        return node.getAncestors().Count;
    }

    public Node AISolution() {

        n = PlayerPrefs.GetInt("Size");
        List<Node> queue = new List<Node>(1);
        List<int> ancestors = new List<int>(0);
        List<int[, ]> visited = new List<int[, ]>(1);
        int[, ] position = gameManager.GetPosition();

        int p1 = gameManager.GetP1();
        int p2 = gameManager.GetP2();

        Node node = new Node(0, position, p1, p2, 0, ancestors);
        queue.Add(node);

        node = Puzzle(queue,node, visited);

        return node;

    }

    IEnumerator Wait(List<int> path) {
        for (int i = 0; i < path.Count; i++) {
            yield return new WaitForSeconds(0.4f);
            FindObjectOfType<MoveSquare>().Move(button[path[i] - 1]);
        }
    }

    public static Node Puzzle(List<Node> queue, Node node, List<int[, ]> visited) {

        while (!IsGoal(node.getPosition())) {
            int[,] pos = new int[n, n];
            pos = Copy(pos, node.getPosition());
            visited.Add(pos);

            List<int> tempAncestors = new List<int>(node.getAncestors().Count);
            tempAncestors = CopyList(tempAncestors, node.getAncestors());

            queue = AddToQueue(queue, node.getPosition(), node.getP1() + 1, node.getP2(), node.getP1(), node.getP2(), node.getPathCost(), tempAncestors, visited);
            queue = AddToQueue(queue, node.getPosition(), node.getP1() - 1, node.getP2(), node.getP1(), node.getP2(), node.getPathCost(), tempAncestors, visited);
            queue = AddToQueue(queue, node.getPosition(), node.getP1(), node.getP2() + 1, node.getP1(), node.getP2(), node.getPathCost(), tempAncestors, visited);
            queue = AddToQueue(queue, node.getPosition(), node.getP1(), node.getP2() - 1, node.getP1(), node.getP2(), node.getPathCost(), tempAncestors, visited);

            bool inVisited = true;
            while (inVisited) {
                node = queue[0];
                queue.RemoveAt(0);
                inVisited = false;
                for (int i = 0; i < visited.Count; i++) {
                    if (node.getPosition() == visited[i]) {
                        inVisited = true;
                    }
                }
            }

            if (node.getAncestors().Count > 28) {
                Debug.Log("fail");
                return node;
            }

        }

        return node;

    }

    public static List<Node> AddToQueue(List<Node>  queue, int[, ] position, int p1, int p2, int o1, int o2, int pathCost, List<int> ancestors, List<int[, ]> visited) {

        if (((p1 >= 0) && (p1 < n)) && ((p2 >= 0) && (p2 < n))) {

            int[, ] pos = new int[n, n];
            pos = Copy(pos, position);
            int temp = pos[p1, p2];
            pos[p1, p2] = 0;
            pos[o1, o2] = temp;

            if (!Contains(visited, pos)) {

                int fValue = 0;
                for (int i = 0; i < n; i++) {
                    for (int j = 0; j < n; j++) {
                        if (pos[i, j] != 0) {
                            fValue += Math.Abs(Convert(pos[i, j])[0] - i) + Math.Abs(Convert(pos[i, j])[1] - j);
                        }
                    }
                }
                fValue += pathCost;

                List<int> tempAncestors = new List<int>(ancestors.Count);
                tempAncestors = CopyList(tempAncestors, ancestors);
                tempAncestors.Add(temp);

                Node node = new Node(fValue, pos, p1, p2, pathCost + 1, tempAncestors);
                int placement = queue.Count - 1;
                for (int i = 0; i < queue.Count; i++)
                {
                    if (fValue <= queue[i].getFValue()) {
                        placement = i;
                        break;
                    }
                }
                queue.Insert(placement, node);

                return queue;
            }
        }

        return queue;

    }

    public static bool Contains(List<int[, ]> a, int[, ] b) {

        for (int i = 0; i < a.Count; i++) {
            bool equal = true;
            for (int j = 0; j < n && equal; j++) {
                for (int k = 0; k < n && equal; k++) {
                    if (a[i][j, k] != b[j, k]) {
                        equal = false;
                    }
                }
            }
            if (equal) {
                return true;
            }
        }
        return false;

    }

    public static bool IsGoal(int[, ] position) {

        if (n == 3) {
            int[,] goal = { { 1, 4, 7 }, { 2, 5, 8 }, { 3, 6, 0 } };

            for (int i = 0; i < n; i++) {
                for (int j = 0; j < n; j++) {
                    if (position[i, j] != goal[i, j]) {
                        return false;
                    }
                }
            }
            return true;
        } else {
            int[,] goal = { { 1, 5, 9, 13 }, { 2, 6, 10, 14 }, { 3, 7, 11, 15 }, { 4, 8, 12, 0 } };

            for (int i = 0; i < n; i++) {
                for (int j = 0; j < n; j++) {
                    if (position[i, j] != goal[i, j]) {
                        return false;
                    }
                }
            }
            return true;
        }

    }

    public static int[] Convert(int i) {

        if (n == 3)
        {

            if (i == 1)
            {
                return Merge(0, 0);
            }
            else if (i == 2)
            {
                return Merge(1, 0);
            }
            else if (i == 3)
            {
                return Merge(2, 0);
            }
            else if (i == 4)
            {
                return Merge(0, 1);
            }
            else if (i == 5)
            {
                return Merge(1, 1);
            }
            else if (i == 6)
            {
                return Merge(2, 1);
            }
            else if (i == 7)
            {
                return Merge(0, 2);
            }
            else if (i == 8)
            {
                return Merge(1, 2);
            }
            else
            {
                return Merge(2, 2);
            }
        } else {

            if (i == 1)
            {
                return Merge(0, 0);
            }
            else if (i == 2)
            {
                return Merge(1, 0);
            }
            else if (i == 3)
            {
                return Merge(2, 0);
            }
            else if (i == 4)
            {
                return Merge(3, 0);
            }
            else if (i == 5)
            {
                return Merge(0, 1);
            }
            else if (i == 6)
            {
                return Merge(1, 1);
            }
            else if (i == 7)
            {
                return Merge(2, 1);
            }
            else if (i == 8)
            {
                return Merge(3, 1);
            }
            else if (i == 9)
            {
                return Merge(0, 2);
            }
            else if (i == 10)
            {
                return Merge(1, 2);
            }
            else if (i == 11)
            {
                return Merge(2, 2);
            }
            else if (i == 12)
            {
                return Merge(3, 2);
            }
            else if (i == 13)
            {
                return Merge(0, 3);
            }
            else if (i == 14)
            {
                return Merge(1, 3);
            }
            else if (i == 15)
            {
                return Merge(2, 3);
            }
            else
            {
                return Merge(3, 3);
            }

        }

    }

    public static int[] Merge(int a, int b) {
        int[] arr = { a, b };
        return arr;
    }

    public static void printArr(int[][] arr) {
        string str = "";
        for (int j = 0; j < n; j++)
        {
            for (int k = 0; k < n; k++)
            {
                str += arr[j][k] + " ";
            }
        }
        Debug.Log(str);
    }

    public static int[, ] Copy(int[, ] newArr, int[, ] arr) {

        for (int i = 0; i < arr.GetLength(0); i++) {
            for (int j = 0; j < arr.GetLength(1); j++) {
                newArr[i, j] = arr[i, j];
            }
        }
        return newArr;

    }

    public static List<int> CopyList(List<int> newList, List<int> list) {

        for (int i = 0; i < list.Count; i++) {
            newList.Add(list[i]);
        }
        return newList;

    }

}
