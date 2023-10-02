using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax2 : MonoBehaviour
{

    public Camera cam;
    public Transform subject;

    Vector2 startPosition;
    float startZ;
    public bool background;

    Vector2 travel => (Vector2) cam.transform.position - startPosition;

    float distanceFromSubject => transform.position.z - subject.position.z;
    float clippingPlain => (cam.transform.position.z + (distanceFromSubject > 0 ? cam.farClipPlane : cam.nearClipPlane));
    float parallaxFactor => Mathf.Abs(distanceFromSubject) / clippingPlain;
    
    void Start()
    {
        startPosition = transform.position;
        startZ = transform.position.z;
    }

    
    void LateUpdate()
    {
        Vector2 newPos = (startPosition + travel * parallaxFactor);

        if (background)
        {
            transform.position = new Vector3(subject.position.x, transform.position.y, startZ);
        }
        else
        {
            transform.position = new Vector3(newPos.x, newPos.y, startZ);
        }
    }
}
