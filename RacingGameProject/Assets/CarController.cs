using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [Header("Connections")]
    [SerializeField] internal Rigidbody rb;

    [Header("Number n shit")]
    [SerializeField] private float acceleration;
    [SerializeField] private float reverseAcceleration;
    [SerializeField] private float turnStrength;
    [SerializeField] private float gravityForce;
    [SerializeField] private float dragGround = 3f;
    [SerializeField] private float boostAmount = 18f;
    private float speedInput;
    private float turnInput;

    [Header("Ground Checking")]
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float groundRay = 0.5f;
    [SerializeField] private Transform rayPosition;
    private bool isGrounded;




    // Start is called before the first frame update
    void Start()
    {
        rb.transform.parent = null;
    }

    private void Update()
    {
        //movement inputs
        speedInput = 0;
        if (Input.GetAxis("Vertical") > 0)
        {
            speedInput = Input.GetAxis("Vertical") * acceleration * 1000f;

        }
        else if (Input.GetAxis("Vertical") < 0)
        {
            speedInput = Input.GetAxis("Vertical") * reverseAcceleration * 1000f;
        }


        //rotation
        turnInput = Input.GetAxis("Horizontal");
        turnStrength = rb.velocity.magnitude * 180f;
        Vector3 rotation = new Vector3(0f, turnInput * Mathf.Clamp(turnStrength, 0, 180) * Time.deltaTime * rb.velocity.normalized.magnitude, 0f);
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + rotation);


        //set the position
        transform.position = rb.transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //ground check
        isGrounded = false;
        RaycastHit hit;

        if (Physics.Raycast(rayPosition.position, -transform.up, out hit, groundRay, groundMask))
        {
            isGrounded = true;

            transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
        }

        //applying force and gravity
        if (isGrounded)
        {
            rb.drag = dragGround;
            if (Mathf.Abs(speedInput) > 0)
            {
                rb.AddForce(transform.forward * speedInput);
            }
        }
        else
        {
            rb.drag = 0.01f;
            rb.AddForce(Vector3.up * -gravityForce * 100);
        }

    }

    IEnumerator Boost()
    {
        float cacheAccel = acceleration;

        acceleration = boostAmount;
        float t = boostAmount;

        while (acceleration > cacheAccel)
        {
            acceleration -= Time.deltaTime * 15;
            yield return null;
        }

        acceleration = cacheAccel;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Powerup")
        {
            print("WOW");
            StartCoroutine("Boost");
            Destroy(other.gameObject);
        }
    }
}
