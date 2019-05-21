using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : PlayerObject
{
    public float rotationSpeed;
    public GameObject basicAttackPosition, largeAttackInitialPosition;

    private bool fire3Pressed = false, fire4Pressed = false;
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
            gameObject.GetComponentInChildren<Animator>().SetTrigger("Attack");
            gameObject.transform.parent = null;
            StartCoroutine(InitialMovement(basicAttackPosition, false));
        }

        if (Input.GetAxis("Fire4") == 0) fire4Pressed = false;
        else if (objectState == 0 && Input.GetAxis("Fire4") != 0 && !fire4Pressed)
        {
            fire4Pressed = true;
            gameObject.GetComponentInChildren<Animator>().SetTrigger("LargeAttack");
            gameObject.transform.parent = null;
            StartCoroutine(LargeAttackCoroutine());
        }
    }

    public IEnumerator LargeAttackCoroutine()
    {
        yield return StartCoroutine(InitialMovement(largeAttackInitialPosition, true));
        gameObject.GetComponentInChildren<Animator>().SetTrigger("BeginLargeAttack");
    }

    public IEnumerator InitialMovement(GameObject position, bool Ymovement)
    {
        Vector3 destinationPosition = position.transform.position;
        float angleIncrement = 4;
        float height = 3;
        Vector3 initialPosition = transform.position;
        Vector3 movementVector = destinationPosition - initialPosition;
        Vector3 portionMovementVector = movementVector / (180 / angleIncrement);

        float perpendicularForce = 0.3f;
        Vector2 perpendicularDirection = Vector2.Perpendicular(new Vector2(movementVector.x, movementVector.z));

        float angle = 0;
        int count = 0;
        while (angle < 180)
        {
            yield return null;
            transform.position = initialPosition + (portionMovementVector * count);
            count++;
            
            if (Ymovement) transform.position = new Vector3(transform.position.x, initialPosition.y + (height * Mathf.Sin(angle * Mathf.Deg2Rad)), transform.position.z);
            transform.position = new Vector3(transform.position.x + (perpendicularForce * perpendicularDirection.x * Mathf.Sin(angle * Mathf.Deg2Rad)), transform.position.y, transform.position.z + (perpendicularForce * perpendicularDirection.y * Mathf.Sin(angle * Mathf.Deg2Rad)));
            angle+=angleIncrement;
        }
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
