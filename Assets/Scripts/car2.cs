using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class car2 : CarController, IgetDamaged, IpickObject
{
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
        if (Input.GetKey(KeyCode.O) && canFire)
        {
            canFire = false;
            FireBullet();

        }

        if (Input.GetKey(KeyCode.I) && activeMisil)
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

        if (hp >= 50)
        {
            hpBars[0].SetActive(true);
            hpBars[1].SetActive(true);
            hpBars[2].SetActive(true);
            hpBars[3].SetActive(true);
            hpBars[4].SetActive(true);
            hp = 50;
        }

        if (hp > 30 && hp <= 40)
        {
            hpBars[0].SetActive(true);
            hpBars[1].SetActive(true);
            hpBars[2].SetActive(true);
            hpBars[3].SetActive(true);
            hpBars[4].SetActive(false);
        }

        if (hp > 20 && hp <= 30)
        {
            hpBars[0].SetActive(true);
            hpBars[1].SetActive(true);
            hpBars[2].SetActive(true);
            hpBars[3].SetActive(false);
            hpBars[4].SetActive(false);
        }

        if (hp > 10 && hp <= 20)
        {
            hpBars[0].SetActive(true);
            hpBars[1].SetActive(true);
            hpBars[2].SetActive(false);
            hpBars[3].SetActive(false);
            hpBars[4].SetActive(false);
        }

        if (hp <= 10)
        {
            hpBars[0].SetActive(true);
            hpBars[1].SetActive(false);
            hpBars[2].SetActive(false);
            hpBars[3].SetActive(false);
            hpBars[4].SetActive(false);
        }

        if (hp <= 0)
        {
            SceneManager.LoadScene("WinP1");
        }
    }

    private void FixedUpdate()
    {
        motor = maxMotorTorque * Input.GetAxis("Vertical_P2");
        steering = maxSteerAngle * Input.GetAxis("Horizontal_P2");

        if (Input.GetKey(KeyCode.P))
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
        if (objectName == "rocket")
        {
            rocketUI.SetActive(true);
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
        maxMotorTorque = maxMotorTorque * 2;
        velocityUI.SetActive(true);
        yield return new WaitForSeconds(10f);
        maxMotorTorque = maxMotorTorque / 2;
        velocityUI.SetActive(false);
    }
}
