using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankControllerP1 : MonoBehaviour {

    
    [SerializeField] GameObject[] wheels;
    [SerializeField] GameObject turret;
    bool moving = false;
    float turnSpeed = 40f;
    float turretSpeed = 13f;
    bool moveDirection;

    private void Update() {
        GetInput();
        TurnWheels(); // TODO replace with actual physics
    }

    void GetInput() {
        if (Input.GetAxis("P1_Horizontal") > 0.3f) {
            print("p1 horizontal RIGHT");
            Vector3 turn = new Vector3(0f, 1f, 0f) * Time.deltaTime * turnSpeed;
            transform.Rotate(turn);
        } else if (Input.GetAxis("P1_Horizontal") < -0.3f) {
            print("p1 horizontal LEFT");
            Vector3 turn = -new Vector3(0f, 1f, 0f) * Time.deltaTime * turnSpeed;
            transform.Rotate(turn);
        } else {
            print("neutral");
        }

        if (Input.GetAxis("P1_Vertical") > 0.3f) {
            print("UP/DOWN");
            moving = true;
            moveDirection = true;
            MoveForward();
        } else if (Input.GetAxis("P1_Vertical") < -0.3f) {
            moving = true;
            MoveBackward();
            moveDirection = false;
        } else {
            moving = false;
        }


        if (Input.GetAxis("Fire1") > 0.1f) {
            print("Fire1 Up");
            //moving = true;
            //MoveForward();
        } else {
            //moving = false;
        }

        if (Input.GetAxis("P1_TurretHorizontal") > 0.1f) {
            Vector3 turretRotation = new Vector3(0, 5, 0);
            turret.transform.Rotate(turretRotation * Time.deltaTime * turretSpeed);
        } else if (Input.GetAxis("P1_TurretHorizontal") < -0.1f) {
            Vector3 turretRotation = new Vector3(0, -5, 0);
            turret.transform.Rotate(turretRotation * Time.deltaTime * turretSpeed);
        }


    }


    void MoveForward() {
        transform.Translate(Vector3.forward * Time.deltaTime);        
    }

    void MoveBackward() {
        transform.Translate(-Vector3.forward * Time.deltaTime);
    }

    void TurnWheels() {
        if (moving) {
            print("spin wheels");
            for (int i = 0; i < wheels.Length; i++) {
                if (moveDirection) {
                    wheels[i].transform.Rotate(new Vector3(10, 0, 0) * 10 * Time.deltaTime);
                } else {
                    wheels[i].transform.Rotate(-new Vector3(10, 0, 0) * 10 * Time.deltaTime);
                }
            }
        }
    }
}
