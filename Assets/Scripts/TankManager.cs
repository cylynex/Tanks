using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankManager : MonoBehaviour {

    List<int> availableColors = new List<int>();

    private void Start() {
        availableColors.Add(0);
        availableColors.Add(1);
        availableColors.Add(2);
        availableColors.Add(3);
    }

    public int SelectTankColor() {
        print("total: " + availableColors.Count);
        int outInt = availableColors[Random.Range(0, availableColors.Count - 1)];
        availableColors.Remove(outInt);
        return outInt;
    }

}
