/// <summary>
/// wszystkie bronie mają ulepszenia obrażeń (hitscanowe mają ulepszenie czasowych obrażeń)
/// projectile mają ponadto ulepszenie szybkostrzelności i szybkości pocisków
/// hitscanowe zasięgu
/// rakietowe namierzania, szybkości rakiet i ich zwrotności
/// 
/// jeden tier odpowiada za wszystko
/// </summary>

using UnityEngine;

public class Weapon_irl : MonoBehaviour
{


    // <WPN-00> - GetMaterialByName failed by not finding any material of specified name

    static string s_colorName = "ShipColor (Instance)";

    public new Renderer renderer;

    public int tier;

    public float energyConsumption = 0;

    public int ammoConsumption = 0;

    public float range; //this is here instead of HitscanWeapon_irl couse of telemetry indicating stuff
    public float safeRange = 0; //range above which shooting is safe (currently only flak uses)

    protected WeaponManaging_irl weaponManager;

    public virtual void Fire()
    {
        Debug.Log("Dakka!");
    }

    public virtual void SetTier(int tier)
    {
        this.tier = tier;
    }

    public virtual void AssignWeaponManager(WeaponManaging_irl _weaponManager)
    {
        weaponManager = _weaponManager;
    }

    public static Color TierColor(int tier)
    {
        switch(tier)
        {
            case 1: return Color.HSVToRGB(0, 1f, 1f); //Red
            case 2: return Color.HSVToRGB(0.083333f, 1f, 1f); //Orange
            case 3: return Color.HSVToRGB(0.166666f, 1f, 1f); //Yellow
            case 4: return Color.HSVToRGB(0.333333f, 1f, 1f); //Green
            case 5: return Color.HSVToRGB(0.666666f, 1f, 1f); //Blue
            case 6: return Color.HSVToRGB(0.75f, 1f, 1f); //Purple
            case 7: return Color.HSVToRGB(0.092777f, 0.45f, 1f); //Peach
            case 8: return Color.HSVToRGB(0f, 0f, 0f); //Black
            default: return Color.white;
        }
    }

    public void SetColor(Color color)
    {
        GetMaterialByName(renderer.materials, s_colorName).color = Color.green;
    }

    public void SetColor(int tier)
    {
        GetMaterialByName(renderer.materials, s_colorName).color = TierColor(tier);
    }

    public Material GetMaterialByName(Material[] materials , string name)
    {
        for(int a = materials.Length - 1; a >= 0; a--)
        {
            if (materials[a].name == name) return materials[a];
        }
        Debug.LogWarning(gameObject.name + ": <WPN-00> I have failed to find " + name + " material in given materials :(");
        return null;
    }
}
