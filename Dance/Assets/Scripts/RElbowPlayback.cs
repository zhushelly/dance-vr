using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using System.IO;

public class RElbowPlayback : MonoBehaviour {

    public string fileName = "motiondata";
    public TextMeshPro playbackText;
    public TextMeshPro debugText;

    Transform rElbowTransform; // Reference to R_Elbow transform
    bool isPlaying = false;
    int frameIndex = 0;
    List<string> motionData;

    // Add reference to the InputActionAsset
    public InputActionAsset inputActionAsset;
    private InputAction togglePlaybackAction;

    private int fileIndex = 0;  // Define fileIndex

    // Declare the VR input references
    private VRHeadInput vrHeadInput;
    private VRControllerInput vrControllerInput;

    void Start () {
        // Find the R_Elbow transform
        rElbowTransform = GetTransformByName("R_Elbow");

        if (rElbowTransform != null) {
            debugText.text = "R_Elbow transform found.";
        } else {
            debugText.text = "R_Elbow transform not found!";
        }

        // Initialize VR input components
        vrHeadInput = GetComponent<VRHeadInput>();
        vrControllerInput = GetComponent<VRControllerInput>();

        // Check if VR inputs are found
        if (vrHeadInput == null) {
            debugText.text += "\nVRHeadInput component not found!";
        }

        if (vrControllerInput == null) {
            debugText.text += "\nVRControllerInput component not found!";
        }

        LoadMotionData();

        // Set up the playback toggle action
        togglePlaybackAction = inputActionAsset.FindActionMap("GamePlay").FindAction("StartPlayback");
        togglePlaybackAction.Enable();
        togglePlaybackAction.performed += _ => TogglePlayback();
    }

    void LoadMotionData() {
        motionData = new List<string>();

        string filePath = Path.Combine(Application.persistentDataPath, fileName + "-" + fileIndex + ".txt");
        if (File.Exists(filePath)) {
            using (StreamReader reader = new StreamReader(filePath)) {
                string line;
                while ((line = reader.ReadLine()) != null) {
                    motionData.Add(line);
                }
            }
            debugText.text = "Motion data loaded: " + filePath + " with " + motionData.Count + " frames.";
        } else {
            debugText.text = "No motion file found to play at path: " + filePath;
        }
    }

    void Update () {
        if (isPlaying) {
            if (frameIndex < motionData.Count) {
                ApplyFrameData(motionData[frameIndex]);
                frameIndex++;
            } else {
                StopPlayback();
            }
        }
    }

    public void TogglePlayback() {
        Debug.Log("Toggle Playback");
        if (isPlaying) {
            StopPlayback();
        } else {
            StartPlayback();
        }
    }

    public void StartPlayback() {
        Debug.Log("Start Playback: isPlaying = " + isPlaying);
        if (motionData.Count > 0) {
            isPlaying = true;
            frameIndex = 0;

            // Update the playback text
            if (playbackText != null) {
                playbackText.text = "Playback Started";
            }
        } else {
            if (playbackText != null) {
                playbackText.text = "No Motion Data";
            }
        }

        // Disable VR inputs
        if (vrHeadInput != null) vrHeadInput.enabled = false;
        if (vrControllerInput != null) vrControllerInput.enabled = false;
        
    }

    public void StopPlayback() {
        Debug.Log("Stop Playback: isPlaying = " + isPlaying);
        isPlaying = false;

        // Update the playback text
        if (playbackText != null) {
            playbackText.text = "Playback Stopped";
        }

        // Re-enable VR inputs
        if (vrHeadInput != null) vrHeadInput.enabled = true;
        if (vrControllerInput != null) vrControllerInput.enabled = true;
    }

    void ApplyFrameData(string frameData) {
        // Split the data assuming only one rotation quaternion is present for R_Elbow
        string[] data = frameData.Split(',');

        if (data.Length != 4) { // Expecting 4 values for quaternion
            debugText.text = $"Data length mismatch. Expected: 4, Got: {data.Length}";
            return;
        }

        // Apply rotation data to R_Elbow joint
        if (rElbowTransform != null) {
            Quaternion rotation = new Quaternion(
                float.Parse(data[0]),
                float.Parse(data[1]),
                float.Parse(data[2]),
                float.Parse(data[3])
            );
            rElbowTransform.rotation = rotation;
            debugText.text = $"R_Elbow rotation applied: {rotation}";
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
}
