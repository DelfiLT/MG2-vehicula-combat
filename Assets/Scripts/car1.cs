using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class car1 : CarController, IgetDamaged, IpickObject
{
    public TextMeshProUGUI hpText;
    public GameObject rocketUI;
    public GameObject velocityUI;
    public GameObject centerOfMass;
    private Rigidbody carRb;

    private void Start()
    {
        carRb = GetComponent<Rigidbody>();
        carRb.centerOfMass = centerOfMass.transform.localPosition;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift) && canFire)
        {
            canFire = false;
            FireBullet();

        }

        if (Input.GetKey(KeyCode.Q) && activeMisil)
        {
            rocketUI.SetActive(false);
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

        hpText.text = "HP: " + hp.ToString("00");

        if (hp > 50)
        {
            hp = 50;
        }

        if (hp <= 0)
        {
            SceneManager.LoadScene("WinP2");
        }
    }
    private void FixedUpdate()
    {
        motor = maxMotorTorque * Input.GetAxis("Vertical_P1");
        steering = maxSteerAngle * Input.GetAxis("Horizontal_P1");

        if (Input.GetKey(KeyCode.Space))
        {
            breakingForce = maxBreakingForce;
        }
        else
        {
            breakingForce = 0f;
        }

        WheelLogic();
    }

    public void GetDamaged(int damage)
    {
        hp -= damage;
    }

    public void PickObject(string objectName)
    {
        if(objectName == "rocket")
        {
            activeMisil = true;
            rocketUI.SetActive(true);
        }

        if(objectName == "speed")
        {
            StartCoroutine(activateTurbo());
        }

        if(objectName == "heal")
        {
            hp += 20;
        }
    }

    IEnumerator activateTurbo()
    {
        maxMotorTorque = maxMotorTorque * 1.5f;
        velocityUI.SetActive(true);
        yield return new WaitForSeconds(10f);
        maxMotorTorque = maxMotorTorque / 1.5f;
        velocityUI.SetActive(false);
    }
}
