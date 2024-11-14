using Kelvin;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField] Weapon weaponHolder;

    Weapon weapon;

    void Awake()
    {
        if (weaponHolder != null)
            weapon = Instantiate(weaponHolder);
        else{
            Debug.Log("no weapon holder");
        }
    }

    void Start()
    {
        if (weapon == null){
            Debug.Log("weap Null");
            return;
        }
            

        TurnVisual(false);

        weapon.enabled = false;
        weapon.transform.SetParent(transform, false);
        weapon.transform.localPosition = transform.position;

        weapon.parentTransform = transform;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(weapon == null){
            Debug.Log("no Weap");
        }
        else if(!other.gameObject.CompareTag("Player")){
            Debug.Log("Not A player");
        }
        // Trigger itu bisa ke semua objek, jadi harus cari si Player
        if (weapon != null && other.gameObject.CompareTag("Player"))
        {
            Weapon playerWeapon = other.gameObject.GetComponentInChildren<Weapon>();

            if (playerWeapon != null)
            {
                PickupHandler(true);
                playerWeapon.transform.SetParent(playerWeapon.parentTransform);
                playerWeapon.transform.localScale = new(1, 1);
                playerWeapon.transform.localPosition = new(0, 0);

                TurnVisual(false, playerWeapon);
            }

            weapon.enabled = true;
            weapon.transform.SetParent(other.transform, false);

            TurnVisual(true);
            PickupHandler(false);

            weapon.transform.localPosition = new(0.0f, 0.0f);
            Player player = other.GetComponent<Player>();
            if (player != null){
                player.SwitchWeapon(weapon, this);  // Pass the new weapon and this WeaponPickup instance
            }
        }
        else{
            Debug.Log("no reference");
        }
    }

    void TurnVisual(bool on)
    {
        if (on)
        {
            weapon.GetComponent<SpriteRenderer>().enabled = true;
            weapon.GetComponent<Animator>().enabled = true;
            weapon.GetComponent<Weapon>().enabled = true;
        }
        else
        {
            weapon.GetComponent<SpriteRenderer>().enabled = false;
            weapon.GetComponent<Animator>().enabled = false;
            weapon.GetComponent<Weapon>().enabled = false;
        }

    }

    void TurnVisual(bool on, Weapon weapon)
    {
        if (on)
        {
            weapon.GetComponent<SpriteRenderer>().enabled = true;
            weapon.GetComponent<Animator>().enabled = true;
            weapon.GetComponent<Weapon>().enabled = true;
        }
        else
        {
            weapon.GetComponent<SpriteRenderer>().enabled = false;
            weapon.GetComponent<Animator>().enabled = false;
            weapon.GetComponent<Weapon>().enabled = false;
        }

    }

    public void PickupHandler(bool state){
        gameObject.SetActive(state);
    }

}
