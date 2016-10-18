using UnityEngine;
using System.Collections;

public class HandelLabel : MonoBehaviour {

    public GameObject label;

    [SerializeField]
    private string text;

	// Use this for initialization
	void Start () {
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        label.transform.Translate(0, mesh.bounds.size.y, 0);
        label.GetComponent<TextMesh>().text = text;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
