using UnityEngine;
using TMPro;

public class DisplayStats_irl : MonoBehaviour
{
    //TextMeshPro text

    [Header("Stats to display:")]
    public PlayerStats_irl stats;
    public new Rigidbody rigidbody;
    public WeaponManaging_irl weaponManager;
    public CameraMode_irl cameraMode;
    public RangeFinder_irl rangeFinder;


    [Header("HUD elements:")]
    public TextMeshPro hpText;
    public TextMeshPro energyText;
    public TextMeshPro shieldText;

    public TextMeshPro velocityText;

    public TextMeshPro weaponText;
    public TextMeshPro ammoText;
    protected Color ammoColor;

    public TextMeshPro cameraModeText;

    public TextMeshPro telemetry;
    protected Color telemetryColor;

    [Header("Console:")]
    public LogConsole_irl console_Irl;

    void Start()
    {
        ammoColor = ammoText.color;
        UpdateWeapon();
        telemetryColor = telemetry.color;
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
                    if (weaponManager.cannonAmmo == 0) ammoText.color = Color.red;
                    else ammoText.color = ammoColor;

                    ammoText.text = "Ammo: " + weaponManager.cannonAmmo + " / " + weaponManager.maxCannonAmmo;
                    break;
                }
            case 2:
                {
                    if (weaponManager.cannonAmmo == 0) ammoText.color = Color.red;
                    else ammoText.color = ammoColor;

                    ammoText.text = "Ammo: " + weaponManager.cannonAmmo + " / " + weaponManager.maxCannonAmmo + " (" + weaponManager.weapons[0].ammoConsumption + ")";
                    break;
                }
            case 3:
                {
                    ammoText.color = Color.HSVToRGB(weaponManager.weapons[0].transform.GetComponent<HitscanWarmupWeapon_irl>().warmupPhase * 0.33333f, 1f, 1f);
                    ammoText.text = "Warmup: " + System.Math.Round(weaponManager.weapons[0].transform.GetComponent<HitscanWarmupWeapon_irl>().warmupPhase * 100f) + "% (" + weaponManager.weapons[0].transform.GetComponent<HitscanWarmupWeapon_irl>().warmupTime + "s)";
                    break;
                }
            default:
                {
                    ammoText.text = "Ammo: NaN";
                    break;
                }
        }

        if (rangeFinder.currentDistance < 0)
        {
            telemetry.text = "???";
            telemetry.color = Color.red;
        }
        else if (rangeFinder.currentDistance > weaponManager.weapons[0].range || rangeFinder.currentDistance < weaponManager.weapons[0].safeRange) //doesn't matter which weapon is called couse both of them have same range
        {
            telemetry.text = System.Convert.ToString(System.Math.Round(rangeFinder.currentDistance, 1)) + "m";
            telemetry.color = Color.yellow;
        }
        else
        {
            telemetry.text = System.Convert.ToString(System.Math.Round(rangeFinder.currentDistance, 1)) + "m";
            telemetry.color = telemetryColor;
        }

    }

    public virtual void UpdateWeapon()
    {
        if(weaponManager.weaponTiers[weaponManager.currentWeaponID] == 8) weaponText.text = "Weapon: Null Field " + weaponManager.availableWeapons[weaponManager.currentWeaponID].name;
        else weaponText.text = "Weapon: " + weaponManager.availableWeapons[weaponManager.currentWeaponID].name + " " + RomanSignOfInt(weaponManager.currentWeaponTier);
    }

    public virtual void UpdateCameraMode()
    {
        cameraModeText.text = "Mode: " + cameraMode.modeName;
    }

    public virtual string RomanSignOfInt(int number)
    {
        switch (number)
        {
            case 1: return "I";
            case 2: return "II";
            case 3: return "III";
            case 4: return "IV";
            case 5: return "V";
            case 6: return "VI";
            case 7: return "VII";
            default: return "";
        }
    }
}
