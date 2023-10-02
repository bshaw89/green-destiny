using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform subject;
    public float smoothSpeed;
    public Vector3 offset;

    void LateUpdate()
    {
        // Vector3 targetPosition = subject.position + offset;
        // Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);
        // transform.position = smoothedPosition;
        transform.position = new Vector3 (subject.transform.position.x + offset.x, transform.position.y, offset.z);


    }
  
}
