using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    private int damage = 1;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<IgetDamaged>() != null)
        {
            collision.gameObject.GetComponent<IgetDamaged>().GetDamaged(damage);
        }
    }
}
