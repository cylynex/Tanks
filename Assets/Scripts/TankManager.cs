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

        print("picking random entry: ");
        print(availableColors[Random.Range(0, availableColors.Count)]);
    }

    public int SelectTankColor() {
        int outInt = availableColors[Random.Range(0, availableColors.Count)];
        availableColors.Remove(outInt);
        return outInt;
    }

}
