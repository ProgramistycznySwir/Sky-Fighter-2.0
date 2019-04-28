using UnityEngine;

public class PlayerStats_irl : Stats
{
    [Header("   Armor:")]

    public int armorTier = 0; //
    public float armorValue = 0f;

    [Header("   Energy:")]
    public int batteryTier = 1; //
    public float maxEnergy = 1000f;
    public float energy;
    public int reactorTier = 1; //
    public float reactor = 200f;

    [Header("   Shield:")]
    public bool shieldOnline = false;
    public int shieldCapacitorTier = 0; //
    public float shieldCapacity = 0f;
    public float shield;

    public int shieldGeneratorTier = 0; //
    public float shieldGenerator = 0f;
    public float shieldGeneratorEffeciency = 0f;
    public int shieldRebooterTier = 0; //
    public float shieldGeneratorDelay = 0f; //when shield is off delay is twice longer

    public ShieldAnimation_irl shieldAnimator;
    public GameObject deathEfect;


    public LogConsole_irl console;

    // Start is called before the first frame update
    void Start()
    {
        hull = maxHull;
        energy = maxEnergy;
        shield = shieldCapacity;

        console.CoolConsoleStartupMessage();
    }

    protected bool warnedAboutLowHull = false;

    // Update is called once per frame
    void Update()
    {
        CheckLife();
        timeFromLastAttack += Time.fixedDeltaTime;
        Regenerations();

        if ((hull / maxHull) < 0.1f && !warnedAboutLowHull)
        {
            warnedAboutLowHull = true;
            console.NewLog("Hull integrity critical!");
        }
        else if ((hull / maxHull) > 10f && warnedAboutLowHull) warnedAboutLowHull = false;
        
    }

    public override void ReceiveDamage(float dmg, Vector3 point)
    {
        timeFromLastAttack = 0;

        if (shield > 0)
        {

            float ratio = dmg / shieldCapacity;
            if (ratio > 1) ratio = 1;

            shield -= dmg;
            dmg = -shield;
            if (shield < 0)
            {
                hull -= dmg;
                shield = 0;
                shieldOnline = false;
                //shieldAnimator.Enabled(false);
            }

            ShieldReaction(ratio, point);
        }
        else hull -= dmg;

    }

    protected override void Regenerations()
    {
        if (hull < maxHull)
        {
            if (timeFromLastAttack > autoRepairDelay)
            {
                hull += autoRepair * Time.fixedDeltaTime;
                if (hull > maxHull) hull = maxHull;
            }
        }
        if (shieldOnline)
        {
            if (shield < shieldCapacity)
            {
                if (timeFromLastAttack > shieldGeneratorDelay/* * (2 - boolToInt(shieldOnline))*/)
                {
                    if (energy > shieldGenerator * shieldGeneratorEffeciency * Time.fixedDeltaTime)
                    {
                        shield += shieldGenerator * Time.fixedDeltaTime;
                        energy -= shieldGenerator * shieldGeneratorEffeciency * Time.fixedDeltaTime;
                        if (shield > shieldCapacity) shield = shieldCapacity;
                    }
                }
            }
        }
        else
        {
            if(timeFromLastAttack > shieldGeneratorDelay * 2)
            {
                shieldOnline = true;
                shieldAnimator.Enabled(true);
            }
        }

        if(energy < maxEnergy)
        {
            energy += reactor * Time.fixedDeltaTime;
            if(energy > maxEnergy)
            {
                energy = maxEnergy;
            }
        }
    }

    protected void ShieldReaction(float ratio, Vector3 point)
    {
        shieldAnimator.React(ratio, point);
    }

    public void CheckLife()
    {
        if (hull < 0) Die();
    }

    public void Die()
    {
        hull = maxHull;
        Instantiate(deathEfect, transform.position, transform.rotation);
        transform.position = new Vector3(0, 0, 0); //allocates player to coordinates after death
        console.NewLog("You have died...", 30f);
    }
}
