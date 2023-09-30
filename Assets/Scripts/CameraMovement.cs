using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform subject;
    public float smoothSpeed;
    public Vector3 offset;

    void FixedUpdate()
    {
        Vector3 targetPosition = subject.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;

    }
  
}
