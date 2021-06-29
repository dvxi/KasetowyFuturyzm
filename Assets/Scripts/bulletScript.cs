using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletScript : MonoBehaviour
{
    public int type;
    public ManagerScript manager;

    GameObject currTouchingArea;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Boost" || other.tag == "Slow")
        {
            currTouchingArea = other.gameObject;
        }
        else
        {
            gameObject.GetComponent<Rigidbody>().isKinematic = true;
            manager.CreateArea(type, transform.position);

            Destroy(currTouchingArea);
            Destroy(gameObject);
        }
    }
}
