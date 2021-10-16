using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat2Control : MonoBehaviour
{
    //Vector3 eulerAngleVelocity;
    private Rigidbody RB;
    public float speed;
    //GameObject saftyRing;

    // Start is called before the first frame update
    void Start()
    {
        //RB = gameObject.GetComponent<Rigidbody>();
        Invoke("FixedUpdate", 3f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //transform.Translate(new Vector3(0.0f, 0.0f, 1.0f) * Time.deltaTime);

        Vector3 forward = transform.forward;
        var directionL = transform.forward * Time.deltaTime * speed;
        transform.Translate(directionL, Space.World);
    }
}
