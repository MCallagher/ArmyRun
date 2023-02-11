using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMath : MonoBehaviour {
    public static int Pow(int a, int b) {
        if(a == 0) {
            return a;
        }
        List<int> ops = new List<int>();
        while(b > 0) {
            if(b % 2 == 1) {
                ops.Add(1);
                b--;
            }
            if(b > 0) {
                ops.Add(2);
                b /=2;
            }
        }
        ops.Reverse();
        int p = 1;
        foreach(int op in ops) {
            if(op == 1) {
                p *= a;
            }
            else {
                p *= p;
            }
        }
        return p;
    }
}
