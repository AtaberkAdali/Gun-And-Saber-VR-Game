using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
   
    public float fireRate = 0.1f;
    public GameObject bulletPrefab;

    float elapsedTime;

    public Transform nozzleTransform;

 
    public Animator gunAnimator;

    public OVRInput.Button shootingButton;

    public GameObject slicerGO;

    // Update is called once per frame
    void Update()
    {
        //elapsed time
        elapsedTime += Time.deltaTime;
        //
        if (OVRInput.GetDown(shootingButton, OVRInput.Controller.RTouch))
        {
            if (elapsedTime > fireRate)
            {
                Shoot();
                
                elapsedTime = 0;
            }
            Debug.Log("tikladi");
        }

    }

    private void Shoot()
    {
        //Play sound
        AudioManager.instance.gunSound.gameObject.transform.position = nozzleTransform.position;
        AudioManager.instance.gunSound.Play();

        //Play animation
        gunAnimator.SetTrigger("Fire");

      
        //Create the bullet
        GameObject bulletGameobject = Instantiate(bulletPrefab, nozzleTransform.position, Quaternion.Euler(0, 0, 0));
        bulletGameobject.transform.forward = nozzleTransform.forward;

        Physics.IgnoreCollision(bulletGameobject.GetComponent<Collider>(), slicerGO.GetComponent<Collider>());
    }

   


}
