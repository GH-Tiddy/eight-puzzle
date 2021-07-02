using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace George {
	public class MyTuple {

		public int i;
		public int j;

		public MyTuple(int i, int j) {
			this.i = i;
			this.j = j;
		}

		public int getI() {
			return i;
		}

		public int getJ() {
			return j;
		}

		public void setI(int i) {
            this.i = i;
        }

		public void setJ(int j) {
            this.j = j;
        }

		public void printTuple() {
            Debug.Log(i + ", " + j);
        }
	}
}