using UnityEngine;

public class GenerateMap : MonoBehaviour
{
    [Header("  Map:")]
    public float mapRadius;
    public float mapBorder;
    public float startRingRadius;

    [Header("  Asteroids:")]
    public GameObject[] asteroidShapes;
    public Material[] asteroidMaterials;
    public Vector2 size;
    [Tooltip("The lowest and highest value by which particular dimensions of asteroid are modified owo")]
    public Vector2 deformation;

    
    [Header("  Freelancers")]
    public int freelancingAsteroids;

    [Header("  Clusters:")]
    public int clusters;
    public Vector2 radius;
    public Vector2 quantityOfAsteroids;

    [Header("  Large Clusters:")]
    public int largeClusters;
    public Vector2 radiusL;
    public Vector2 quantityOfAsteroidsL;

    // Start is called before the first frame update
    void Start()
    {
        for(int a = freelancingAsteroids; a > 0; a--)
        {
            int whichShape = Random.Range(0, asteroidShapes.Length);

            float theta = Random.Range(0, Mathf.PI * 2);
            float distance = Random.Range(startRingRadius, mapRadius - mapBorder);

            Vector3 position = new Vector3(Mathf.Cos(theta) * distance, 0, Mathf.Sin(theta) * distance);
            Quaternion rotation = new Quaternion(Random.value, Random.value, Random.value, Random.value);

            int whichMaterial = Random.Range(0, asteroidMaterials.Length);

            float overallSize = Random.Range(this.size.x, this.size.y);

            Vector3 size = new Vector3(overallSize * Random.Range(deformation.x, deformation.y), overallSize * Random.Range(deformation.x, deformation.y), overallSize * Random.Range(deformation.x, deformation.y));


            GameObject newAsteroid = Instantiate(asteroidShapes[whichShape], position, rotation);

            newAsteroid.GetComponent<Renderer>().material = asteroidMaterials[whichMaterial];

            newAsteroid.transform.localScale = size;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
