using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
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

    // XR controller reference for haptic feedback
    public XRBaseController leftController;

    void Start () {
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

            foreach (var recorder in recordObjs) {
                WriteTransformDataToText(recorder);
            }

            if (recordBlendShape) {
                foreach (var recorder in blendShapeRecorders) {
                    // Handle blendshape recording if needed
                }
            }
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

        // Trigger haptic feedback on recording start
        SendHapticFeedback(0.5f, 0.2f); // Adjust intensity and duration as needed
    }

    public void StopRecording () {
        Debug.Log("End Record, data saved to file");
        isRecording = false;
        writer.Close();
        fileIndex++;

        // Trigger haptic feedback on recording stop
        SendHapticFeedback(0.3f, 0.2f); // Adjust intensity and duration as needed
    }

    void WriteTransformDataToText(Transform recorder) {
        if (writer != null) {
            string data = $"{recorder.name},{recorder.position.x},{recorder.position.y},{recorder.position.z}," +
                          $"{recorder.rotation.x},{recorder.rotation.y},{recorder.rotation.z},{recorder.rotation.w}";
            writer.WriteLine(data);
        }
    }

    void OnApplicationQuit() {
        if (writer != null) {
            writer.Close();
        }
    }

    void SendHapticFeedback(float intensity, float duration) {
        if (leftController != null) {
            leftController.SendHapticImpulse(intensity, duration);
        }
    }
}
