using UnityEngine;

public class HitscanWarmupWeapon_irl : Weapon_irl
{
    public LineRenderer laserBeamRenderer;
    public new Light light;

    [Tooltip("Time in seconds which weapon must shoot to achieve full damage")]
    public float warmupTime;
    public float dewarmingRatio;

    [Tooltip("Damage per second when weapon is fully warmed up")]
    public float dpsFull;

    ///Current phase of warmup 1 is full
    public float warmupPhase { get { return timeFiring / warmupTime; } }

    public Color colorOfLaser;

    public float beamZDislocation = -1f;

    private bool firing;
    private float timeFiring;


    private GradientColorKey[] colorKeys = new GradientColorKey[2];
    private GradientAlphaKey[] alphaKeys = new GradientAlphaKey[2];
    // Start is called before the first frame update
    void Start()
    {
        SetTier(tier);
    }

    float energyPerFrame;

    // Update is called once per frame
    void Update()
    {
        energyPerFrame = energyConsumption * Time.deltaTime;

        if (firing && CheckForEnergy())
        {
            laserBeamRenderer.gameObject.SetActive(true);


            GradientAlphaKey keyA1 = new GradientAlphaKey(warmupPhase, 0);
            GradientAlphaKey keyA2 = new GradientAlphaKey(warmupPhase, 1);

            alphaKeys[0] = keyA1;
            alphaKeys[1] = keyA2;

            Gradient finalGradient = new Gradient();

            finalGradient.alphaKeys = alphaKeys;
            finalGradient.colorKeys = colorKeys;

            laserBeamRenderer.colorGradient = finalGradient;
            

            light.enabled = true;

            if (timeFiring < warmupTime)
            {
                timeFiring += Time.deltaTime;
            }
             _Fire();
        }
        else
        {
            laserBeamRenderer.gameObject.SetActive(false);
            light.enabled = false;

            if (timeFiring > 0)
            {
                timeFiring -= Time.deltaTime * dewarmingRatio;
            }
        }
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

    protected void _Fire()
    {
        PlayerStats_irl stats = weaponManager.GetComponentInParent<PlayerStats_irl>();

        RaycastHit hit;

        laserBeamRenderer.SetPosition(0, laserBeamRenderer.transform.position + laserBeamRenderer.transform.forward * beamZDislocation);



        if (Physics.Raycast(laserBeamRenderer.transform.position, laserBeamRenderer.transform.forward, out hit, range))
        {

            if (hit.collider.tag == "Player")
            {
                hit.collider.transform.parent.GetComponentInParent<Stats>().ReceiveDamage(warmupPhase * dpsFull * Time.deltaTime, hit.point);
            }

            laserBeamRenderer.SetPosition(1, hit.point);
            light.transform.position = hit.point - transform.forward * 0.1f;
        }
        else
        {
            laserBeamRenderer.SetPosition(1, laserBeamRenderer.transform.position + laserBeamRenderer.transform.forward * range);
            light.enabled = false;
        }

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
                    dpsFull = 125f; //20
                    range = 800;
                    energyConsumption = 250f;
                    warmupTime = 3f;
                    SetBeamColor();
                    break;
                }
            case 2:
                {
                    dpsFull = 200f; //20
                    range = 1000;
                    energyConsumption = 350f;
                    warmupTime = 2.9f;
                    SetBeamColor();
                    break;
                }
            case 3:
                {
                    dpsFull = 300f; //20
                    range = 1100;
                    energyConsumption = 450f;
                    warmupTime = 2.8f;
                    SetBeamColor();
                    break;
                }
            case 4:
                {
                    dpsFull = 400f; //20
                    range = 1200;
                    energyConsumption = 600f;
                    warmupTime = 2.7f;
                    SetBeamColor();
                    break;
                }
            case 5:
                {
                    dpsFull = 500f; //20
                    range = 1350;
                    energyConsumption = 750f;
                    warmupTime = 2.65f;
                    SetBeamColor();
                    break;
                }
            case 6:
                {
                    dpsFull = 700f; //20
                    range = 1450;
                    energyConsumption = 900f;
                    warmupTime = 2.6f;
                    SetBeamColor();
                    break;
                }
            case 7:
                {
                    dpsFull = 1500f; //20
                    range = 1600;
                    energyConsumption = 1800f;
                    warmupTime = 2.5f;
                    SetBeamColor();
                    break;
                }
            case 8: //Nullo
                {
                    dpsFull = 1800f; //20
                    range = 1800;
                    energyConsumption = 1000f;
                    warmupTime = 2.3f;
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
        GradientColorKey keyC1 = new GradientColorKey(colorOfLaser, 0);
        GradientColorKey keyC2 = new GradientColorKey(colorOfLaser, 1);
        colorKeys[0] = keyC1;
        colorKeys[1] = keyC2;

        light.color = colorOfLaser;
    }
}
