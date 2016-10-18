using UnityEngine;
using System.Collections;

public class SpeechAction : MonoBehaviour {

    [SerializeField]
    private Transform explosionTransform;

    private Vector3 resetPosition;

    private bool explode;

    // Use this for initialization
    void Start () {
        resetPosition = this.transform.localPosition;
        explode = false;
    }
	
	// Update is called once per frame
	void Update () {
	    if(explode && explosionTransform)
        {
            float speed = 0.5f;
            float step = speed * Time.deltaTime;
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, explosionTransform.localPosition, step);

            if (transform.localPosition == explosionTransform.localPosition)
                explode = !explode;
        }
	}

    // Called by SpeechManager when the user says the "Reset world" command
    void OnReset()
    {
        // Put the sphere back into its original local position.
        this.transform.localPosition = resetPosition;
        GameObject nameText = transform.FindChild("Mesh Part Name").gameObject;
        if (nameText != null)
            nameText.SetActive(false);
    }

    // Called by SpeechManager when the user says the "Explode" command
    void Explode()
    {
        /*Transform explosionTransform = transform.FindChild("ExplosionPosition");
        if (!explosionTransform)
            return;

        if (this.transform.localPosition != explosionTransform.localPosition)
        {
            //this.transform.localPosition = explosionTransform.localPosition;
        }*/

        explode = !explode;
        GameObject nameText = transform.FindChild("Mesh Part Name").gameObject;
        if(nameText != null)
            nameText.SetActive(true);
    }
}