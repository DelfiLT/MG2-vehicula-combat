using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class misil : MonoBehaviour
{
    public float speed;
    public Transform player;
    private int damage = 10;
    public GameObject particlePfab;

    void Start()
    {
        if(Input.GetKey(KeyCode.Q))
        {
            player = GameObject.FindGameObjectWithTag("Player2").GetComponent<Transform>();
        }
        if (Input.GetKey(KeyCode.I))
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        }
    }

    void Update()
    {
        if(player != null)
        {
            transform.LookAt(player.transform.position);
            transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<IgetDamaged>() != null)
        {
            other.gameObject.GetComponent<IgetDamaged>().GetDamaged(damage);
            GameObject particleVFX = Instantiate(particlePfab, transform.position, Quaternion.identity);
            Destroy(particleVFX, 2f);
            Destroy(this.gameObject);
        }

        if (other.gameObject.CompareTag("Terrain")) 
        {
            GameObject particleVFX = Instantiate(particlePfab, transform.position, Quaternion.identity);
            Destroy(particleVFX, 2f);
            Destroy(this.gameObject);
        }
    }
}
