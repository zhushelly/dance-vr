using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class AnimationRecorder : MonoBehaviour {

    // save file path
    public string savePath;
    public string fileName = "motiondata";

    // use it when saving multiple files
    int fileIndex = 0;

    // Input Action asset reference
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

    // Use this for initialization
    void Start () {
        SetupRecorders ();

        // Find action in the input action asset
        toggleRecordingAction = inputActionAsset.FindActionMap("GamePlay").FindAction("StartRecording");

        // Enable action
        toggleRecordingAction.Enable();

        // Bind action to method
        toggleRecordingAction.performed += _ => ToggleRecording();
    }

    void SetupRecorders () {
        recordObjs = gameObject.GetComponentsInChildren<Transform>();
        blendShapeRecorders = new List<SkinnedMeshRenderer>();

        frameIndex = 0;
        nowTime = 0.0f;

        for (int i = 0; i < recordObjs.Length; i++) {
            string path = AnimationRecorderHelper.GetTransformPathName(transform, recordObjs[i]);

            // check if there’s blendShape
            if (recordBlendShape) {
                SkinnedMeshRenderer tempSkinMeshRenderer = recordObjs[i].GetComponent<SkinnedMeshRenderer>();

                // if blendShape exists
                if (tempSkinMeshRenderer != null && tempSkinMeshRenderer.sharedMesh.blendShapeCount > 0) {
                    blendShapeRecorders.Add(tempSkinMeshRenderer);
                }
            }
        }
    }

    // Update is called once per frame
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
        string filePath = Path.Combine(savePath, fileName + "-" + fileIndex + ".txt");
        writer = new StreamWriter(filePath, false);
    }

    public void StopRecording () {
        Debug.Log("End Record, data saved to file");
        isRecording = false;
        writer.Close();
        fileIndex++;
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
}
