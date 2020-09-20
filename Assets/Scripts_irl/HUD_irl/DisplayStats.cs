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

    public TextMesh weaponText;
    public TextMesh ammoText;
    private Color ammoColor;

    // Start is called before the first frame update
    void Start()
    {
        ammoColor = ammoText.color;
        UpdateWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        hpText.text = $"Hull: {System.Convert.ToInt16(stats.hull * 100f / stats.maxHull)}%";
        energyText.text = $"Energy: {System.Convert.ToInt16(stats.energy * 100f / stats.maxEnergy)}%";
            shieldText.text = $"Shield: {((stats.shieldOnline) ? $"{System.Convert.ToInt16(stats.shield * 100f / stats.shieldCapacity)}%" : "ERR")}";
        
        velocityText.text = $"v: {rigidbody.velocity.magnitude.ToString("F2")}m/s";


        switch (weaponManager.currentWeaponID)
        {
            case 0:
                {
                    ammoText.color = (weaponManager.cannonAmmo == 0) ? Color.red : ammoColor;

                    ammoText.text = "Ammo: " + weaponManager.cannonAmmo + " / " + weaponManager.maxCannonAmmo;
                    break;
                }
            default:
                {
                    ammoText.text = "Ammo: NaN";
                    break;
                }
        }
        
    }

    public virtual void UpdateWeapon()
    {
        weaponText.text = $"Weapon: {WeaponMaster.availableWeapons[weaponManager.currentWeaponID].name}";
    }
}
