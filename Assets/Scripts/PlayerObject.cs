using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObject: MonoBehaviour
{
    public GameObject position;

    protected int objectState = 0;

    public void ReturnObjectToOrigin()
    {
        StartCoroutine(ReturnObjectToOriginCoroutine());
    }

    private IEnumerator ReturnObjectToOriginCoroutine()
    {
        objectState = -1;

        float t = 0;
        Vector3 initialPosition = gameObject.transform.position;
        Quaternion initialRotation = gameObject.transform.rotation;
        while (t < 1)
        {
            yield return null;
            gameObject.transform.position = Vector3.Lerp(initialPosition, position.transform.position, t);
            gameObject.transform.rotation = Quaternion.Slerp(initialRotation, position.transform.rotation, t);
            t += 0.05f;
        }

        objectState = 0;
        gameObject.transform.parent = Player.instance.transform;
        gameObject.transform.position = position.transform.position;
    }

    public void IncrementObjectState()
    {
        objectState += 1;
    }

    public void SetObjectState(int state)
    {
        objectState = state;
    }
}
