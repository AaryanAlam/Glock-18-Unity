using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class gun : MonoBehaviour
{
    private Animator animator;
    public GameObject bullet;
    private GameObject bulletHolder;
    private AudioSource audioSource;
    public Camera aimCam;
    bool isMouseDown;
    bool isMouse2Down;
    private RaycastHit hit;
    private Ray Ray;
    public GameObject impactEff;
    public float bulletSpeed = 1400f;
    public float AimOffsetF = 0.7f;
    public float AimOffsetD = -0.5f;
    private Vector3 OgGunPos;

    private void Start()
    {
        animator = GetComponent<Animator>();
        bulletHolder = GameObject.FindGameObjectWithTag("BulletHolder");
        audioSource = GetComponent<AudioSource>();
        Vector3 offsetEY = Camera.main.transform.up * -0.5592621f;
        Vector3 offsetEZ = Camera.main.transform.forward * 0.957f;
        Vector3 offsetEX = Camera.main.transform.right * 0.4808595f;
        transform.position = Camera.main.transform.position + offsetEY + offsetEZ + offsetEX;

    }

    // Update is called once per frame
    void Update()
    {
        Ray = Camera.main.ViewportPointToRay(new Vector3(.5f, .5f, 0));


        if (Input.GetMouseButtonDown(1))
        {
            isMouse2Down = true;
        }

        if (Input.GetMouseButtonUp(1))
        {
            isMouse2Down = false;
        }


        if (Input.GetMouseButtonDown(0))
        {

            shotAnim();
        }

        if (isMouse2Down)
        {
            // Calculate the screen's center position in pixels
            Vector3 screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0);

            // Convert screen position to world position
            Vector3 worldCenter = Camera.main.ScreenToWorldPoint(screenCenter);

            // Calculate the offset to position the object slightly in front of the camera
            Vector3 offset = Camera.main.transform.forward * AimOffsetF;
            Vector3 offsetD = Camera.main.transform.up * AimOffsetD;


            // Set the object's position to the world center position plus the offset
            transform.position = worldCenter + offset + offsetD;
        }
        else
        {
            Vector3 offsetEY = Camera.main.transform.up * -0.5592621f;
            Vector3 offsetEZ = Camera.main.transform.forward * 0.957f;
            Vector3 offsetEX = Camera.main.transform.right * 0.4808595f;
            transform.position = Camera.main.transform.position + offsetEY + offsetEZ + offsetEX;


        }



    }


    void bulletShot()
    {

            if (Physics.Raycast(Ray, out hit, Mathf.Infinity))
            {
                GameObject impactEffGO = Instantiate(impactEff, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal)) as GameObject;
                Destroy(impactEffGO, 5);
                GameObject BulletGO = Instantiate(bullet, bulletHolder.transform.position, Quaternion.identity) as GameObject;


                Vector3 bulletDirection = (hit.point - BulletGO.transform.position).normalized;
                BulletGO.transform.rotation = Quaternion.LookRotation(bulletDirection);
                BulletGO.GetComponent<Rigidbody>().velocity = bulletDirection * bulletSpeed;
            }
        audioSource.Play();
    }

    void shotAnim()
    {
        animator.SetBool("shot", true);
    }

    void resetShotAnim()
    {
        animator.SetBool("shot", false);

    }


}
