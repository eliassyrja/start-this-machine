using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task3 : Task
{
    [SerializeField] private GameObject fuelFlowValve1;
    [SerializeField] private GameObject fuelFlowValve2;
    [SerializeField] private GameObject fuelFlowValve3;
    [SerializeField] private GameObject switchPumpOnButton; //Acts as enter


    public override bool CheckTaskState()
    {
        if (fuelFlowValve1.GetComponent<ToggleSwitch>().switchState && fuelFlowValve2.GetComponent<ToggleSwitch>().switchState && fuelFlowValve3.GetComponent<ToggleSwitch>().switchState)
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
