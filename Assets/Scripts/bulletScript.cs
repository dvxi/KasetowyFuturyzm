using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletScript : MonoBehaviour
{
    public int type;
    public ManagerScript manager;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if (other.tag != "Boost" && other.tag != "Slow")
        {
            gameObject.GetComponent<Rigidbody>().isKinematic = true;
            manager.CreateArea(type, transform.position);
            Destroy(gameObject);
        }
    }
}
