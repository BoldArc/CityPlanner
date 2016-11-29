using UnityEngine;
using System.Collections;

public class SpeechAction : MonoBehaviour
{

    [SerializeField]
    private Transform explosionTransform;

    private Vector3 startPosition;

    private bool m_bExplode;
    public bool m_bLabel;
    public bool m_bWorld;
    public bool m_bInfo;
    public bool m_bPlan;
    public bool m_bWall;
    public bool m_bModel;
    public bool m_bExtraWall;

    // Use this for initialization
    void Start()
    {
        startPosition = this.transform.localPosition;
        m_bExplode = false;
        // Hide all "Label" objects from start.
        if (m_bLabel)
        {
            Renderer[] rendererComponents = GetComponentsInChildren<Renderer>();
            foreach (Renderer component in rendererComponents)
            {
                component.enabled = false;
            }
        }
        if (m_bInfo)
        {
            // Hide the building info.
            Renderer[] rendererComponents = GetComponentsInChildren<Renderer>();
            foreach (Renderer component in rendererComponents)
            {
                component.enabled = false;
            }
        }

        if (m_bPlan || m_bWall || m_bExtraWall)
        {
            // Hide the plan and the walls from start.
            Renderer planRenderer = GetComponent<Renderer>();
            planRenderer.enabled = false;
        }

        if (m_bModel)
        {
            Renderer[] rendererComponents = GetComponentsInChildren<Renderer>();
            foreach (Renderer component in rendererComponents)
            {
                component.enabled = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (m_bExplode && explosionTransform)
        {
            float speed = 0.5f;
            float step = speed * Time.deltaTime;
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, explosionTransform.localPosition, step);

            if (transform.localPosition == explosionTransform.localPosition)
                m_bExplode = !m_bExplode;
        }
    }

    // Called by SpeechManager when the user says the "Reset world" command
    void OnReset()
    {
        // Puts the object back into its original local position.
        this.transform.localPosition = startPosition;
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

        m_bExplode = !m_bExplode;
        GameObject nameText = transform.FindChild("Mesh Part Name").gameObject;
        if (nameText != null)
            nameText.SetActive(true);
    }

    // Called by SpeechManager when the user says the "Show Label" command
    void ShowLabel()
    {
        if (m_bLabel)
        {
            // Shows/hides the building labels.
            Renderer[] rendererComponents = GetComponentsInChildren<Renderer>();
            foreach (Renderer component in rendererComponents)
            {
                component.enabled = !component.enabled;
            }
        }
    }

    void PlaceWorld()
    {
        if (m_bWorld)
        {
            // First set the world offset so that the scene object is located one meter in front of
            // the user and 30 centimeter bellow.
            Vector3 cameraOffset = new Vector3(0.0f, -0.3f, 1.0f);
            // Get head rotation as a Quaternion and then remove the pitch and roll rotations.
            Quaternion headRotation = Camera.current.transform.rotation;
            Vector3 headEulerAngles = Vector3.zero;
            headEulerAngles.y = headRotation.eulerAngles.y;
            headRotation = Quaternion.Euler(headEulerAngles.x, headEulerAngles.y, headEulerAngles.z);
            // Add the head position + the camera offset with the head rotation so that
            // you get the scene object infront of you. 
            cameraOffset = headRotation * cameraOffset;
            transform.position = Camera.current.transform.position + cameraOffset;
        }
    }

    void ShowInfo()
    {
        if (m_bInfo)
        {
            // Shows/hides the building info.
            Renderer[] rendererComponents = GetComponentsInChildren<Renderer>();
            foreach (Renderer component in rendererComponents)
            {
                component.enabled = !component.enabled;
            }
        }
    }

    void PlacePlan()
    {
        if (m_bPlan || m_bWall)
        {
            // First set the plane offset so that it is located 1.8 meter bellow the users head.
            Vector3 cameraOffset = new Vector3(0.0f, -1.8f, 0.0f);
            // Get head rotation as a Quaternion and then remove the pitch and roll rotations.
            Quaternion headRotation = Camera.current.transform.rotation;
            Vector3 headEulerAngles = Vector3.zero;
            headEulerAngles.y = headRotation.eulerAngles.y;
            headRotation = Quaternion.Euler(headEulerAngles.x, headEulerAngles.y, headEulerAngles.z);
            // Add the head position + the camera offset with the head rotation so that
            // you get the scene object infront of you. 
            cameraOffset = headRotation * cameraOffset;
            transform.position = Camera.current.transform.position + cameraOffset + (headRotation * startPosition);
            transform.rotation = headRotation;
        }
    }

    void ShowPlan()
    {
        if (m_bPlan)
        {
            Renderer renderer = GetComponent<Renderer>();
            renderer.enabled = true;
        }
    }

    void HidePlan()
    {
        if (m_bPlan)
        {
            Renderer renderer = GetComponent<Renderer>();
            renderer.enabled = false;
        }
    }

    void PlaceModel()
    {
        if (m_bModel)
        {
            // First set the offset so that the scene object is located one meter in front of
            // the user and 30 centimeter bellow.
            Vector3 cameraOffset = new Vector3(0.0f, -0.3f, 1.0f);
            // Get head rotation as a Quaternion and then remove the pitch and roll rotations.
            Quaternion headRotation = Camera.current.transform.rotation;
            Vector3 headEulerAngles = Vector3.zero;
            headEulerAngles.y = headRotation.eulerAngles.y;
            headRotation = Quaternion.Euler(headEulerAngles.x, headEulerAngles.y, headEulerAngles.z);
            // Add the head position + the camera offset with the head rotation so that
            // you get the scene object infront of you. 
            cameraOffset = headRotation * cameraOffset;
            transform.position = Camera.current.transform.position + cameraOffset;
        }
    }

    void ShowModel()
    {
        if (m_bModel)
        {
            Renderer[] rendererComponents = GetComponentsInChildren<Renderer>();
            foreach (Renderer component in rendererComponents)
            {
                component.enabled = true;
            }
        }
    }

    void HideModel()
    {
        if (m_bModel)
        {
            Renderer[] rendererComponents = GetComponentsInChildren<Renderer>();
            foreach (Renderer component in rendererComponents)
            {
                component.enabled = false;
            }
        }
    }

    void ShowWall()
    {
        if (m_bWall)
        {
            Renderer renderer = GetComponent<Renderer>();
            renderer.enabled = true;
        }
    }

    void HideWall()
    {
        if (m_bWall)
        {
            Renderer renderer = GetComponent<Renderer>();
            renderer.enabled = false;
        }
    }

    void AddWall()
    {
        if (m_bExtraWall)
        {
            // Hide the plan and the walls from start.
            Renderer planRenderer = GetComponent<Renderer>();
            planRenderer.enabled = true;

            // First set the plane offset so that it is located 1.8 meter bellow the users head.
            Vector3 cameraOffset = new Vector3(0.0f, -1.7f, 2.0f);
            // Get head rotation as a Quaternion and then remove the pitch and roll rotations.
            Quaternion headRotation = Camera.current.transform.rotation;
            Vector3 headEulerAngles = Vector3.zero;
            headEulerAngles.y = headRotation.eulerAngles.y;
            headRotation = Quaternion.Euler(headEulerAngles.x, headEulerAngles.y, headEulerAngles.z);
            // Add the head position + the camera offset with the head rotation so that
            // you get the scene object infront of you. 
            cameraOffset = headRotation * cameraOffset;
            transform.position = Camera.current.transform.position + cameraOffset;
            transform.rotation = headRotation;
        }
    }
}