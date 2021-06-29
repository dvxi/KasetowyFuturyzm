using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerScript : MonoBehaviour
{
    List<GameObject> activeAreas = new List<GameObject>();

    public GameObject areaPrefab;
    public Material[] areaMaterials;

    string[] tags = { "Boost", "Slow" };

    public void CreateArea(int type, Vector3 pos)
    {
        GameObject temp;

        temp = Instantiate(areaPrefab);
        temp.transform.position = pos;
        temp.GetComponent<AreaScript>().mng = this;

        temp.GetComponent<MeshRenderer>().material = areaMaterials[type]; //0 boost orange, 1 slow blue
        temp.tag = tags[type];
        
        activeAreas.Add(temp);

        //Debug.Log(activeAreas.Count);

        if (activeAreas.Count > 3)
        {
            Destroy(activeAreas[0]);
            activeAreas.RemoveAt(0);
        }
    }

    public void RemoveArea(GameObject obj)
    {
        activeAreas.Remove(obj);
    }
}
