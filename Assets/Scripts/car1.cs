using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class car1 : CarController, IgetDamaged
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

        if (Input.GetKey(KeyCode.LeftShift) && canFire)
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

    public void GetDamaged(int damage)
    {
        hp -= damage;
    }
}
