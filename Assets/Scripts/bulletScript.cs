using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletScript : MonoBehaviour
{
    public int type;
    public ManagerScript manager;
    public LayerMask wallLayer;

    /*private void Update()
    {
        if (Physics.CheckSphere(transform.position, .5f, wallLayer))
        {
            gameObject.GetComponent<Rigidbody>().isKinematic = true;
            manager.CreateArea(type, transform.position);
            Destroy(gameObject);
        }
    }*/

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ShootingTarget")
        {
            gameObject.GetComponent<Rigidbody>().isKinematic = true;
            manager.CreateArea(type, transform.position);
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
