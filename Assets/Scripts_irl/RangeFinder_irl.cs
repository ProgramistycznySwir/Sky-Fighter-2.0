using UnityEngine;

public class RangeFinder_irl : MonoBehaviour
{
    public float maxRange = 10000f;
    public float currentDistance;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    protected RaycastHit hit;

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hit, maxRange)) currentDistance = Vector3.Distance(transform.position, hit.point);
        else currentDistance = -1f;
        
    }
}
