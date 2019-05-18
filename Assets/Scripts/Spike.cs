using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : PlayerObject
{
    public float rotationSpeed;
    public GameObject largeAttackInitialPosition;

    private bool fire3Pressed = false;
    private float initialRotationSpeed;

    private void Start()
    {
        initialRotationSpeed = rotationSpeed;
        rotationSpeed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //GameObject spike = transform.GetChild(0).gameObject;
        //spike.transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);

        if (Input.GetAxis("Fire3") == 0) fire3Pressed = false;
        else if (objectState == 0 && Input.GetAxis("Fire3") != 0 && !fire3Pressed)
        {
            fire3Pressed = true;
            gameObject.GetComponentInChildren<Animator>().SetTrigger("LargeAttack");
            gameObject.transform.parent = null;
            StartCoroutine(InitialMovement());
        }
    }

    public IEnumerator InitialMovement()
    {
        Vector3 destinationPosition = largeAttackInitialPosition.transform.position;
        float angleIncrement = 3;
        float height = 3;
        Vector3 initialPosition = transform.position;
        Vector3 movementVector = destinationPosition - initialPosition;
        Vector3 portionMovementVector = movementVector / (180 / angleIncrement);

        float perpendicularForce = 0.4f;
        Vector2 perpendicularDirection = Vector2.Perpendicular(new Vector2(movementVector.x, movementVector.z));

        float angle = 0;
        int count = 0;
        while (angle < 180)
        {
            yield return null;
            transform.position = initialPosition + (portionMovementVector * count);
            count++;
            
            transform.position = new Vector3(transform.position.x, initialPosition.y + (height * Mathf.Sin(angle * Mathf.Deg2Rad)), transform.position.z);
            transform.position = new Vector3(transform.position.x + (perpendicularForce * perpendicularDirection.x * Mathf.Sin(angle * Mathf.Deg2Rad)), transform.position.y, transform.position.z + (perpendicularForce * perpendicularDirection.y * Mathf.Sin(angle * Mathf.Deg2Rad)));
            angle+=angleIncrement;
        }
        gameObject.GetComponentInChildren<Animator>().SetTrigger("BeginAttack");
    }

    public void StartRotation()
    {
        StartCoroutine(StartRotationCoroutine());
    }

    public IEnumerator StartRotationCoroutine()
    {
        while (rotationSpeed < initialRotationSpeed)
        {
            rotationSpeed += 8;
            yield return null;
        }
        rotationSpeed = initialRotationSpeed;
    }

    public void StopRotation()
    {
        StartCoroutine(StopRotationCoroutine());
    }

    public IEnumerator StopRotationCoroutine()
    {
        while (rotationSpeed > 0)
        {
            rotationSpeed -= 8;
            yield return null;
        }
        rotationSpeed = 0;
    }
}
