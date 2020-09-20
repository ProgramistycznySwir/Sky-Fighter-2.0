using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponMaster : MonoBehaviour
{
    public GameObject[] availableWeapons_;

    public static GameObject[] availableWeapons;

    // Start is called before the first frame update
    void Awake()
    {
        availableWeapons = availableWeapons_;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
