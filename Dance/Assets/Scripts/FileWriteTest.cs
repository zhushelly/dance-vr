using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;

public class FileWriteTest : MonoBehaviour
{
    private TestFileWrite inputActions; // Your custom input actions class
    private string filePath;

    void Awake()
    {
        // Initialize the input actions
        inputActions = new TestFileWrite();
    }

    void Start()
    {
        // Set the file path to the persistent data path of the application
        filePath = Path.Combine(Application.persistentDataPath, "testFile.txt");
    }

    void OnEnable()
    {
        // Enable the input actions and subscribe to the Write action's performed event
        inputActions.Control.Write.Enable();
        inputActions.Control.Write.performed += WriteToFile;
    }

    void OnDisable()
    {
        // Unsubscribe and disable input actions
        inputActions.Control.Write.performed -= WriteToFile;
        inputActions.Control.Write.Disable();
    }

    private void WriteToFile(InputAction.CallbackContext context)
    {
        // The data you want to write
        string data = "This is a test message written at " + System.DateTime.Now;

        try
        {
            // Write the data to the file
            File.AppendAllText(filePath, data + "\n");
            Debug.Log("Data written to: " + filePath);
        }
        catch (System.Exception e)
        {
            Debug.LogError("Failed to write to file: " + e.Message);
        }
    }
}
