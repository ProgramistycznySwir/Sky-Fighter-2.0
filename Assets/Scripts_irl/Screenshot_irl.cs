using UnityEngine;

public class Screenshot_irl : MonoBehaviour
{
    public KeyCode takeScreenshot = KeyCode.SysReq;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(takeScreenshot))
        {
            string date = System.DateTime.Now.ToString();
            date = date.Replace("/", "-");
            date = date.Replace(":", "-");

            string fileName = Application.dataPath + "/Screenshots/screenshot_" + date + ".png";
            
            ScreenCapture.CaptureScreenshot(fileName);
            
            Debug.Log("Screenshot at: " + fileName);
        }
    }
}
