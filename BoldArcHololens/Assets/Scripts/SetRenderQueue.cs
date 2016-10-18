using UnityEngine;
using System.Collections;

public class SetRenderQueue : MonoBehaviour {
    [Tooltip("Background=1000, Geometry=2000, AlphaTest=2450, Transparent=3000, Overlay=4000")]
    public int queue = 1;

    public int[] queues;

    // Use this for initialization
    void Start () {
        Renderer renderer = GetComponent<Renderer>();
        if (!renderer || !renderer.sharedMaterial || queues == null)
            return;
        renderer.sharedMaterial.renderQueue = queue;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
