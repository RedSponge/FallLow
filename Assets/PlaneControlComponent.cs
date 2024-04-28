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

    public bool isLeftWingAttached;
    public bool isRightWingAttached;

    public float MaxRiseAngle = -30;
    public float MaxTiltAngle = 30;

    public float DriftLerpSpeed;
    public float DriftDownwardAngle;
    public float DriftRotationSpeed;
    public float DriftTiltAngle;

    public KeyCode Right;
    public KeyCode Left;
    public Animator LeftWingAnimator;
    public Animator RightWingAnimator;

    // Start is called before the first frame update
    void Start()
    {
        isLeftWingAttached = true;
        isRightWingAttached = true;
    }

    void Update()
    {
        bool rightInput = Input.GetKeyDown(Right);
        bool leftInput = Input.GetKeyDown(Left);
        if (rightInput)
        {
            //mAnimator.SetTrigger("LeftWingTrigger");
            isRightWingAttached = !isRightWingAttached;
        }
        if (leftInput)
        {
            isLeftWingAttached = !isLeftWingAttached;
        }

        LeftWingAnimator.SetBool("LeftWingAttached", isRightWingAttached);
        RightWingAnimator.SetBool("RightWingAttached", isLeftWingAttached);

    }

    void FixedUpdate()
    {
        if (!isLeftWingAttached && !isRightWingAttached)
        {
            Fall(Time.fixedDeltaTime);
        }
        else
        {
            StopFalling();
            if (isLeftWingAttached && isRightWingAttached)
            {
                Glide(Time.fixedDeltaTime);
            }
            else if (isLeftWingAttached)
            {
                DriftRight(Time.fixedDeltaTime);
            }
            else if (isRightWingAttached)
            {
                DriftLeft(Time.fixedDeltaTime);
            }
        }     //     }
              // }

        // if (isFalling)
        // {
        //     if (!Input.GetKey(KeyCode.Space))
        //     {
        //         StopFalling();
        //     }
        //     else
        //     {
        //         Fall(Time.fixedDeltaTime);
        //     }
        // }
    }

    private void DriftRight(float delta)
    {
        float desiredZRotation = -DriftTiltAngle;
        float desiredXRotation = DriftDownwardAngle;
        float deltaYRotation = DriftRotationSpeed;


        Vector3 euler = gameObject.transform.eulerAngles;
        euler.x = Mathf.LerpAngle(euler.x, desiredXRotation, DriftLerpSpeed * delta);
        euler.z = Mathf.LerpAngle(euler.z, desiredZRotation, DriftLerpSpeed * delta);
        euler.y += deltaYRotation * delta;

        gameObject.transform.eulerAngles = euler;

        gameObject.transform.Translate(Vector3.forward * Speed * delta);
    }

    private void DriftLeft(float delta)
    {
        float desiredZRotation = DriftTiltAngle;
        float desiredXRotation = DriftDownwardAngle;
        float deltaYRotation = -DriftRotationSpeed;


        Vector3 euler = gameObject.transform.eulerAngles;
        euler.x = Mathf.LerpAngle(euler.x, desiredXRotation, DriftLerpSpeed * delta);
        euler.z = Mathf.LerpAngle(euler.z, desiredZRotation, DriftLerpSpeed * delta);
        euler.y += deltaYRotation * delta;

        gameObject.transform.eulerAngles = euler;

        gameObject.transform.Translate(Vector3.forward * Speed * delta);
    }

    private void Fall(float delta)
    {
        FallSpeed += FallGravity * delta;
        Vector3 euler = gameObject.transform.eulerAngles;
        float desiredXRotation = 60;
        float desiredZRotation = 0;
        euler.x = Mathf.LerpAngle(euler.x, desiredXRotation, 1 * delta);
        euler.z = Mathf.LerpAngle(euler.z, desiredZRotation, 1 * delta);
        gameObject.transform.eulerAngles = euler;
        gameObject.transform.Translate(Vector3.down * FallSpeed * delta, Space.World);
    }

    private void StopFalling()
    {
        FallSpeed = 0;
        // isFalling = false;
    }

    private void Glide(float delta)
    {
        float axis = 0; //Input.GetAxis("Horizontal");

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

    public bool IsFalling()
    {
        return !isLeftWingAttached && !isRightWingAttached;
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entered trigger!");
        PlaneTriggerable triggerable = other.gameObject.GetComponent<PlaneTriggerable>();
        if (triggerable != null)
        {
            triggerable.Trigger();
        }
        Debug.Log(other.gameObject.name);
    }
}