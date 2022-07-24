using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [Range(0f, 10f)]
    public float accelleration;
    [Range(0f, 10f)]
    public float maxSpeeed;
    [Range(0f, 10f)]
    public float turnStrenght;

    public Transform frontCollisionCheck;
    public Rigidbody rb;
    public float groundOffset;


    private float inputAccellerator = 0;
    private float inputBrake = 0;
    private float inputTourn = 0;
    private float speed = 0;
    private Vector3 previousPosition;
    private float currentAccelleration = 0;
    private Vector3 accellerationVector;
    private Vector3 gravityVector;
    private Vector3 groundRotation;

    // Start is called before the first frame update
    void Start()
    {
        speed = 0;
        previousPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        stickToGound();
        getInputs();
        accellerate();
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0f, inputTourn * turnStrenght * 100 * Time.deltaTime, 0f));
        transform.position += gravityVector + accellerationVector;
        
    }

    void accellerate() {
        currentAccelleration = Mathf.Lerp(speed, (maxSpeeed * inputAccellerator * 10), Time.deltaTime * accelleration);
        accellerationVector = transform.forward * currentAccelleration * Time.deltaTime;
        Debug.Log("speed: " + speed + " | accelleration: " + currentAccelleration + " | InputAccelleration: " + inputAccellerator);
        speed = transform.InverseTransformDirection(transform.position - previousPosition).z /Time.deltaTime;
        previousPosition = transform.position;
    }

    void stickToGound() {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.up, out hit, Mathf.Infinity))
        {
            Debug.DrawRay(transform.position, -transform.up * hit.distance, Color.yellow);
            gravityVector = -transform.up * (hit.distance - groundOffset);
            //gravityVector = Vector3.LerpUnclamped(gravityVector, -transform.up * (hit.distance - groundOffset), Time.deltaTime);
            
            groundRotation = Quaternion.FromToRotation(hit.normal, Vector3.up).eulerAngles;
        }
        else {
            gravityVector = Vector3.zero;
        }
    }

    void physicsAccellerate() {
        currentAccelleration = Mathf.Lerp(speed, (maxSpeeed * inputAccellerator /2), accelleration);

        rb.AddForce(transform.forward * currentAccelleration, ForceMode.VelocityChange);
        Debug.Log("force vector: " + (transform.forward * currentAccelleration) + "speed: " + speed );

        speed = rb.velocity.z;
        previousPosition = transform.position;
    }

    void physicsRotate() {
        rb.AddTorque(transform.up * inputTourn * turnStrenght * 100, ForceMode.Force);
    }

    void getInputs() {
        inputAccellerator = Input.GetAxis("Accellerator");
        inputBrake = Input.GetAxis("Brake");
        inputTourn = Input.GetAxis("Horizontal");
    }

    void checkFastCollision() { 
        
    } 
}
