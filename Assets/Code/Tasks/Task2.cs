using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task2 : Task
{
    [SerializeField] private GameObject button1;
    [SerializeField] private GameObject button2;
    [SerializeField] private GameObject button3;

    public override bool CheckTaskState()
    {
        return button1.GetComponent<Button>().buttonState && button2.GetComponent<Button>().buttonState && button3.GetComponent<Button>().buttonState;
    }
}
