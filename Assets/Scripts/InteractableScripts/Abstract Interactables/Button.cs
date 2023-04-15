using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Button : Interactable
{
    public float buttonTimer = 0.75f;
    private Vector3 orginalPos;
    public bool readyToPress;
    public void Start()
    {
        orginalPos = GetComponent<Transform>().position;
        readyToPress = true;
    }
    public override void Main() {
        if(readyToPress)
            ButtonPress();
    }

    public abstract void ButtonAction();

    public void ButtonPress() {
        ButtonAction();
        readyToPress = false;
        //buttonAction();
        transform.position = Vector3.MoveTowards(transform.position,
            new Vector3(transform.position.x, 0, transform.position.z),
            0.04f);
        Invoke("ButtonReset", buttonTimer);
    }

    public void ButtonReset() {
        readyToPress = true;
        transform.position = Vector3.MoveTowards(transform.position,
            orginalPos,
            0.04f);
    }
}
