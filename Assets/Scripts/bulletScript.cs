using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletScript : MonoBehaviour
{
    public int type;
    public ManagerScript manager;

    private void OnTriggerEnter(Collider other)
    {
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
        manager.CreateArea(type, transform.position);
        Destroy(gameObject);
    }
}
