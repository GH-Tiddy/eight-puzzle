using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace George {
    public class Node {

        public int fValue;
        public int[,] position;
        public int p1;
        public int p2;
        public int pathCost;
        public List<int> ancestors;

        public Node(int fValue, int[,] position, int p1, int p2, int pathCost, List<int> ancestors) {
            this.fValue = fValue;
            this.position = position;
            this.p1 = p1;
            this.p2 = p2;
            this.pathCost = pathCost;
            this.ancestors = ancestors;
        }

        public int getFValue() {
            return fValue;
        }

        public int[,] getPosition() {
            return position;
        }

        public int getP1() {
            return p1;
        }

        public int getP2() {
            return p2;
        }

        public int getPathCost() {
            return pathCost;
        }

        public List<int> getAncestors() {
            return ancestors;
        }

        public void printNode() {
            string str = "";
            str += "fValue: " + fValue + "; ";
            for (int i = 0; i < 3; i++) {
                for (int j = 0; j < 3; j++) {
                    str += position[i, j] + " ";
                }
            }
            str += "Position: " + str + "; ";
            str += "p1, p2: " + p1 + " " + p2 + "; ";
            str += pathCost + "; ";
            for (int i = 0; i < ancestors.Count; i++) {
                str += ancestors[i];
            }
            Debug.Log(str);
        }

    }
}