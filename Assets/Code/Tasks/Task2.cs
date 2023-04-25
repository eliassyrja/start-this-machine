using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task2 : Task
{
    [SerializeField] private GameObject rockerSwitch1;
    [SerializeField] private GameObject rockerSwitch2;
    [SerializeField] private GameObject rockerSwitch3;
    [SerializeField] private GameObject rockerSwitch4;
    [SerializeField] private GameObject screen1;
    [SerializeField] private GameObject screen2;
    [SerializeField] private GameObject screen3;
    [SerializeField] private GameObject screen4;

    public override bool CheckTaskState()
    {
        if (rockerSwitch1.GetComponent<RockerSwitch>().buttonState && rockerSwitch2.GetComponent<RockerSwitch>().buttonState &&
            rockerSwitch3.GetComponent<RockerSwitch>().buttonState && rockerSwitch4.GetComponent<RockerSwitch>().buttonState)
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
