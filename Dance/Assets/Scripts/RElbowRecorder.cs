using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;  
using System.Collections.Generic;
using System.IO;

public class RElbowRecorder : MonoBehaviour {

    public string fileName = "motiondata";
    int fileIndex = 0;

    public InputActionAsset inputActionAsset;
    private InputAction toggleRecordingAction;

    public bool recordLimitedFrames = false;
    public int recordFrames = 1000;
    int frameIndex = 0;

    public bool recordBlendShape = false;

    Transform rElbowTransform; // Reference to R_Elbow transform

    bool isRecording = false;
    float nowTime = 0.0f;

    StreamWriter writer;

    // Add a public reference to the TextMeshPro component
    public TextMeshPro recordingText;

    void Start () {
        // Application.targetFrameRate = 30;

        SetupRecorder();

        toggleRecordingAction = inputActionAsset.FindActionMap("GamePlay").FindAction("StartRecording");
        toggleRecordingAction.Enable();
        toggleRecordingAction.performed += _ => ToggleRecording();
    }

    void SetupRecorder () {
        // Find the R_Elbow transform
        rElbowTransform = GetTransformByName("R_Elbow");

        frameIndex = 0;
        nowTime = 0.0f;
    }

    void Update () {
        if (isRecording) {
            nowTime += Time.deltaTime;
            WriteFrameDataToText();
        }
    }

    public void ToggleRecording() {
        if (isRecording) {
            StopRecording();
        } else {
            StartRecording();
        }
    }

    public void StartRecording () {
        Debug.Log("Start Recorder");
        isRecording = true;
        string filePath = Path.Combine(Application.persistentDataPath, fileName + "-" + fileIndex + ".txt");
        writer = new StreamWriter(filePath, false);
        
        // Update the recording text
        if (recordingText != null) {
            recordingText.text = "Recording Started";
        }
    }

    public void StopRecording () {
        Debug.Log("End Record, data saved to file");
        isRecording = false;
        writer.Close();
        fileIndex++;
        
        // Update the recording text
        if (recordingText != null) {
            recordingText.text = "Recording Stopped";
        }
    }

    void WriteFrameDataToText() {
        if (writer != null && rElbowTransform != null) {
            // Write only the rotation (quaternion) of the R_Elbow joint
            Quaternion rotation = rElbowTransform.rotation;
            string frameData = $"{rotation.x},{rotation.y},{rotation.z},{rotation.w}";
            
            writer.WriteLine(frameData);
        }
    }

    Transform GetTransformByName(string name) {
        Transform[] transforms = gameObject.GetComponentsInChildren<Transform>();
        foreach (var transform in transforms) {
            if (transform.name == name) {
                return transform;
            }
        }
        return null;
    }

    void OnApplicationQuit() {
        if (writer != null) {
            writer.Close();
        }
    }
}
