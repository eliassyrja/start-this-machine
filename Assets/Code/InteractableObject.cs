using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class InteractableObject : MonoBehaviour
{
    [SerializeField] private KeyCode interactionKey = KeyCode.Mouse0;

    public void OnMouseOver()
    {
        if (Input.GetKeyDown(interactionKey))
        {
            print("Interaction attempted");
            Interact();
        }
    }
    public void Interact()
    {
        this.gameObject.GetComponent<Renderer>().material.color = new Color(0, 255, 0);
        Debug.LogFormat("{0} has been interacted with!", this);
    }
}
