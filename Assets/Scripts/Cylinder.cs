using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cylinder : PlayerObject
{
    public static Cylinder instance = null;

    private bool fire1Pressed = false, fire2Pressed = false;
    private AudioSource asource;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this);
    }

    private void Start()
    {
        asource = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Fire1") == 0) fire1Pressed = false;
        else if (objectState != -1 && !fire1Pressed && Input.GetAxis("Fire1") != 0)
        {
            fire1Pressed = true;
            if (objectState == 0)
            {
                gameObject.transform.parent = null;
                gameObject.GetComponentInChildren<Animator>().SetTrigger("Attack");
                PlayAirCutSound(1.1f);
            }
            else if (objectState == 1)
            {
                gameObject.GetComponentInChildren<Animator>().SetTrigger("Attack");
            }
            objectState++;
        }
        if (Input.GetAxis("Fire2") == 0) fire2Pressed = false;
        else if (!fire2Pressed && Input.GetAxis("Fire2") != 0)
        {
            fire2Pressed = true;
            if (objectState == 0)
            {
                gameObject.transform.parent = null;
                gameObject.GetComponentInChildren<Animator>().SetTrigger("LargeAttack");
                objectState = -1;
            }
        }
    }

    public void PlayAirCutSound(float delay)
    {
        StartCoroutine(PlayAirCutSoundCoroutine(delay));
    }

    public IEnumerator PlayAirCutSoundCoroutine(float delay)
    {
        yield return new WaitForSeconds(delay);
        asource.Play();
    }
}
