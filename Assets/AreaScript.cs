using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaScript : MonoBehaviour
{
    public LayerMask lrl;
    public ManagerScript mng;
    private void Start()
    {
        Collider[] nearObects = Physics.OverlapSphere(transform.position, 2.5f, lrl);

        for (int i = 0; i < nearObects.Length; i++)
        {
            mng.RemoveArea(nearObects[i].gameObject);
        }
    }
}
