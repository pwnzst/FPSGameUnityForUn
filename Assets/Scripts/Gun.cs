using System.Collections;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{

    [SerializeField]
    float damage = 10;

    public Transform fpsCam;
    public float range = 20;
    public float impactForce = 150;

    public int fireRate = 10;
    private float nextTimeToFire = 0;

    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;

    public int currentAmmo;
    public int maxAmmo = 30;
    public int magazineAmmo = 90;

    public float reloadTime = 2f;
    public bool isReloading;

    public Animator animator;

    InputAction shoot;
    InputAction reload;
    
    // Start is called before the first frame update
    void Start()
    {
        shoot = new InputAction("Shoot", binding: "<mouse>/leftButton");
        reload = new InputAction("Reload", binding: "<keyboard>/r");
        shoot.Enable();
        reload.Enable();
        currentAmmo = maxAmmo;

    }
    private void OnEnable()
    {
        isReloading = false;
        animator.SetBool("isReloading", false);
    }

    // Update is called once per frame
    void Update()
    {

        if (currentAmmo == 0 && magazineAmmo == 0)
        {
            animator.SetBool("isShooting", false);
            return;
        }

        if (isReloading)
            return;

        bool isShooting = shoot.ReadValue<float>() == 1;
        

        animator.SetBool("isShooting", isShooting);


        //test
        isReloading = reload.ReadValue<float>() == 1;

        if (isShooting && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Fire();
        }

        if ((currentAmmo == 0 && magazineAmmo > 0 && !isReloading) || ((currentAmmo < maxAmmo) && (magazineAmmo > 0) && isReloading))
        {
            StartCoroutine(Reload());
        }

        if(currentAmmo==0 && magazineAmmo == 0)
        {
            currentAmmo = maxAmmo;
            magazineAmmo += 10;
        }


    }

    private void Fire()
    {
        AudioManager.instance.Play("Shoot");

        muzzleFlash.Play();
        currentAmmo--;
        RaycastHit hit;
        
        
        
        if (Physics.Raycast(fpsCam.position, fpsCam.forward, out hit, range))
        {
            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
                //Debug.Log("стрельнул");
               
            }

            Quaternion impactRotation = Quaternion.LookRotation(hit.normal);
            GameObject impact = Instantiate(impactEffect, hit.point, impactRotation);
            impact.transform.parent = hit.transform;
            if (hit.transform.tag == "Enemy")
            {
                //Debug.Log("попал");
                EnemyHealth enemyHealthScript = hit.transform.GetComponent<EnemyHealth>();
                enemyHealthScript.DeductHealth(damage);
            }
            Destroy(impact, 5);
        }
    }

    IEnumerator Reload()
    {
        isReloading = true;
        animator.SetBool("isReloading", true);
        yield return new WaitForSeconds(reloadTime);
        animator.SetBool("isReloading", false);
        if (magazineAmmo >= maxAmmo && currentAmmo == 0)
        {
            currentAmmo = maxAmmo;
            magazineAmmo -= maxAmmo;
        }
        else if (currentAmmo < maxAmmo && magazineAmmo >= 1)
        {
            int tmpdif = (maxAmmo - currentAmmo);
            if (magazineAmmo >= tmpdif) {
                currentAmmo += tmpdif;
                magazineAmmo -= tmpdif;
            }
            else
            {
                currentAmmo += magazineAmmo;
                magazineAmmo = 0;
            }

        } else {
            currentAmmo = magazineAmmo;
            magazineAmmo = 0;
        }
        isReloading = false;
    }
}
