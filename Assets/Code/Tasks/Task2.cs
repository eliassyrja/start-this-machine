using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task2 : Task
{
    [SerializeField] private GameObject button1;

    public override bool CheckTaskState()
    {
        return button1.GetComponent<RockerSwitch>().buttonState;
    }
}
