using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerScript : MonoBehaviour
{
    GameObject[] activeAreas;

    public GameObject areaPrefab;
    public Material[] areaMaterials;

    string[] tags = { "Boost", "Slow" };
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void CreateArea(int type, Vector3 pos)
    {
        GameObject temp;

        temp = Instantiate(areaPrefab);
        temp.transform.position = pos;

        temp.GetComponent<MeshRenderer>().material = areaMaterials[type]; //0 boost orange, 1 slow blue
        temp.tag = tags[type];
    }
}
