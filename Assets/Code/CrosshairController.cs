using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairController : MonoBehaviour
{
    private bool isActive;

	private void Start()
	{
        ShowCrosshair();
	}
	public void ShowCrosshair()
	{
        isActive = true;
        gameObject.SetActive(true);
	}
    public void HideCrosshair()
    {
        isActive = false;
        gameObject.SetActive(false);
    }
}
