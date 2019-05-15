using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerActions
{
    Cylinder_move1
}

public class Player : MonoBehaviour
{
    public static Player instance = null;
    public float speed, rotationSpeed;
    public GameObject cylinderAttack;
    public GameObject cylinderAttackPosition;

    private bool fire1Pressed = false, fire2Pressed = false;
    private int cylinderState = 0;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this);
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        if (Input.GetAxis("Fire1") == 0) fire1Pressed = false;
        else if (!fire1Pressed && Input.GetAxis("Fire1") != 0)
        {
            fire1Pressed = true;
            if (cylinderState == 0)
            {
                cylinderAttack.transform.parent = null;
                cylinderAttack.GetComponentInChildren<Animator>().SetTrigger("Attack");
            }
            else if (cylinderState == 1)
            {
                cylinderAttack.GetComponentInChildren<Animator>().SetTrigger("Attack");
            }
            cylinderState++;
        }
        if (Input.GetAxis("Fire2") == 0) fire2Pressed = false;
        if (!fire2Pressed && Input.GetAxis("Fire2") != 0)
        {
            fire2Pressed = true;
            if (cylinderState == 0)
            {
                cylinderAttack.transform.parent = null;
                cylinderAttack.GetComponentInChildren<Animator>().SetTrigger("LargeAttack");
                cylinderState = -1;
            }
        }
    }

    public void MovePlayer()
    {
        transform.Rotate(Vector3.up * Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime);
        transform.Translate(Vector3.forward * Input.GetAxis("Vertical") * speed * Time.deltaTime);
    }

    public void SetActionToIdle()
    {
        StartCoroutine(ReturnCylinderToOrigin());
    }

    private IEnumerator ReturnCylinderToOrigin()
    {
        cylinderState = -1;

        float t = 0;
        Vector3 initialPosition = cylinderAttack.transform.position;
        Quaternion initialRotation = cylinderAttack.transform.rotation;
        while (t < 1)
        {
            yield return null;
            cylinderAttack.transform.position = Vector3.Lerp(initialPosition, cylinderAttackPosition.transform.position, t);
            cylinderAttack.transform.rotation = Quaternion.Slerp(initialRotation, cylinderAttackPosition.transform.rotation, t);
            t += 0.05f;
        }

        cylinderState = 0;
        cylinderAttack.transform.parent = gameObject.transform;
        cylinderAttack.transform.position = cylinderAttackPosition.transform.position;
    }
}
