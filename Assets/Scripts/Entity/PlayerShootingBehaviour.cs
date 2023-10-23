using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShootingBehaviour : MonoBehaviour
{
    private Camera mainCam;
    private Vector3 mousePos;
    public GameObject bullet;
    public Transform bulletTransform;
    private bool canFire = true;
    private float timer;
    public float fireRate;

    private GameObject Player;

    private void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        Player = GameObject.FindWithTag("Player");
    }

    void Update()
    { 
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 rotation = mousePos - transform.position;
        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotZ);
        

    }

    public void Shoot()
    {
        Debug.Log("Pew pew");


        Debug.Log("Equipped Weapon >> " + Player.GetComponent<PlayerObject>().Inventory.getEquippedWeapon().GetItemName());
        float FireRate = Player.GetComponent<PlayerObject>().Inventory.getEquippedWeapon().GetFireRate();
        Debug.Log("Fire rate of weapon >> " + FireRate);
        InvokeRepeating("ShootBullet", 0, 1 / FireRate != 0 ? 1 / FireRate : 1);

    }


    public void StopShoot()
    {
        CancelInvoke("ShootBullet");
    }

    void ShootBullet()
    {

        ushort BulletsInMag = Player.GetComponent<PlayerObject>().Inventory.getEquippedWeapon().GetBulletsInMag();
        if(BulletsInMag > 0)
        {
            GameObject b = Instantiate(bullet, bulletTransform.position, Quaternion.identity);
            b.GetComponent<BulletDamage>().SetDamage((int)Player.GetComponent<PlayerObject>().Inventory.getEquippedWeapon().GetDamageDealtPerBullet());
            Player.GetComponent<PlayerObject>().Inventory.getEquippedWeapon().SetBulletsInMag((ushort)(BulletsInMag - 1));

        }
        else
        {
            Debug.Log("Need to reload");
            uint SpareAmmo = Player.GetComponent<PlayerObject>().Inventory.getEquippedWeapon().GetSpareAmmo();
            ushort MagSize = Player.GetComponent<PlayerObject>().Inventory.getEquippedWeapon().GetMagazineSize();
            StartCoroutine(ReloadDelay(SpareAmmo, MagSize));

           

        }


        Player.GetComponent<PlayerObject>().Inventory.UpdateDetails();
    }

    private IEnumerator ReloadDelay(uint SpareAmmo, ushort MagSize)
    {
        yield return new WaitForSeconds(2f);

        if (SpareAmmo > 0)
        {
            if (SpareAmmo >= MagSize)
            {
                Player.GetComponent<PlayerObject>().Inventory.getEquippedWeapon().SetBulletsInMag(MagSize);
                Player.GetComponent<PlayerObject>().Inventory.getEquippedWeapon().SetSpareAmmo(SpareAmmo - MagSize);
            }
            else
            {
                Player.GetComponent<PlayerObject>().Inventory.getEquippedWeapon().SetBulletsInMag((ushort)MagSize);
                Player.GetComponent<PlayerObject>().Inventory.getEquippedWeapon().SetSpareAmmo(0);
            }

        }

    }

}
