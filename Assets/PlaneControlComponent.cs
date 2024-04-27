using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class PlaneControlComponent : MonoBehaviour
{
    public float Speed = 3;
    public float RotationSpeed = 15;

    public float FallGravity = 100;
    private float FallSpeed;

    public bool isFalling;

    public float MaxRiseAngle = -30;
    public float MaxTiltAngle = 30;

    // Start is called before the first frame update
    void Start()
    {
        isFalling = false;

    }

    // Update is called once per frame
    void Update()
    {
    }

    void FixedUpdate()
    {
        if (!isFalling)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                StartFalling();
            }
            else
            {
                Glide(Time.fixedDeltaTime);
            }
        }

        if (isFalling)
        {
            if (!Input.GetKey(KeyCode.Space))
            {
                StopFalling();
            }
            else
            {
                Fall(Time.fixedDeltaTime);
            }
        }
    }

    private void Fall(float delta)
    {
        FallSpeed += FallGravity * delta;
        Vector3 euler = gameObject.transform.eulerAngles;
        euler.x = 0;
        euler.z = 0;
        gameObject.transform.eulerAngles = euler;
        gameObject.transform.Translate(Vector3.down * FallSpeed * delta, Space.World);
    }

    private void StopFalling()
    {
        isFalling = false;
    }

    private void StartFalling()
    {
        isFalling = true;
        FallSpeed = 0;
    }

    private void Glide(float delta)
    {
        float axis = Input.GetAxis("Horizontal");

        float desiredZRotation = axis * -MaxTiltAngle;
        Vector3 euler = gameObject.transform.localEulerAngles;
        euler.z = Mathf.LerpAngle(euler.z, desiredZRotation, 1 * delta);
        gameObject.transform.localEulerAngles = euler;

        gameObject.transform.localEulerAngles += Vector3.up * axis * RotationSpeed * delta;

        float vert = Input.GetAxis("Vertical");
        float desiredXRotation = Mathf.Min(vert * MaxRiseAngle, 0);
        euler = gameObject.transform.localEulerAngles;
        euler.x = Mathf.LerpAngle(euler.x, desiredXRotation, 10 * delta);
        gameObject.transform.localEulerAngles = euler;

        gameObject.transform.Translate(Vector3.forward * Speed * delta);
    }
}
