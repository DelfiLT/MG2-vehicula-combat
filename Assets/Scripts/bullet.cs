using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    private int damage = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<IgetDamaged>() != null)
        {
            other.gameObject.GetComponent<IgetDamaged>().GetDamaged(damage);
        }
    }
}
