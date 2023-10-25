using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShootingBehaviour : MonoBehaviour
{
    private Vector3 mousePos;
    public GameObject bullet;
    private float bulletLifeSpan = 4f;
    public Transform bulletTransform;
    public float fireRate;

    private Weapon CurrentWeapon;
    private Weapon ReloadWeapon;

    private GameObject Player;

    private void Start()
    {
        Player = GameObject.FindWithTag("Player");
    }

    void Update()
    { 
        // Get the rotation for the bullet
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 rotation = mousePos - transform.position;
        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotZ);
        

    }

    public void Shoot()
    {
        // Get the fire rate of the currently equipped weapon
        float FireRate = Player.GetComponent<PlayerObject>().Inventory.getEquippedWeapon().GetFireRate();
        // The fire rate is then put in the formula 1 / FireRate
        InvokeRepeating("ShootBullet", 0, 1 / FireRate != 0 ? 1 / FireRate : 1);
    }

    // Stops shooting
    public void StopShoot()
    {
        CancelInvoke("ShootBullet");
    }

    /**
     * Shoot bullet method
     * Sets the Damage of the equipped weapon to the instantiated bullet
     * Subtracts the bullets left in the magazine
     * Ones no bullets are left in the magazine a reload in invoked
     * A reload takes n seconds and afterwards subtracts from the spare ammo
     */
    void ShootBullet()
    {
        CurrentWeapon = Player.GetComponent<PlayerObject>().Inventory.getEquippedWeapon();
        ushort BulletsInMag = Player.GetComponent<PlayerObject>().Inventory.getEquippedWeapon().GetBulletsInMag();
        if(BulletsInMag > 0)
        {
            GameObject b = Instantiate(bullet, bulletTransform.position, Quaternion.identity);
            b.GetComponent<BulletDamage>().SetDamage((int)Player.GetComponent<PlayerObject>().Inventory.getEquippedWeapon().GetDamageDealtPerBullet());
            Player.GetComponent<PlayerObject>().Inventory.getEquippedWeapon().SetBulletsInMag((ushort)(BulletsInMag - 1));

        }
        else
        {
            Reload();
            return;
        }


        Player.GetComponent<PlayerObject>().Inventory.UpdateDetails();
    }


    public void Reload()
    {
        uint SpareAmmo = Player.GetComponent<PlayerObject>().Inventory.getEquippedWeapon().GetSpareAmmo();
        ushort MagSize = Player.GetComponent<PlayerObject>().Inventory.getEquippedWeapon().GetMagazineSize();
        ReloadWeapon = Player.GetComponent<PlayerObject>().Inventory.getEquippedWeapon();
        StartCoroutine(ReloadDelay(SpareAmmo, MagSize));
        Player.GetComponent<PlayerObject>().Inventory.UpdateDetails();
    }

    /**
     * Reload delay method
     */
    private IEnumerator ReloadDelay(uint SpareAmmo, ushort MagSize)
    {
        yield return new WaitForSeconds(2f);

        if (SpareAmmo > 0)
        {
            if (SpareAmmo >= MagSize)
            {
                ReloadWeapon.SetBulletsInMag(MagSize);
                ReloadWeapon.SetSpareAmmo(SpareAmmo - MagSize);
            }
            else
            {
                ReloadWeapon.SetBulletsInMag((ushort)MagSize);
                ReloadWeapon.SetSpareAmmo(0);
            }
        }
        Player.GetComponent<PlayerObject>().Inventory.UpdateDetails();
    }

}
