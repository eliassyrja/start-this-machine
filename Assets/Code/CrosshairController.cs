using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairController : MonoBehaviour
{

	private void Start()
	{
        ShowCrosshair();
	}
	public void ShowCrosshair()
	{
        gameObject.SetActive(true);
	}
    public void HideCrosshair()
    {
        gameObject.SetActive(false);
    }
}
