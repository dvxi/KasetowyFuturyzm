using UnityEngine;

public class ShootScript : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform spawnPoint;
    public ManagerScript manager;

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot(0);
        } else if (Input.GetButtonDown("Fire2"))
        {
            Shoot(1);
        }
    }

    void Shoot(int type)
    {
        /*RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 100f)) //temp range set to 100 units
        {
            Debug.Log(hit.transform.name);
        }
        */

        GameObject bullet = Instantiate(bulletPrefab);
        bullet.transform.position = spawnPoint.position;
        bullet.transform.rotation = transform.rotation;

        bullet.GetComponent<Rigidbody>().AddForce(transform.forward * 2000f);

        bulletScript bulletScr = bullet.GetComponent<bulletScript>();

        bulletScr.manager = manager;
        bulletScr.type = type;

        Destroy(bullet, 5f);
    }
}
