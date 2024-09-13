// AnimationPlayback.cs

using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class AnimationPlayback : MonoBehaviour {

    public string fileName = "motiondata";
    public TextMeshPro playbackText;
    public TextMeshPro debugText;

    Transform[] playbackObjs;
    bool isPlaying = false;
    int frameIndex = 0;
    List<string> motionData;

    // Add reference to the InputActionAsset
    public InputActionAsset inputActionAsset;
    private InputAction togglePlaybackAction;

    private int fileIndex = 0;  // Define fileIndex
    
    private readonly string[] rotationJointNames = new string[] {
        "Pelvis", "R_Hip", "L_Hip", "Spine1", "R_Knee", "L_Knee", "Spine2", "R_Ankle", "L_Ankle", 
        "Spine3", "R_Foot", "L_Foot", "Neck", "R_Collar", "L_Collar", "Head", "R_Shoulder", 
        "L_Shoulder", "R_Elbow", "L_Elbow", "R_Wrist", "L_Wrist"
    };

    private readonly string[] jointOrder = new string[] {
        "Pelvis", "R_Hip", "L_Hip", "Spine1", "R_Knee", "L_Knee", "Spine2", "R_Ankle", "L_Ankle", 
        "Spine3", "R_Foot", "L_Foot", "Neck", "R_Collar", "L_Collar", "Head", "R_Shoulder", 
        "L_Shoulder", "R_Elbow", "L_Elbow", "R_Wrist", "L_Wrist", "Head_End", "R_Foot_End", 
        "L_Foot_End", "R_Hand", "L_Hand"
    };

    private VRHeadInput vrHeadInput;
    private VRControllerInput vrControllerInput;

    void Start () {
        playbackObjs = gameObject.GetComponentsInChildren<Transform>();

        foreach (var obj in playbackObjs) {
            debugText.text = "Transform found: " + obj.name;
        }

        vrHeadInput = GetComponent<VRHeadInput>();
        vrControllerInput = GetComponent<VRControllerInput>();

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
            debugText.text = "Motion data loaded: " + motionData.Count + " frames.";
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

            // Disable VR inputs
            if (vrHeadInput != null) vrHeadInput.enabled = false;
            if (vrControllerInput != null) vrControllerInput.enabled = false;
        } else {
            if (playbackText != null) {
                playbackText.text = "No Motion Data";
            }
        }
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
        string[] data = frameData.Split(',');

        int dataIndex = 0;

        // Apply rotation data
        foreach (var jointName in rotationJointNames) {
            Transform jointTransform = GetTransformByName(jointName);
            if (jointTransform != null) {
                jointTransform.rotation = new Quaternion(
                    float.Parse(data[dataIndex++]),
                    float.Parse(data[dataIndex++]),
                    float.Parse(data[dataIndex++]),
                    float.Parse(data[dataIndex++])
                );
                jointTransform.rotation = rotation;
                debugText.text = $"{jointName} rotation applied: {rotation}";
            } else {
                dataIndex += 4; // Skip the quaternion values if joint not found
            }
        }

        // Apply position data
        foreach (var jointName in jointOrder) {
            Transform jointTransform = GetTransformByName(jointName);
            if (jointTransform != null) {
                jointTransform.position = new Vector3(
                    float.Parse(data[dataIndex++]),
                    float.Parse(data[dataIndex++]),
                    float.Parse(data[dataIndex++])
                );
                jointTransform.position = position;
                debugText.text = $"{jointName} position applied: {position}";
            } else {
                dataIndex += 3; // Skip the vector3 values if joint not found
            }
        }
    }

    Transform GetTransformByName(string name) {
        foreach (var transform in playbackObjs) {
            if (transform.name == name) {
                return transform;
            }
        }
        return null;
    }
}