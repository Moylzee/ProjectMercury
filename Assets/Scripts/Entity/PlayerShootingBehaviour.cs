using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShootingBehaviour : MonoBehaviour
{
    private Vector3 mousePos;
    public GameObject bullet;
    public GameObject mag;
    public Transform bulletTransform;
    public float fireRate;
    private PlayerInventory WeaponReference;
    private AudioScript audioScript;
    private GameObject Player;
    private Coroutine reloadCoroutine;
    private Coroutine reloadAnimationCoroutine;


    public void OffsetPosition(float x)
    {
        transform.position = new Vector3(x, transform.position.y, transform.position.z);
    }

    private void Start()
    {
        Player = GameObject.FindWithTag("Player");
        audioScript = Player.GetComponent<AudioScript>();
        if (audioScript == null)
        {
            Debug.LogError("AudioScript not found");
        }
    }

    void Update()
    {
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
        if(Player.GetComponent<PlayerObject>().Inventory.getEquippedWeapon() == null)
        {
            return;
        }


        if (Player.GetComponent<PlayerObject>().Inventory.getEquippedWeapon().IsReloading)
        {
            // TODO: Let the player know that they can't shoot whilst reloading
            return;
        }

        ushort BulletsInMag = Player.GetComponent<PlayerObject>().Inventory.getEquippedWeapon().GetBulletsInMag();
        if (BulletsInMag > 0)
        {

            GameObject b = ObjectPool.SharedInstance.GetPooledObjectBullet();
            if (b != null)
            {
                b.GetComponent<BulletMovement>().Init(bulletTransform.position);
                b.GetComponent<BulletDamage>().SetDamage((int)Player.GetComponent<PlayerObject>().Inventory.getEquippedWeapon().GetDamageDealtPerBullet());
                b.SetActive(true);
                audioScript.ShootSound();
                Player.GetComponent<PlayerObject>().Inventory.getEquippedWeapon().SetBulletsInMag((ushort)(BulletsInMag - 1));
                Player.GetComponent<PlayerObject>().Inventory.UpdateDetails();
            }

            return;

        }

        // TODO: let the player know that a reload is required
        return;
    }


    public void Reload()
    {

        // TODO: let the player know that they are reloading
        WeaponReference = Player.GetComponent<PlayerObject>().Inventory;
        uint SpareAmmo = WeaponReference.GetWeaponSpareAmmoBasedOnCategory();
        ushort MagSize = WeaponReference.getEquippedWeapon().GetMagazineSize();
        ushort AmmoLeftInMag = WeaponReference.getEquippedWeapon().GetBulletsInMag();
        uint ReloadTime = WeaponReference.getEquippedWeapon().GetReloadRate();

        // Weapon magazine is full; Cannot reload
        if (WeaponReference.getEquippedWeapon().GetBulletsInMag() >= WeaponReference.getEquippedWeapon().GetMagazineSize())
        {
            return;
        }

        WeaponReference.getEquippedWeapon().IsReloading = true;


        // Start reloading routine
        reloadCoroutine = StartCoroutine(ReloadDelay(SpareAmmo, MagSize, AmmoLeftInMag, ReloadTime));
    }

    /*
     * If reloading gets interrupted stop reloading weapon 
     */
    public void StopReloading()
    {
        if (reloadCoroutine == null)
        {
            return;
        }
        StopCoroutine(reloadCoroutine);
        StopCoroutine(reloadAnimationCoroutine);

        if(WeaponReference == null)
        {
            return;
        }
        WeaponReference.getEquippedWeapon().IsReloading = false;
    }

    /**
     * Reload Animation method
     */

    private IEnumerator ReloadAnimation(uint ReloadTime)
    {
        float interval = (float)(ReloadTime / 3f);

        for (float i = 0; i < ReloadTime; i += interval)
        {
            var magObject = Instantiate(mag, transform.position, Quaternion.identity);
            audioScript.ReloadSound();
            Destroy(magObject, 2);
            yield return new WaitForSeconds(interval);
        }

    }

    /**
     * Reload delay method
     */
    private IEnumerator ReloadDelay(uint SpareAmmo, ushort MagSize, ushort AmmoLeftInMag, uint ReloadTime)
    {
        // if no spare ammo exit
        if (SpareAmmo <= 0)
        {
            yield return 0;
        }

        // Start reloading animation
        reloadAnimationCoroutine = StartCoroutine(ReloadAnimation(ReloadTime));

        yield return new WaitForSeconds((float)ReloadTime); // wait for reload duration

        // Weapon reference was lost
        if(WeaponReference == null)
        {
            yield return 0;
        }

        // If there is more spare ammo in reserve than necessary
        if (SpareAmmo >= MagSize - AmmoLeftInMag)
        {
            WeaponReference.getEquippedWeapon().SetBulletsInMag(MagSize);
            PlayerInventory.SetWeaponSpareAmmoBasedOnCategory(WeaponReference, (uint)(SpareAmmo - (MagSize - AmmoLeftInMag)));
        }
        // When the player can't reload a full mag
        else if (SpareAmmo < MagSize - AmmoLeftInMag)
        {
            WeaponReference.getEquippedWeapon().SetBulletsInMag((ushort)(AmmoLeftInMag + SpareAmmo));
            PlayerInventory.SetWeaponSpareAmmoBasedOnCategory(WeaponReference, 0);
        }

        WeaponReference.getEquippedWeapon().IsReloading = false;

        // update the details for the weapon
        Player.GetComponent<PlayerObject>().Inventory.UpdateDetails();

    }

}
