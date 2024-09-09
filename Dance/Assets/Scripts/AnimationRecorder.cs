// AnimationRecorder.cs

using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;  
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class AnimationRecorder : MonoBehaviour {

    public string fileName = "motiondata";
    int fileIndex = 0;

    public InputActionAsset inputActionAsset;
    private InputAction toggleRecordingAction;

    public bool recordLimitedFrames = false;
    public int recordFrames = 1000;
    int frameIndex = 0;

    public bool recordBlendShape = false;

    Transform[] recordObjs;
    SkinnedMeshRenderer[] blendShapeObjs;
    List<SkinnedMeshRenderer> blendShapeRecorders;

    bool isRecording = false;
    float nowTime = 0.0f;

    StreamWriter writer;

    // Add a public reference to the TextMeshPro component
    public TextMeshPro recordingText;

    // List of joint names in the order as per the output format
    private readonly string[] jointOrder = new string[] {
        "Pelvis", "R_Hip", "L_Hip", "Spine1", "R_Knee", "L_Knee", "Spine2", "R_Ankle", "L_Ankle", 
        "Spine3", "R_Foot", "L_Foot", "Neck", "R_Collar", "L_Collar", "Head", "R_Shoulder", 
        "L_Shoulder", "R_Elbow", "L_Elbow", "R_Wrist", "L_Wrist", "Head_End", "R_Foot_End", "L_Foot_End", 
        "R_Hand", "L_Hand"
    };

    private readonly string[] rotationJointNames = new string[] {
        "Pelvis", "R_Hip", "L_Hip", "Spine1", "R_Knee", "L_Knee", "Spine2", "R_Ankle", "L_Ankle", 
        "Spine3", "R_Foot", "L_Foot", "Neck", "R_Collar", "L_Collar", "Head", "R_Shoulder", 
        "L_Shoulder", "R_Elbow", "L_Elbow", "R_Wrist", "L_Wrist"
    };

    void Start () {
        // Application.targetFrameRate = 30;

        SetupRecorders ();

        toggleRecordingAction = inputActionAsset.FindActionMap("GamePlay").FindAction("StartRecording");
        toggleRecordingAction.Enable();
        toggleRecordingAction.performed += _ => ToggleRecording();
    }

    void SetupRecorders () {
        recordObjs = gameObject.GetComponentsInChildren<Transform>();
        blendShapeRecorders = new List<SkinnedMeshRenderer>();

        frameIndex = 0;
        nowTime = 0.0f;

        for (int i = 0; i < recordObjs.Length; i++) {
            string path = AnimationRecorderHelper.GetTransformPathName(transform, recordObjs[i]);

            if (recordBlendShape) {
                SkinnedMeshRenderer tempSkinMeshRenderer = recordObjs[i].GetComponent<SkinnedMeshRenderer>();

                if (tempSkinMeshRenderer != null && tempSkinMeshRenderer.sharedMesh.blendShapeCount > 0) {
                    blendShapeRecorders.Add(tempSkinMeshRenderer);
                }
            }
        }
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
        if (writer != null) {
            string frameData = "";

            // Write rotations (quaternions) for the first 22 joints
            foreach (var jointName in rotationJointNames) {
                Transform jointTransform = GetTransformByName(jointName);
                if (jointTransform != null) {
                    frameData += $"{jointTransform.rotation.x},{jointTransform.rotation.y},{jointTransform.rotation.z},{jointTransform.rotation.w},";
                } else {
                    frameData += "0,0,0,0,"; // Fallback in case of missing joint
                }
            }

            // Write positions (vector3) for all 27 joints
            foreach (var jointName in jointOrder) {
                Transform jointTransform = GetTransformByName(jointName);
                if (jointTransform != null) {
                    frameData += $"{jointTransform.position.x},{jointTransform.position.y},{jointTransform.position.z},";
                } else {
                    frameData += "0,0,0,"; // Fallback in case of missing joint
                }
            }

            // Remove the last comma and write the data to the file
            frameData = frameData.TrimEnd(',');
            writer.WriteLine(frameData);
        }
    }

    Transform GetTransformByName(string name) {
        foreach (var transform in recordObjs) {
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