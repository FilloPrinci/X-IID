using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Range(0.0f, 300.0f)]
    public float maxSpeed;
    [Range(0.0f, 10.0f)]
    public float handling;
    [Range(0.0f, 10.0f)]
    public float accelleration;

    private GameObject gameObject;
    private Rigidbody rigidBody;
    private float inputAccellerator = 0;

    // Start is called before the first frame update
    void Start()
    {
        gameObject = GameObject.Find("Player");
        rigidBody = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        GetInputs();

        rigidBody.AddForce(Vector3.forward * accelleration * inputAccellerator);
    }


    void GetInputs() {
        inputAccellerator = Input.GetAxis("Fire1");
    }


}
