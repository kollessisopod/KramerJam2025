using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Common : MonoBehaviour
{
    public void FixPhysics(Rigidbody2D rb)
    {
        rb.rotation = 0;
        rb.angularVelocity = 0;
        rb.velocity = new Vector3(0, 0, 0);
    }

}
