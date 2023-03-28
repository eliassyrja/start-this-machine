using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public int maxFPS;
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = maxFPS;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
