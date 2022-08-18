using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{

    public Transform barrel; // shooting pivot as Mehrab call it :)
    public GameObject bullet; // player bullet prefab
    public float projectionSpeed = 200;

    public ParticleSystem muzzleFX;
    public RectTransform aim; //cross hair
    private Camera cam; // used for cross hair
    private float aimDistance;
    private float aimFocus = 300; // focus speed
                               //public float aimDistance = 5;


    public enum weapone { rifle, shotgun, machinegun, minigun };
    public weapone curWeapone;
    private int curIndx = 0;
    public GameObject[] weaponeModels;
    private GameObject minigunBarrel;

    private float[] FPS = { 1.2f, 0.6f, 7, 15 };
    private float[] delays;
    private float nexttimetofire;
    private int burstshotCount = 0;

    private void Start()
    {
        delays = new float[4];
        for (int i = 0; i < 4; i++)
        {
            delays[i] = 1 / FPS[i];
        }

        changeGun(0);

        cam = Camera.main;
        minigunBarrel = weaponeModels[3].transform.GetChild(3).gameObject;
    }


    private void Update()
    {
        //inputs
        if (Input.GetKey(KeyCode.Mouse0))
        {
            shoot();
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            changeGun(curIndx + 1);
        }

        //fire rate
        nexttimetofire -= Time.deltaTime;

        //cross hair
        Ray ray = new Ray(barrel.position, barrel.forward);
        RaycastHit hit;
        Vector3 screenPos;
        Vector3 aimPos;
        if (Physics.Raycast(ray, out hit))
        {
            aimDistance = hit.distance; //Mathf.Lerp(aimDistance, hit.distance, Time.time);
            //aimDistance = Mathf.MoveTowards(aimDistance, hit.distance, Time.deltaTime * aimFocus);
        }
        else
        {
            aimDistance = 35;//Mathf.Lerp(aimDistance, 50, Time.time); // 50 for a default sky distance
        }
        aimPos = barrel.position + (barrel.forward * aimDistance);
        screenPos = cam.WorldToScreenPoint(aimPos, Camera.MonoOrStereoscopicEye.Mono);
        aim.position = Vector3.MoveTowards(aim.position, screenPos, Time.deltaTime * aimFocus);

    }


    public void changeGun(int gun)
    {
        gun = gun > 3 ? 0 : gun;
        curIndx = gun;

        switch (gun)
        {
            case 0:
                {
                    curWeapone = weapone.rifle;
                    for (int i = 0; i < weaponeModels.Length; i++)
                    {
                        weaponeModels[i].SetActive(i == gun);
                    }
                    break;
                }
            case 1:
                {
                    curWeapone = weapone.shotgun;
                    for (int i = 0; i < weaponeModels.Length; i++)
                    {
                        weaponeModels[i].SetActive(i == gun);
                    }
                    break;
                }
            case 2:
                {
                    curWeapone = weapone.machinegun;
                    for (int i = 0; i < weaponeModels.Length; i++)
                    {
                        weaponeModels[i].SetActive(i == gun);
                    }
                    break;
                }
            case 3:
                {
                    curWeapone = weapone.minigun;
                    for (int i = 0; i < weaponeModels.Length; i++)
                    {
                        weaponeModels[i].SetActive(i == gun);
                    }
                    break;
                }

            default: break;
        }
    }

    public void DisableGuns()
    {
        for (int i = 0; i < weaponeModels.Length; i++)
        {
            weaponeModels[i].SetActive(false);
        }
    }
    private void shoot()
    {
        if (!GameManager.ins._isEndGame && !GameManager.ins._isPause)
        {
            switch (curWeapone)
            {
                case weapone.rifle:
                    {
                        burstShoot();
                        break;
                    }
                case weapone.shotgun:
                    {
                        shotgunShoot();
                        break;
                    }
                case weapone.machinegun:
                    {
                        machineGunShoot();
                        break;
                    }
                case weapone.minigun:
                    {
                        minigunShoot();
                        break;
                    }
                default: break;
            }
        }
    }
    private void minigunShoot() // miningun shot
    {
        minigunBarrel.transform.Rotate(Vector3.forward, 15, Space.Self);
        if (nexttimetofire <= 0)
        {

            nexttimetofire = delays[3];
            GameObject projectile = Instantiate(bullet, barrel.position + (barrel.forward * 2), barrel.rotation);
            projectile.GetComponent<Rigidbody>().velocity = barrel.forward * projectionSpeed;
            muzzleFX.Play();
        }
    }

    private void machineGunShoot() // machinegun shot
    {
        if (nexttimetofire <= 0)
        {

            nexttimetofire = delays[2];
            GameObject projectile = Instantiate(bullet, barrel.position + (barrel.forward * 2), barrel.rotation);
            projectile.GetComponent<Rigidbody>().velocity = barrel.forward * projectionSpeed;
            muzzleFX.Play();
        }
    }

    private void burstShoot() // burst shot
    {
        if (nexttimetofire <= 0)
        {
            if (burstshotCount == 3)
            {
                nexttimetofire = delays[0];
                burstshotCount = 0;
            }
            else
            {
                nexttimetofire = 0.1f;
            }

            GameObject projectile = Instantiate(bullet, barrel.position + (barrel.forward * 2), barrel.rotation);
            projectile.GetComponent<Rigidbody>().velocity = barrel.forward * projectionSpeed;
            muzzleFX.Play();
            burstshotCount++;
        }
    }

    private void shotgunShoot() // shotgun shot
    {
        if (nexttimetofire <= 0)
        {
            nexttimetofire = delays[1];

            for (int i = 0; i < 8; i++)
            {

                Vector3 direction = barrel.forward;

                Vector3 spread = Vector3.zero;
                spread += barrel.up * Random.Range(-1, 1);
                spread += barrel.right * Random.Range(-1, 1);

                direction += spread.normalized * Random.Range(0, 0.15f);

                GameObject projectile = Instantiate(bullet, barrel.position + (barrel.forward * 2),
                Quaternion.LookRotation(Vector3.RotateTowards(barrel.forward, direction, 1, 0))
                );
                projectile.GetComponent<Rigidbody>().velocity = direction * projectionSpeed;
            }
            muzzleFX.Play();
        }
    }

}