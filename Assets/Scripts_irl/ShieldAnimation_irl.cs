using UnityEngine;

public class ShieldAnimation_irl : MonoBehaviour
{
    public Stats stats;
    public Renderer shield;
    public MeshCollider collider;
    public float fullDisolveTime = 0.2f;
    public float maximalOpaqueness = 0.5f;

    public float currentLoad = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        shield.material.color = Color.HSVToRGB(0.472222f, 1f, currentLoad * maximalOpaqueness);

        //if (currentLoad > 0) ; //currentLoad -= Time.deltaTime / fullDisolveTime;
        //else
        //{
        //    SetDamageCenter(transform.forward);
        //    currentLoad = 0;
        //}
    }

    public void React(float ratio, Vector3 point)
    {
        currentLoad += ratio;
        SetDamageCenter(point);
    }
    public void Enabled(bool enabled)
    {
        collider.enabled = enabled;
    }

    /// <summary>
    /// Function for setting Damage Center for Shield Animations
    /// </summary>
    /// <param name="point"> Point in which damage was taken</param>
    /// <param name="ratio"> Strongness of attack</param>
    public void SetDamageCenter(Vector3 point)
    {
        float angle = 180 + Vector3.SignedAngle(Vector3.right, point - transform.position, Vector3.up);
        float _angle = angle / 360;
        shield.material.SetTextureOffset(Shader.PropertyToID("_MainTex"), new Vector2(_angle, 0));
        Debug.Log(angle + " : " + _angle);

        Debug.DrawLine(transform.position, Vector3.right, Color.red);
        Debug.DrawLine(transform.position, point - transform.position);
    }
}
