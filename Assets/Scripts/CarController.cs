using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarController : MonoBehaviour
{
    // Wheels Setup

    public List<AxleInfo> axleInfos;
    public float maxMotorTorque;
    public float maxSteerAngle;
    public float maxBreakingForce;
    protected float motor;
    protected float steering;
    protected float breakingForce;

    // Shoot Setup

    [SerializeField] protected GameObject bullet;
    [SerializeField] protected GameObject misil;
    [SerializeField] protected Transform bulletSpawn;
    [SerializeField] protected Transform carBody;

    [SerializeField] protected float bulletForce;
    [SerializeField] protected float timeToFire;
    protected float timer;
    protected bool canFire;

    public GameObject flameThrower;
    protected int fireUse;

    //Car stats

    [SerializeField] protected float hp;
    [SerializeField] protected bool activeMisil = false;
    [SerializeField] protected GameObject[] hpBars;
    protected Rigidbody carRb;

    //Particles

    [SerializeField] protected GameObject healParticle;
    [SerializeField] protected GameObject velocityParticle;
    [SerializeField] protected GameObject rocketParticle;

    //UI

    public GameObject rocketUI;
    public GameObject velocityUI;
    public GameObject centerOfMass;
    public Slider slider;


    protected void WheelLogic()
    {
        foreach (AxleInfo axleInfo in axleInfos)
        {
            if (axleInfo.steering)
            {
                axleInfo.leftWheel.steerAngle = steering;
                axleInfo.rightWheel.steerAngle = steering;
            }
            if (axleInfo.motor)
            {
                axleInfo.leftWheel.motorTorque = motor;
                axleInfo.rightWheel.motorTorque = motor;
            }
            if (axleInfo.breakingForce)
            {
                axleInfo.leftWheel.brakeTorque = breakingForce;
                axleInfo.rightWheel.brakeTorque = breakingForce;
            }
            UpdateWheelVisual(axleInfo.leftWheel);
            UpdateWheelVisual(axleInfo.rightWheel);
        }
    }

    protected void UpdateWheelVisual(WheelCollider collider)
    {
        if (collider.transform.childCount == 0)
        {
            return;
        }

        Transform visualWheel = collider.transform.GetChild(0);

        Vector3 position;
        Quaternion rotation;
        collider.GetWorldPose(out position, out rotation);

        visualWheel.transform.position = position;
        visualWheel.transform.rotation = rotation;
    }

    protected void FireBullet()
    {
        GameObject bulletClone = Instantiate(bullet, bulletSpawn.position, carBody.rotation);

        Rigidbody bulletRbLeft = bulletClone.GetComponent<Rigidbody>();

        bulletRbLeft.AddRelativeForce(Vector3.forward * bulletForce, ForceMode.Impulse);

        Destroy(bulletClone, 4);
    }

    protected void FireMisil()
    {
       Instantiate(misil, bulletSpawn.position, carBody.rotation);
    }

    [System.Serializable]
    public class AxleInfo
    {
        public WheelCollider leftWheel;
        public WheelCollider rightWheel;
        public bool motor;
        public bool steering;
        public bool breakingForce;
    }
}
