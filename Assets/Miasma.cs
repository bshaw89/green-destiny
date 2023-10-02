using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Miasma : MonoBehaviour
{
    public Rigidbody2D miasma;
    public GameObject player;
    void Start()
    {
        miasma.velocity = new Vector3(3f, 0, 0);
        // miasma.position.transform.x = new Vector
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.x > 160f)
        {
            miasma.velocity = new Vector3(0, 0, 0);
        }
    }
}
