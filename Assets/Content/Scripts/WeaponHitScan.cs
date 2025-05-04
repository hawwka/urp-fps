using UnityEngine;
using UnityEngine.VFX;

public class WeaponHitScan : MonoBehaviour
{
    public int damage = 25;
    public float range = 100f;
    public float fireRate = 10f;

    public VisualEffect muzzleFlash;
    public GameObject impactEffect;
    public AudioSource gunAudio;
    
    public Camera fpsCamera;

    float nextTimeToFire = 0f;
    
    void Update()
    {
        // Handle fire input and rate limiting
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }
    }

    void Shoot()
    {
        muzzleFlash.Play();
        gunAudio.Play();

        // Raycast from camera forward
        RaycastHit hit;
        Vector3 origin = fpsCamera.transform.position;
        Vector3 direction = fpsCamera.transform.forward;

        if (Physics.Raycast(origin, direction, out hit, range))
        {
            // Try to damage a target
            var target = hit.transform.GetComponent<Health>();
            if (target != null)
                target.TakeDamage(damage);

            return;
            
            // Spawn impact effect
            if (impactEffect != null)
            {
                GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impactGO, 2f);
            }
        }
    }
}
