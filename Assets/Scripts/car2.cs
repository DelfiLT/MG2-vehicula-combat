using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class car2 : CarController
{
    private void FixedUpdate()
    {
        WheelLogic();
        currentAcceleration = acceleration * Input.GetAxis("Vertical_P2");
        currentTurnAngle = maxTurnAngle * Input.GetAxis("Horizontal_P2");

        if (Input.GetKey(KeyCode.P))
        {
            currentBreakForce = breakingForce;
        }
        else
        {
            currentBreakForce = 0f;
        }

        if (Input.GetKey(KeyCode.O) && canFire)
        {
            canFire = false;
            FireBullet();

        }

        if (!canFire)
        {
            timer += Time.deltaTime;
            if (timer > timeToFire)
            {
                canFire = true;
                timer = 0;
            }
        }
    }
}
