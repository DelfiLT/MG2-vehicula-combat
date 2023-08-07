using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class car1 : CarController, IgetDamaged, IpickObject
{
    private void Start()
    {
        fireUse = 1000;
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
            rocketParticle.SetActive(false);
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

        manageHealth();

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

        if (fireUse > 0 && Input.GetKey(KeyCode.E))
        {
            fireUse = fireUse - 2;
            slider.value = fireUse;
            flameThrower.SetActive(true);
        }
        else
        {
            flameThrower.SetActive(false);
        }

        if (fireUse <= 1000 && !Input.GetKey(KeyCode.E))
        {
            fireUse++;
            slider.value = fireUse;
            flameThrower.SetActive(false);
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
            rocketParticle.SetActive(true);
            activeMisil = true;
        }

        if (objectName == "speed")
        {
            StartCoroutine(activateTurbo());
        }

        if (objectName == "heal")
        {
            StartCoroutine(ActivateHealParticle());
            hp += 20;
        }
    }

    public void manageHealth()
    {
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
    }

    IEnumerator activateTurbo()
    {
        maxMotorTorque = maxMotorTorque * 2;
        velocityUI.SetActive(true);
        velocityParticle.SetActive(true);
        yield return new WaitForSeconds(10f);
        maxMotorTorque = maxMotorTorque / 2;
        velocityUI.SetActive(false);
        velocityParticle.SetActive(false);
    }

    IEnumerator ActivateHealParticle()
    {
        healParticle.SetActive(true);
        yield return new WaitForSeconds(2f);
        healParticle.SetActive(false);
    }

    IEnumerator FixRotation()
    {
        yield return new WaitForSeconds(3f);
        transform.rotation = Quaternion.identity;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            StartCoroutine(FixRotation());
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("FlameThrowerP2"))
        {
            hp = hp - 0.09f;
        }
    }
}
