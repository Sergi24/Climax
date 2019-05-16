using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cylinder : MonoBehaviour
{
    public static Cylinder instance = null;
    public GameObject cylinderPosition;

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
        if (Input.GetAxis("Fire1") == 0) fire1Pressed = false;
        else if (cylinderState != -1 && !fire1Pressed && Input.GetAxis("Fire1") != 0)
        {
            fire1Pressed = true;
            if (cylinderState == 0)
            {
                gameObject.transform.parent = null;
                gameObject.GetComponentInChildren<Animator>().SetTrigger("Attack");
            }
            else if (cylinderState == 1)
            {
                gameObject.GetComponentInChildren<Animator>().SetTrigger("Attack");
            }
            cylinderState++;
        }
        if (Input.GetAxis("Fire2") == 0) fire2Pressed = false;
        else if (!fire2Pressed && Input.GetAxis("Fire2") != 0)
        {
            fire2Pressed = true;
            if (cylinderState == 0)
            {
                gameObject.transform.parent = null;
                gameObject.GetComponentInChildren<Animator>().SetTrigger("LargeAttack");
                cylinderState = -1;
            }
        }
    }

    public void ReturnCylinderToOrigin()
    {
        StartCoroutine(ReturnCylinderToOriginCoroutine());
    }

    private IEnumerator ReturnCylinderToOriginCoroutine()
    {
        cylinderState = -1;

        float t = 0;
        Vector3 initialPosition = gameObject.transform.position;
        Quaternion initialRotation = gameObject.transform.rotation;
        while (t < 1)
        {
            yield return null;
            gameObject.transform.position = Vector3.Lerp(initialPosition, cylinderPosition.transform.position, t);
            gameObject.transform.rotation = Quaternion.Slerp(initialRotation, cylinderPosition.transform.rotation, t);
            t += 0.05f;
        }

        cylinderState = 0;
        gameObject.transform.parent = Player.instance.transform;
        gameObject.transform.position = cylinderPosition.transform.position;
    }
}
