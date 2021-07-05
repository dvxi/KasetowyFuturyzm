using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UsableItemsHandler : MonoBehaviour
{
    public LayerMask usableLayer;
    public GameObject usePopup;
    public Text useText;

    Ray ray;
    RaycastHit pointingAt;

    void Update()
    {
        ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0f));

        if (Physics.Raycast(ray, out pointingAt, 3f, usableLayer))
        {
            usePopup.SetActive(true);
        } else
        {
            usePopup.SetActive(false);
        }
    }
}
