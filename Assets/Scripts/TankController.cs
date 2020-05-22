using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TankController : MonoBehaviour {

    [Header("Turret")]
    [SerializeField] GameObject turret;
    [SerializeField] GameObject cannon;
    [SerializeField] float turretSpeed;
    Vector2 turretMovement;

    [Header("Wheels")]
    [SerializeField] GameObject[] wheels;

    [Header("Tank Movement")]
    Vector2 movement;
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] float turnSpeed = 1f;
    bool moveDirection;
    bool moving;

    [Header("Colors")]
    int tankColorID;
    [SerializeField] Material[] tankMaterials;

    [Header("Fire Control")]
    [SerializeField] Transform primaryFirePoint;
    [SerializeField] GameObject primaryWeapon;
    [SerializeField] float primaryFireForce = 15f;
    [SerializeField] float fireRate = 5f;
    [SerializeField] float fireTimer;

    // Singletons
    GameObject gameController;
    TankManager tankManager;

    private void Awake() {
        gameController = GameObject.FindGameObjectWithTag("GameController");
        tankManager = gameController.GetComponent<TankManager>();

        tankColorID = tankManager.SelectTankColor();
        print("this tank will use color code: " + tankColorID);
        GetComponent<MeshRenderer>().material = tankMaterials[tankColorID];
        turret.GetComponent<MeshRenderer>().material = tankMaterials[tankColorID];
        cannon.GetComponent<MeshRenderer>().material = tankMaterials[tankColorID];
    }

    private void FixedUpdate() {
        Move();
        TurnWheels();
        AimTurret();
        FireTimeCalc();
    }

    /**** MAIN MESSAGES ****/

    private void OnMove(InputValue value) {
        movement = value.Get<Vector2>();
    }

    void OnMoveTurret(InputValue value) {
        turretMovement = value.Get<Vector2>();;
    }

    void OnFireMain() {
        FireControl();
    }


    /**** CONTROL METHODS ****/

    void FireTimeCalc() {
        if (fireTimer > 0) {
            fireTimer -= Time.deltaTime;
        } else if (fireTimer < 0) {
            fireTimer = 0;
        }
    }

    void Move() {
        //Vector3 fullMovement = new Vector3(movement.x, 0, 0) * moveSpeed * Time.deltaTime;
        //transform.Translate(fullMovement);

        // Forward and Back
        if (movement.y > 0.3f) {
            MoveForward();
        } else if (movement.y < -0.3f) {
            MoveBackward();
        } else {
            StopMoving();
        }

        // Turning
        if (movement.x < -0.1f) {
            TurnLeft();
        } else if (movement.x > 0.1f) {
            TurnRight();
        } else {
            StopTurning();
        }
    }

    void TurnLeft() {
        Vector3 turn = new Vector3(0, movement.x, 0) * Time.deltaTime * turnSpeed;
        transform.Rotate(turn);
        TurnWheel(wheels[0], -5);
        TurnWheel(wheels[2], -5);
        TurnWheel(wheels[1], 5);
        TurnWheel(wheels[3], 5);
    }

    void TurnRight() {
        Vector3 turn = new Vector3(0, movement.x, 0) * Time.deltaTime * turnSpeed;
        transform.Rotate(turn);
        TurnWheel(wheels[0], 5);
        TurnWheel(wheels[2], 5);
        TurnWheel(wheels[1], -5);
        TurnWheel(wheels[3], -5);
    }
    
    void StopMoving() {
        moving = false;
    }

    void StopTurning() {

    }

    // TODO transform shift on x axis to make it "jump" a little when starting out.
    void MoveForward() {
        transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed);
        moveDirection = true;
        moving = true;
        TurnWheels();
    }

    void MoveBackward() {
        transform.Translate(-Vector3.forward * Time.deltaTime * moveSpeed);
        moveDirection = false;
        moving = true;
        TurnWheels();
    }

    void TurnWheels() {
        if (moving) {
            for (int i = 0; i < wheels.Length; i++) {
                if (moveDirection) {
                    TurnWheel(wheels[i], 5);
                } else {
                    TurnWheel(wheels[i], -5);
                }
            }
        }
    }

    void TurnWheel(GameObject wheel, float amount) {
        wheel.transform.Rotate(new Vector3(amount, 0, 0) * 9 * Time.deltaTime);
    }

    void AimTurret() {
        if (turretMovement.x < -0.1f) {
            Vector3 turretMotion = new Vector3(0, turretMovement.x, 0) * turretSpeed * Time.deltaTime;
            turret.transform.Rotate(turretMotion);
        } else if (turretMovement.x > 0.1f) {
            Vector3 turretMotion = new Vector3(0, turretMovement.x, 0) * turretSpeed * Time.deltaTime;
            turret.transform.Rotate(turretMotion);
        }

        /*
        if (turretMovement.y > 0.3f) {
            //print("AIM DOWN SON");
        } else if (turretMovement.y < -0.3f) {
            Vector3 turretHeight = new Vector3(turretMovement.y, 0, 0) * turretSpeed * Time.deltaTime;
            print("Turret X At: " + turret.transform.rotation.x);
            if (turret.transform.rotation.x <= -0.2f) {
                // At max height do nothing
                print("at max yo");
            } else {
                turret.transform.Rotate(turretHeight);
            }
        }
        */
    }

    void FireControl() {
        if (fireTimer == 0) {
            print("SHOOT BITCH DEMOCRACY IS AT STAKE!!!");
            GameObject rocket = Instantiate(primaryWeapon, primaryFirePoint.position, primaryFirePoint.rotation);
            rocket.GetComponent<Rigidbody>().velocity = primaryFireForce * primaryFirePoint.forward;
            fireTimer = fireRate;

            // Kickback
            //GetComponent<Rigidbody>().AddForce(new Vector3(-6000, 0, 0), ForceMode.Acceleration);
            
        }
    }

}
