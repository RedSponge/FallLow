using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCharacter : MonoBehaviour
{
    public Vector3 GlideOffset;
    public Vector3 GlideRotation = new Vector3(-15, 0, 0);

    public Vector3 FallOffset = new Vector3(0, 8, 0);
    public Vector3 FallRotation = new Vector3(0, 0, 0);

    public float PositionLerpFactor = 20;
    public float RotationLerpFactor = 20;
    public GameObject toFollow;

    private PlaneControlComponent planeControlComponent;

    // Start is called before the first frame update
    void Start()
    {
        planeControlComponent = toFollow.GetComponent<PlaneControlComponent>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 posOffset = planeControlComponent.IsFalling() ? FallOffset : GlideOffset;
        Vector3 rot = planeControlComponent.IsFalling() ? FallRotation : GlideRotation;

        Vector3 desiredPosition = toFollow.transform.position + toFollow.transform.rotation * posOffset;
        Quaternion desiredRotation = Quaternion.LookRotation(toFollow.transform.position - desiredPosition) * Quaternion.Euler(rot);

        gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, desiredPosition, PositionLerpFactor * Time.fixedDeltaTime);
        gameObject.transform.rotation = Quaternion.Lerp(gameObject.transform.rotation, desiredRotation, RotationLerpFactor * Time.fixedDeltaTime);
    }
}
