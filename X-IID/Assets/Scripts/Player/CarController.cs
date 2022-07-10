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

    private float inputAccellerator = 0;
    private float inputTourn = 0;
    private float speed = 0;
    private Vector3 previousPosition;
    private float currentAccelleration = 0;

    // Start is called before the first frame update
    void Start()
    {
        previousPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        getInputs();
        currentAccelleration = Mathf.Lerp(speed, maxSpeeed, Time.deltaTime * accelleration);

        transform.position += transform.forward * inputAccellerator * currentAccelleration * Time.deltaTime;
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0f, inputTourn * turnStrenght * 100 * Time.deltaTime, 0f));

        speed = (transform.position - previousPosition).magnitude / Time.deltaTime;
        previousPosition = transform.position;

    }

    void getInputs() {
        inputAccellerator = Input.GetAxis("Fire1");
        inputTourn = Input.GetAxis("Horizontal");
    }
}
