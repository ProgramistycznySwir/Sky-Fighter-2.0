using UnityEngine;

public class WeaponManaging_irl : MonoBehaviour
{
    // <MNG-00> - Failed trying to assign tier to magazine

    public int magazineTier = 1;

    public int[] weaponTiers = { 1, 1, 1 };

    public KeyCode fire = KeyCode.G;
    public KeyCode cycleWeapon = KeyCode.R;

    public Transform hardpointR;
    public Transform hardpointL;

    public Weapon_irl[] weapons;


    public int cannonAmmo;
    public int maxCannonAmmo;

    public int rockets;
    public int maxRockets;

    public int currentWeaponID = 0; //as in availableWeapons[]: cannons, lasers, flak


    public DisplayStats_irl displayStats; //used for updating chosen weapon

    // Start is called before the first frame update
    void Start()
    {
        AssignWeapon(0); //gives ship basic weapon which is cannon
        SetTier(magazineTier); //ustawia tier magazynu amunicji

        cannonAmmo = maxCannonAmmo;
        rockets = maxRockets;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(fire))
        {
            foreach (Weapon_irl weapon in weapons)
            {
                weapon.Fire();
            }
        }
        if (Input.GetKeyDown(cycleWeapon))
        {
            CycleWeapon();
        }
    }

    public void SetTier(int tier)
    {
        this.magazineTier = tier;

        switch (tier)
        {
            case 1:
                {
                    maxCannonAmmo = 150; //100
                    maxRockets = 15;
                    break;
                }
            default:
                {
                    Debug.LogWarning(gameObject.name + " <MNG-00> I'm sowwi, I have disapointed you my master, but i tried my bests. Have here tier which i tried to assign: ( " + tier + " )");
                    break;
                }
        }
    }

    public void CycleWeapon()
    {
        if(currentWeaponID + 1 > WeaponMaster.availableWeapons.Length - 1)
        {
            ChangeWeapon(0);
        }
        else ChangeWeapon(currentWeaponID + 1);
    }

    private float[] weaponDelay = new float[2]; //don't forget about this (

    public void ChangeWeapon(int newWeaponID)
    {
        foreach(Weapon_irl weapon in weapons)
            Destroy(weapon.gameObject);

        currentWeaponID = newWeaponID;

        AssignWeapon(currentWeaponID);
    }

    private void AssignWeapon(int weaponID)
    {
        currentWeaponID = weaponID;
        {
            GameObject weaponR = Instantiate(WeaponMaster.availableWeapons[weaponID], hardpointR);
            weapons[0] = weaponR.GetComponent<Weapon_irl>();
            weapons[0].AssignWeaponManager(this);
            weapons[0].SetTier(weaponTiers[weaponID]);
        }

        {
            GameObject weaponL = Instantiate(WeaponMaster.availableWeapons[weaponID], hardpointL);
            weapons[1] = weaponL.GetComponent<Weapon_irl>();
            weapons[1].AssignWeaponManager(this);
            weapons[1].SetTier(weaponTiers[weaponID]);
        }
        displayStats.UpdateWeapon();
    }

    public void ReloadComponent()
    {
        Start();
    }

    /// <summary>
    /// Tier of current selected weapon
    /// </summary>
    public int currentWeaponTier{ get { return weaponTiers[currentWeaponID]; } }
}
