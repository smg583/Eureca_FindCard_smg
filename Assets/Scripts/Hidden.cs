using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hidden : MonoBehaviour
{
    ButtonAction btnAction;

    private void Awake()
    {
        btnAction = GetComponent<ButtonAction>();
    }

    private void OnMouseDown()
    {
        btnAction.StartMainScene(-1);
    }
}
