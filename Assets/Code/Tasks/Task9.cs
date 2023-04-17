using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task9 : Task
{
    [SerializeField] private bool pinCodeCorrect;
    [SerializeField] private GameObject safetyLid1;
    [SerializeField] private GameObject safetyLid2;
    [SerializeField] private float openingSpeed = 0.5f;

    private void Update()
	{
        CheckTaskState();
	}

	public override bool CheckTaskState()
    {
        if (pinCodeCorrect)
        {
            StartCoroutine(ChangeRotationSmoothly());
            pinCodeCorrect = false;
            return true;
        }
        else
        {
            return false;
        }
    }

    IEnumerator ChangeRotationSmoothly()
    {

        float startTime = Time.time;
        while (Time.time < startTime + openingSpeed)
        {
            safetyLid1.transform.localRotation = Quaternion.Lerp(Quaternion.Euler(0f, 0f, 0f), Quaternion.Euler(-75f, 0f, 0f), (Time.time - startTime) / openingSpeed);
            safetyLid2.transform.localRotation = Quaternion.Lerp(Quaternion.Euler(0f, 0f, 0f), Quaternion.Euler(-75f, 0f, 0f), (Time.time - startTime) / openingSpeed);
            yield return null;
        }
        safetyLid1.transform.localRotation = Quaternion.Euler(-75f, 0f, 0f);
        safetyLid2.transform.localRotation = Quaternion.Euler(-75f, 0f, 0f);
    }
}
