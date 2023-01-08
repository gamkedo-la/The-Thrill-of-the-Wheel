using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICarController : MonoBehaviour
{
    [SerializeField]
    private float speed = 25f, rotationSpeed = 180f;
    
    [SerializeField]
    private bool playerCar = false;

    private Rigidbody myBody;

    private float translation, rotation;

    private GameObject target;

    private void Awake()
    {
        myBody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        target = GameObject.FindWithTag("Player");    
    }

    private void Update()
    {
        if (!playerCar && !target)
            return;

        if (playerCar)
        {
            if (Mathf.Abs(Input.GetAxis("Vertical")) > 0)
            {
                rotation = Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;
                translation = Input.GetAxis("Vertical") * speed;
            }
            else
            {
                rotation = 0f;
                translation = 0f;
            }
        }
        else
        {
            Vector3 targetDirection = transform.position - target.transform.position;
            targetDirection.Normalize();

            rotation = Vector3.Cross(targetDirection, transform.forward).y;
        }
    }

    private void FixedUpdate()
    {
        if (playerCar)
        {
            myBody.velocity = transform.forward * translation;
            transform.Rotate(Vector3.up * rotation);
        }
        else
        {
            myBody.angularVelocity = rotationSpeed * rotation * new Vector3(0, 1, 0);
            myBody.velocity = transform.forward * speed;
        }
    }

}



























