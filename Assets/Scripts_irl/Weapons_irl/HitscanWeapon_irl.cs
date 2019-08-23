using UnityEngine;

public class HitscanWeapon_irl : Weapon_irl
{
    public LineRenderer laserBeamRenderer;
    public new Light light;

    public float dps;

    public Color colorOfLaser;

    public float beamZDislocation = -1f;

    protected bool firing;

    // Start is called before the first frame update
    void Start()
    {
        SetTier(tier);
    }

    float energyPerFrame;

    // Update is called once per frame
    void Update() //370u/s is hards speed limit couse of hitscan hitting itself
    {

        //energyPerFrame = energyConsumption * Time.deltaTime;

        if (firing && CheckForEnergy())
        {
            laserBeamRenderer.gameObject.SetActive(true);
            light.enabled = true;
            _FireAnimation();
        }
        else
        {
            laserBeamRenderer.gameObject.SetActive(false);
            light.enabled = false;
        }
        //firing = false;
    }

    void FixedUpdate() //370u/s is hards speed limit couse of hitscan hitting itself
    {

        energyPerFrame = energyConsumption * Time.fixedDeltaTime;

        if (firing && CheckForEnergy())
        {
            //laserBeamRenderer.gameObject.SetActive(true);
            //light.enabled = true;
            _Fire();
        }
        //else
        //{
        //    laserBeamRenderer.gameObject.SetActive(false);
        //    light.enabled = false;
        //}
        firing = false;
    }

    public override void Fire()
    {
        firing = true;
    }

    bool CheckForEnergy()
    {
        if (weaponManager.GetComponentInParent<PlayerStats_irl>().energy > energyPerFrame) return true;
        else return false;
    }

    protected void _FireAnimation()
    {
        RaycastHit hit;

        laserBeamRenderer.SetPosition(0, laserBeamRenderer.transform.position + laserBeamRenderer.transform.forward * beamZDislocation);

        if (Physics.Raycast(laserBeamRenderer.transform.position, laserBeamRenderer.transform.forward, out hit, range))
        {
            laserBeamRenderer.SetPosition(1, hit.point);
            light.transform.position = hit.point - transform.forward * 0.1f;
        }
        else
        {
            laserBeamRenderer.SetPosition(1, laserBeamRenderer.transform.position + laserBeamRenderer.transform.forward * range);
            light.enabled = false;
        }
    }

    protected void _Fire()
    {
        PlayerStats_irl stats = weaponManager.GetComponentInParent<PlayerStats_irl>();

        RaycastHit hit;

        //laserBeamRenderer.SetPosition(0, laserBeamRenderer.transform.position + laserBeamRenderer.transform.forward * beamZDislocation);

        if (Physics.Raycast(laserBeamRenderer.transform.position, laserBeamRenderer.transform.forward, out hit, range))
        {

            if (hit.collider.tag == "Player")
            {
                hit.collider.transform.parent.GetComponentInParent<Stats>().ReceiveDamage(dps * Time.deltaTime, hit.point);
            }

            //laserBeamRenderer.SetPosition(1, hit.point);
            //light.transform.position = hit.point - transform.forward * 0.1f;
        }
        //else
        //{
        //    laserBeamRenderer.SetPosition(1, laserBeamRenderer.transform.position + laserBeamRenderer.transform.forward * range);
        //    light.enabled = false;
        //}

        stats.energy -= energyPerFrame;
    }

    public override void SetTier(int tier)
    {
        this.tier = tier;
        SetColor(tier);
        colorOfLaser = TierColor(tier);

        switch (tier)
        {
            case 1:
                {
                    dps = 100f; //20
                    range = 1200;
                    energyConsumption = 300f;
                    SetBeamColor();
                    break;
                }
            case 2:
                {
                    dps = 150f; //20
                    range = 1500;
                    energyConsumption = 450f;
                    SetBeamColor();
                    break;
                }
            case 3:
                {
                    dps = 200f; //20
                    range = 1800;
                    energyConsumption = 600f;
                    SetBeamColor();
                    break;
                }
            case 4:
                {
                    dps = 275f; //20
                    range = 2100;
                    energyConsumption = 825f;
                    SetBeamColor();
                    break;
                }
            case 5:
                {
                    dps = 350f; //20
                    range = 2400;
                    energyConsumption = 950f;
                    SetBeamColor();
                    break;
                }
            case 6:
                {
                    dps = 450f; //20
                    range = 3000;
                    energyConsumption = 1150f;
                    SetBeamColor();
                    break;
                }
            case 7:
                {
                    dps = 1000f; //20
                    range = 5000;
                    energyConsumption = 3000f;
                    SetBeamColor();
                    break;
                }
            case 8: //Nullo
                {
                    dps = 1200f; //20
                    range = 5000;
                    energyConsumption = 2000f;
                    SetBeamColor();
                    break;
                }
            default:
                {
                    Debug.LogWarning(gameObject.name + ": <WPN:PRO-00> I have failed while trying to give a tier to this goddly weapon :<");
                    break;
                }
        }
    }

    public void SetBeamColor()
    {
        laserBeamRenderer.startColor = colorOfLaser;
        laserBeamRenderer.endColor = colorOfLaser;
        light.color = colorOfLaser;
    }
}
