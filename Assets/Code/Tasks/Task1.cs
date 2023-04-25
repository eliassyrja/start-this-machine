using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task1 : Task
{
    [SerializeField] private GameObject powerSwitch;

    public override bool CheckTaskState()
    {
        if (powerSwitch.GetComponent<ToggleSwitch>().switchState)
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
