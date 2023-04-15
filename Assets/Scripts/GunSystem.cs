using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// future things to add. Maybe a zoom in thing with a reticle once we have a model that has it.
//only issue with this is it might make creating 3d models a bit difficult
public class GunSystem : MonoBehaviour
{
    // remind me to add all of this into a scritable object so that we dont have to change it here for each gun or maybe not actually since prefabs might be able to do a similar thing.
    public int damage;
    public float timeBetweenShooting, spread, range, reloadTime, timeBetweenShots;
    public int magazineSize, bulletsPerTap;
    public bool allowButtonHold;
    int bulletsLeft, bulletsShot;

    //bools
    bool shooting, readyToShoot, reloading;

    //Refrence Points
    public Camera fpsCam;
    public Transform attackPoint;
    public RaycastHit rayHit;
    public LayerMask whatIsEnemy;

    //Graphics
    public GameObject muzzleFlash, bulletHoleGraphic;
    //public CamShake camShake;
    //public float camShakeMagnitude, camShakeDurationl;
    public TextMeshProUGUI text;


    private void Awake()
    {
        bulletsLeft = magazineSize;
        readyToShoot = true;
        text.SetText(bulletsLeft + " / " + magazineSize);
    }
    private void Update()
    {
        if(!PauseMenu.isPaused)
            MyInput();

        //SetText
        
    }
    private void MyInput()
    {
        if (allowButtonHold) shooting = Input.GetKey(KeyCode.Mouse0);
        else shooting = Input.GetKeyDown(KeyCode.Mouse0);

        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading) Reload();

        //Shoot
        if (readyToShoot && shooting && !reloading && bulletsLeft > 0) {
            
            bulletsShot = bulletsPerTap;
            Shoot();
        }
    }

    private void Shoot() {
        readyToShoot = false;

        //Spread for gun unless we want to use spray patters which I would rather do even tho its more complicated 
        //also if we want increased spread while moving we can run an if (rigidbody.velocity.magnitude > 0) spread = spread * 1.5f; else spread = "normal spread";
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        //Calculate Direction with Spread
        Vector3 direction = fpsCam.transform.forward + new Vector3(x, y, 0);

        //RayCast
        if (Physics.Raycast(fpsCam.transform.position, direction, out rayHit, range, whatIsEnemy)) {
            Debug.Log(rayHit.collider.name);
            Instantiate(bulletHoleGraphic, rayHit.point, Quaternion.Euler(0, 100, 0));
           
            if (rayHit.collider.CompareTag("Enemy")) { 
                rayHit.collider.GetComponent<EnemyHealth>().Damage(damage);
                
            } 
            else if (rayHit.collider.CompareTag("Target"))
            {
                rayHit.collider.GetComponent<TargetCol>().Damage(damage);

            }
        }
        //cam shake ripped off the guid that we can code in if we want to
        //I can find the guide again if when we want to put this in.
        //ShakeCamer
        //camShake.Shake(camShakeDuration,camShakeMagnitude);

        //Graphics
        //ok so bullet hole always faces the same direction even if the wall is not facing that direction.
        
        Instantiate(muzzleFlash, attackPoint.position, Quaternion.identity);

        bulletsLeft--;
        bulletsShot--;
        text.SetText(bulletsLeft + " / " + magazineSize);
        Invoke("ResetShot", timeBetweenShooting);
        
        //for burts fire guns or shotguns so essentially more bullets per tap.
        if (bulletsShot > 0 && bulletsLeft > 0)
            Invoke("Shoot", timeBetweenShots);

    }

    private void ResetShot()
    {
        readyToShoot = true;
    }

    private void Reload() {
        reloading = true;
        text.SetText("Reloading..." + " / " + magazineSize);
        Invoke("ReloadFinished", reloadTime);
    }

    private void ReloadFinished()
    {
        bulletsLeft = magazineSize;
        reloading = false;
        text.SetText(bulletsLeft + " / " + magazineSize);
    }
}
