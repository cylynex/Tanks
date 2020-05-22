using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {

    float maxLifetime = 2f;
    [SerializeField] float damage = 10f;

    private void Start() {
        Destroy(gameObject, maxLifetime); // Kill if it lives more than maxLifetime
    }

    private void OnCollisionEnter(Collision collision) {
        print("Hit something: " + collision.gameObject.tag);
        switch(collision.gameObject.tag) {
            case "Fallable":
                collision.gameObject.GetComponent<FallDown>().FallOver();
                break;

        }

    }

}
