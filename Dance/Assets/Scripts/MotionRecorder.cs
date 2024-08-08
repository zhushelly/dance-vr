using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class MotionRecorder : MonoBehaviour
{
    public Transform leftController;
    public Transform rightController;
    public Transform hmd;
    private List<string> motionData = new List<string>();
    private bool isRecording = false;
    private float timer = 0f;
    
    void Update()
    {
        if (isRecording)
        {
            timer += Time.deltaTime;
            string data = $"{timer},{leftController.position.x},{leftController.position.y},{leftController.position.z}," +
                          $"{leftController.rotation.x},{leftController.rotation.y},{leftController.rotation.z},{leftController.rotation.w}," +
                          $"{rightController.position.x},{rightController.position.y},{rightController.position.z}," +
                          $"{rightController.rotation.x},{rightController.rotation.y},{rightController.rotation.z},{rightController.rotation.w}," +
                          $"{hmd.position.x},{hmd.position.y},{hmd.position.z}," +
                          $"{hmd.rotation.x},{hmd.rotation.y},{hmd.rotation.z},{hmd.rotation.w}";
            motionData.Add(data);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartRecording();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            StopRecording();
            SaveDataToFile();
        }
    }
    public void StartRecording()
    {
        isRecording = true;
        motionData.Clear();
        timer = 0f;
    }
    public void StopRecording()
    {
        isRecording = false;
    }
    private void SaveDataToFile()
    {
        string path = Path.Combine(Application.persistentDataPath, "motionData.txt");
        File.WriteAllLines(path, motionData.ToArray());
        Debug.Log("Data saved to " + path);
    }
}
