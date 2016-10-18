using UnityEngine;
using System.Collections;

public class PlaceLabelLine : MonoBehaviour {

    [SerializeField]
    private GameObject target;

    // Use this for initialization
    void Start () {
        LineRenderer line = GetComponent<LineRenderer>();
        line.SetPosition(0, this.transform.position);
        line.SetPosition(1, target.transform.position);

    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
