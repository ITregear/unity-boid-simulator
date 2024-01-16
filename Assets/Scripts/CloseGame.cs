using UnityEngine;

public class CloseGame : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        // Check if the Control key and C key are both being pressed
        if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                // Quit the application
                #if UNITY_EDITOR
                // If we're running in the Unity Editor, stop playing the scene
                UnityEditor.EditorApplication.isPlaying = false;
                #else
                // If we're running in a build, quit the application
                Application.Quit();
                #endif
            }
        }
    }
}
