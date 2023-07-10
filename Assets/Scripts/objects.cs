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

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.GetComponent<IpickObject>() != null)
        {
            collision.gameObject.GetComponent<IpickObject>().PickObject(objectName);
            Destroy(this.gameObject);
        }
    }
}
