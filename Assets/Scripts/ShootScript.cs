using UnityEngine;
using System.Collections;

public class ShootScript : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject pointer;
    public Transform spawnPoint;
    public ManagerScript manager;
    public Camera cam;

    public Transform desiredPosition;
    public LayerMask hitBoxLayer;

    bool Shootable = true;

    void Update()
    {

        Debug.LogWarning(Shootable);

        if (Input.GetButtonDown("Fire1") && Shootable)
        {
            Shoot(0);
        } else if (Input.GetButtonDown("Fire2") && Shootable)
        {
            Shoot(1);
        }

        transform.position = Vector3.Lerp(transform.position, desiredPosition.position, Time.deltaTime * 75f);
        transform.rotation = Quaternion.Lerp(transform.rotation, desiredPosition.rotation, Time.deltaTime * 75f);
    }

    void Shoot(int type)
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0f));

        Shootable = false;

        //if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 2000f)) //temp range set to 100 units
        if (Physics.Raycast(ray, out hit, 2000f, hitBoxLayer))
        {
            //Debug.Log(hit.transform.name);
            //Debug.Log(hit.transform.position + " | " + transform.forward);

            GameObject point = Instantiate(pointer);
            point.transform.position = hit.point;
            Destroy(point, .5f);

            GameObject bullet = Instantiate(bulletPrefab);
            bullet.transform.position = spawnPoint.position;
            bullet.transform.rotation = transform.rotation;

            Vector3 moveVector = hit.point - bullet.transform.position;

            //moveVector= Vector3.MoveTowards(bullet.transform.position, hit.transform.position, .5f);

            Debug.Log(bullet.transform.position + " | " + hit.point + " | " + moveVector + " | " + transform.forward);

            //bullet.GetComponent<Rigidbody>().AddForce(transform.forward * 2000f);
            bullet.GetComponent<Rigidbody>().velocity = Vector3.zero;
            bullet.GetComponent<Rigidbody>().AddForce(moveVector * 2000f / Vector3.Distance(bullet.transform.position, hit.point));


            bulletScript bulletScr = bullet.GetComponent<bulletScript>();

            bulletScr.manager = manager;
            bulletScr.type = type;
            
            Destroy(bullet, 5f);
        }

        StartCoroutine(ShootDelay());
    }

    IEnumerator ShootDelay()
    {
        Debug.Log("before " + Shootable);
        yield return new WaitForSeconds(.5f);
        Shootable = true;
        Debug.Log("After: " + Shootable);
    }
}
