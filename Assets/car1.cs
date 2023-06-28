using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class car1 : wheel
{
    private void FixedUpdate()
    {
        WheelLogic();
        currentAcceleration = acceleration * Input.GetAxis("Vertical_P1");
        currentTurnAngle = maxTurnAngle * Input.GetAxis("Horizontal_P1");

        if (Input.GetKey(KeyCode.Space))
        {
            currentBreakForce = breakingForce;
        }
        else
        {
            currentBreakForce = 0f;
        }
    }
}
