using UnityEngine;
using System.Collections;
using System.Diagnostics;

public class MoveObject : MonoBehaviour {

    Vector3 originalPosition;
    Vector3 prevDirection;
    Vector3 prevRotation;
    bool isSelected;

    // Use this for initialization
    void Start () {
        originalPosition = this.transform.localPosition;
        prevRotation = this.transform.localPosition;
        isSelected = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (isSelected)
        {
            transform.Rotate(0, 0, 1);
        }
        else
            return;

        if(prevDirection.x != Camera.main.transform.forward.x)
        {
            float deltaX = prevDirection.x - Camera.main.transform.forward.x;
            System.Diagnostics.Debug.WriteLine("Delta value is " + deltaX);

            transform.Translate(0, deltaX, 0);
        }

        if(prevRotation.x != Camera.main.transform.rotation.eulerAngles.x)
        {
            float deltaX = prevRotation.x - Camera.main.transform.rotation.eulerAngles.x;
            transform.Translate(0, deltaX, 0);
        }

        prevRotation = Camera.main.transform.rotation.eulerAngles;
        prevDirection = Camera.main.transform.forward;
    }

    void OnSelect()
    {
        isSelected = !isSelected;
        System.Diagnostics.Debug.WriteLine("Selected " + isSelected);
    }

    // Called by SpeechManager when the user says the "Reset world" command
    void OnReset()
    {
        // Put the sphere back into its original local position.
        this.transform.localPosition = originalPosition;
    }
}
