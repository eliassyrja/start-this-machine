using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayController : MonoBehaviour
{
    [SerializeField] private GameObject textMesh;
    [SerializeField] private float duration;
    private float timeElapsed;
    private string password = "";
    private string characters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!#¤%&/()=?";
    // Start is called before the first frame update
    void Start()
    {
        timeElapsed = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeElapsed > duration)
		{
            GeneratePassword();
            textMesh.GetComponent<TextMeshPro>().text = password;
            timeElapsed = 0;
            password = "";
		}
        timeElapsed += Time.deltaTime;
    }

    private void GeneratePassword()
	{
        for (int i = 0; i < 9; i++)
		{
            char c = characters[Random.Range(0, characters.Length)];
            password += c;
		}
    }
}
