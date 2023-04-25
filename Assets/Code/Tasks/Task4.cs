using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task4 : Task
{
    [SerializeField] private GameObject fuelSwitch;
    [SerializeField] private GameObject tempSwitch;
    [SerializeField] private GameObject airSwitch;

    public override bool CheckTaskState()
    {
        if (fuelSwitch.GetComponent<ToggleSwitch>().switchState && tempSwitch.GetComponent<ToggleSwitch>().switchState && airSwitch.GetComponent<ToggleSwitch>().switchState)
        {
            // Additional functionality here

            return true;
        }
        else
        {
            return false;
        }
    }
}