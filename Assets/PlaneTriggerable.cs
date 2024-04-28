using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlaneTriggerable : MonoBehaviour
{
    public delegate void PlaneTrigger(GameObject plane);

    public UnityEvent OnPlaneEnter;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Trigger()
    {
        OnPlaneEnter.Invoke();
    }
}
