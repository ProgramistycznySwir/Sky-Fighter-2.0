using UnityEngine;

public class MasterShipColor : MonoBehaviour
{
    public Color shipColor;
    public float hullValue = 0.5f;
    public Renderer[] hull;
    public TextMesh[] texts;
    public TrailRenderer[] trails;
    
    void Start()
    {
        for(int a = 0; a < hull.Length; a++)
        {
            float h, s, v;
            Color.RGBToHSV(shipColor, out h, out s, out v);
            hull[a].material.color = Color.HSVToRGB(h, s, v * hullValue);
        }
        for (int a = 0; a < texts.Length; a++)
        {
            texts[a].color = shipColor;
        }
        for (int a = 0; a < trails.Length; a++)
        {
            trails[a].startColor = shipColor;
        }
    }
}
