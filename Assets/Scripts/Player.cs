using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance = null;
    public float speed, rotationSpeed, teleportationRange;
    public GameObject jumpTrail, shadow;

    private bool jumpPressed = false, inverseJumpPressed = false;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this);
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        if (jumpPressed || inverseJumpPressed)
        {
            ParticleSystem.EmissionModule emissionModule = jumpTrail.GetComponent<ParticleSystem>().emission;
            emissionModule.enabled = false;
        }

        if (Input.GetAxis("Jump") == 0) jumpPressed = false;
        else if (Input.GetAxis("Jump") != 0 && !jumpPressed)
        {
            jumpPressed = true;
            TeleportPlayer();
        }

        if (Input.GetAxis("InverseJump") == 0) inverseJumpPressed = false;
        else if (Input.GetAxis("InverseJump") != 0 && !inverseJumpPressed)
        {
            inverseJumpPressed = true;
            TeleportPlayer();
            transform.Rotate(Vector3.up * 180);
        }
    }

    public void MovePlayer()
    {
        transform.Rotate(Vector3.up * Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime);
        transform.Translate(Vector3.forward * Input.GetAxis("Vertical") * speed * Time.deltaTime);
    }

    public void TeleportPlayer()
    {
        Instantiate(shadow, transform.position, transform.rotation);
        ParticleSystem.EmissionModule emissionModule = jumpTrail.GetComponent<ParticleSystem>().emission;
        emissionModule.enabled = true;
        transform.Translate(Vector3.forward * teleportationRange);
    }
}
