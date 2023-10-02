using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiasmaPatrol : MonoBehaviour
{
    public Rigidbody2D miasma;
    float pingPong;
    void Start()
    {
        miasma.velocity = new Vector3(3.5f, 0, 0);
        // miasma.position.transform.x = new Vector
    }

    // Update is called once per frame
    void Update()
    {


        pingPong = Mathf.PingPong(Time.time/2, 1f);

        if (pingPong > 0.5f)
        {
            miasma.velocity = new Vector3(0, -1.5f, 0);
        }
        else
        {
            miasma.velocity = new Vector3(0, 1.5f, 0);
        }
        
    }
}
