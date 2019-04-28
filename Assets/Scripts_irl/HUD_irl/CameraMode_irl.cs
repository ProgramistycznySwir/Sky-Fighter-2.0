using UnityEngine;

public class CameraMode_irl : MonoBehaviour
{

    [Header("Sniper Mode:")]
    public KeyCode cycleMode = KeyCode.Q;

    [Tooltip("Sensitivity of specific modes: Travel, Fight, Sniper")]
    public float[] sensitivity = { 7.5f, 2.5f, 0.5f, 0.1f };
    [Tooltip("Position of camera in modes: Travel, Fight, Sniper")]
    public Vector3[] cameraPosition;
    [Tooltip("Camera size for modes: Travel, Fight, Sniper")]
    public float[] cameraFOV = { 30f, 30f, 7.5f, 1.875f };

    public string modeName = "Travel";
    public int mode = 0; //Travel, Fight, Sniper 4x, Sniper 16x
    private const int numberOfModes = 4;

    public PlayerControlAndMovement movement;
    public Transform HUD;
    public new Camera camera;

    public DisplayStats_irl displayStats; // for updating camera mode in HUD

    // Start is called before the first frame update
    void Start()
    {
        sensitivity[0] = movement.RCSPower;
        cameraPosition[0] = HUD.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(cycleMode))
        {
            CycleMode();
        }
    }

    public void ChangeMode(int _mode)
    {
        mode = _mode;
        camera.transform.localPosition = new Vector3(0f, 0f, 0f);
        switch (_mode)
        {
            case 0:
                {
                    modeName = "Travel";
                    break;
                }
            case 1:
                {
                    modeName = "Fight";
                    break;
                }
            case 2:
                {
                    modeName = "Sniper 4x";
                    camera.transform.localPosition = new Vector3(0f, 0f, -9.5f);
                    break;
                }
            case 3:
                {
                    modeName = "Sniper 16x";
                    camera.transform.localPosition = new Vector3(0f, 0f, -47f);
                    break;
                }
            default:
                {
                    
                    break;
                }
        }
        HUD.localPosition = cameraPosition[mode];
        movement.RCSPower = sensitivity[mode];
        camera.fieldOfView = cameraFOV[mode];

        displayStats.UpdateCameraMode();
    }

    public void CycleMode()
    {
        mode++;
        if(mode > numberOfModes - 1) mode = 0;

        ChangeMode(mode);
    }
}
