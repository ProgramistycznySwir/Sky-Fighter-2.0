using UnityEngine;

public class DisplayStats : MonoBehaviour
{
    [Header("Stats to display:")]
    public PlayerStats_irl stats;
    public new Rigidbody rigidbody;
    public WeaponManaging_irl weaponManager;


    [Header("HUD elements:")]
    public TextMesh hpText;
    public TextMesh energyText;
    public TextMesh shieldText;

    public TextMesh velocityText;

    public TextMesh weapon;
    public TextMesh ammo;
    private Color ammoColor;

    // Start is called before the first frame update
    void Start()
    {
        ammoColor = ammo.color;
        UpdateWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        hpText.text = "Hull: " + System.Convert.ToString(System.Convert.ToInt16(stats.hull * 100f / stats.maxHull)) + "%";
        energyText.text = "Energy: " + System.Convert.ToString(System.Convert.ToInt16(stats.energy * 100f / stats.maxEnergy)) + "%";
        if (stats.shieldOnline) shieldText.text = "Shield: " + System.Convert.ToString(System.Convert.ToInt16(stats.shield * 100f / stats.shieldCapacity)) + "%";
        else shieldText.text = "Shield: ERR";

        velocityText.text = "v: " + System.Convert.ToSingle(System.Convert.ToInt32(rigidbody.velocity.magnitude * 100f)) / 100f + "m/s";


        switch (weaponManager.currentWeaponID)
        {
            case 0:
                {
                    if (weaponManager.cannonAmmo == 0) ammo.color = Color.red;
                    else ammo.color = ammoColor;

                    ammo.text = "Ammo: " + weaponManager.cannonAmmo + " / " + weaponManager.maxCannonAmmo;
                    break;
                }
            default:
                {
                    ammo.text = "Ammo: NaN";
                    break;
                }
        }
        
    }

    public virtual void UpdateWeapon()
    {
        weapon.text = "Weapon: " + weaponManager.availableWeapons[weaponManager.currentWeaponID].name;
    }
}
