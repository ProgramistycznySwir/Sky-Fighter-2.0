using UnityEngine;

public class Stats : MonoBehaviour
{
    [Header("   Hull:")]
    public int hullTier = 1; //
    public float maxHull = 1000f;
    public float hull;
    public int autoRepairTier = 0; //
    public float autoRepair = 0f;
    public float autoRepairDelay = 5f;

    [Header("   Misc:")]
    public float timeFromLastAttack = 0f;

    // Start is called before the first frame update
    void Start()
    {
        // in start do every stuff you have to do like: set hull to maxHull
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // in here do every regenerations and stuff
    }

    protected virtual void Regenerations()
    {
        //stuff that is done every frame
    }

    public virtual void ReceiveDamage(float dmg, Vector3 point)
    {
        Debug.Log("MERP!!!!");
        hull -= dmg;
    }

    public static int boolToInt(bool a)
    {
        if (a) return 1;
        else return 0;
    }
}
