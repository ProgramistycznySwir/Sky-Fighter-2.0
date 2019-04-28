using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceObject : MonoBehaviour
{
    public bool invert;
    [Tooltip("If true, object as an upward dirrection will always take upward dirrection of object it's facing OwO")]
    public bool worldSpaceUp;
    public Transform objectToFace;

    // Update is called once per frame
    void Update()
    {
        if (worldSpaceUp) transform.LookAt(objectToFace);
        else transform.LookAt(objectToFace, transform.GetComponentInParent<Transform>().up);

        if (invert) transform.Rotate(new Vector3(0, 180, 0), Space.Self);
    }
}
