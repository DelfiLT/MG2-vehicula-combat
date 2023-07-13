using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class car2 : CarController, IgetDamaged, IpickObject
{
    private void Update()
    {
        if (Input.GetKey(KeyCode.O) && canFire)
        {
            canFire = false;
            FireBullet();

        }

        if (Input.GetKey(KeyCode.I) && activeMisil)
        {
            activeMisil = false;
            FireMisil();
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

        if (hp > 100)
        {
            hp = 100;
        }
    }
    private void FixedUpdate()
    {
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
        WheelLogic();
    }

    public void GetDamaged(int damage)
    {
        hp -= damage;
    }

    public void PickObject(string objectName)
    {
        if (objectName == "rocket")
        {
            activeMisil = true;
        }

        if (objectName == "speed")
        {
            StartCoroutine(activateTurbo());
        }

        if (objectName == "heal")
        {
            hp += 20;
        }
    }

    IEnumerator activateTurbo()
    {
        acceleration = acceleration * 2;
        yield return new WaitForSeconds(10f);
        acceleration = acceleration /2;
    }
}
