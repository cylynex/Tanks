using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour {

    private void Update() {
        GetInput();
    }

    void GetInput() {
        if (Input.GetAxis("P1_Horizontal") > 0 ) { 
            print("p1 horizontal RIGHT");
        } else if (Input.GetAxis("P1_Horizontal") < 0) {
            print("p1 horizontal LEFT");
        }
    }
}