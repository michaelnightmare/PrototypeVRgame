using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField]
    [Range(0.1f,1.5f)]
    private float fireRate = .3f;

    [SerializeField]
    [Range(1f,10f)]
    private int damage = 1;

    [SerializeField]
    private Transform firePoint;

    [SerializeField]
    private ParticleSystem muzzleParticle;

    [SerializeField]
    private AudioSource gunFireSource;
    


    private float timer;

    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= fireRate)
        {
            if(Input.GetButton("Fire1"))
            {
                timer = 0f;
                fireGun();
            }
        }
        
    }

    private void fireGun()
    {
        Debug.DrawRay(firePoint.position, firePoint.forward * 100, Color.red, 2f);

        muzzleParticle.Play();
        gunFireSource.Play();

        Ray ray = new Ray(firePoint.position, firePoint.forward);
        RaycastHit hitInfo;


        if (Physics.Raycast(ray,out hitInfo,100))
        {
            var health = hitInfo.collider.GetComponent<Health>();
            if(health != null)
            {
                health.TakeDamage(damage);
            }
        }

    }
}
