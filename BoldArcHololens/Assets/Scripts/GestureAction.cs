using UnityEngine;

/// <summary>
/// GestureAction performs custom actions based on
/// which gesture is being performed.
/// </summary>
public class GestureAction : MonoBehaviour
{
    [Tooltip("Rotation max speed controls amount of rotation.")]
    public float RotationSensitivity = 10.0f;

    private Vector3 manipulationPreviousPosition;

    private float rotationFactor;

    public float m_xFactor;
    public float m_yFactor;
    public float m_zFactor;

    public float m_yMin;
    public float m_yMax;
    public bool m_bControlY;

    void Start()
    {
    }

    void Update()
    {
        //PerformRotation();
    }

    private void PerformRotation()
    {
        if (GestureManager.Instance.IsNavigating &&
            /*(!ExpandModel.Instance.IsModelExpanded ||
            (ExpandModel.Instance.IsModelExpanded &&*/ HandsManager.Instance.FocusedGameObject == gameObject)//))
        {
            /* TODO: DEVELOPER CODING EXERCISE 2.c */

            // 2.c: Calculate rotationFactor based on GestureManager's NavigationPosition.X and multiply by RotationSensitivity.
            // This will help control the amount of rotation.
            rotationFactor = GestureManager.Instance.NavigationPosition.x * RotationSensitivity;

            // 2.c: transform.Rotate along the Y axis using rotationFactor.
            transform.Rotate(new Vector3(0, -1 * rotationFactor, 0));
        }
    }

    void PerformManipulationStart(Vector3 position)
    {
        manipulationPreviousPosition = position;
    }

    void PerformManipulationUpdate(Vector3 position)
    {
        if (GestureManager.Instance.IsManipulating)
        {
            /* TODO: DEVELOPER CODING EXERCISE 4.a */

            Vector3 moveVector = Vector3.zero;

            // 4.a: Calculate the moveVector as position - manipulationPreviousPosition.
            moveVector = position - manipulationPreviousPosition;

            // 4.a: Update the manipulationPreviousPosition with the current position.
            manipulationPreviousPosition = position;

            // 4.a: Increment this transform's position by the moveVector.
            moveVector.x *= m_xFactor;
            moveVector.y *= m_yFactor;
            moveVector.z *= m_zFactor;
            transform.localPosition += moveVector;

            if (m_bControlY)
            {
                Vector3 v;
                v = transform.localPosition;
                if (v.y < m_yMin)
                    v.y = m_yMin;
                if (v.y > m_yMax)
                    v.y = m_yMax;
                transform.localPosition = v;
            }
        }
    }
}
