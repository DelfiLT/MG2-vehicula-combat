using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objects : MonoBehaviour
{
    public float rotateSpeed;
    public string objectName;

    void Update()
    {
        transform.Rotate(0, rotateSpeed, 0, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<IpickObject>() != null)
        {
            other.gameObject.GetComponent<IpickObject>().PickObject(objectName);
            Destroy(this.gameObject);
        }
    }
}
