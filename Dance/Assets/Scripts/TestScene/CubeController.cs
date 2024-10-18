using UnityEngine;
using UnityEngine.XR;

public class CubeController : MonoBehaviour
{
    public GameObject cube;
    public XRNode leftController;
    public XRNode rightController;

    private InputDevice leftDevice;
    private InputDevice rightDevice;

    void Start()
    {
        leftDevice = InputDevices.GetDeviceAtXRNode(leftController);
        rightDevice = InputDevices.GetDeviceAtXRNode(rightController);
    }

    void Update()
    {
        // Move and rotate cube based on controller input
        if (leftDevice.TryGetFeatureValue(CommonUsages.devicePosition, out Vector3 leftPosition))
        {
            cube.transform.position += leftPosition * Time.deltaTime;
        }

        if (rightDevice.TryGetFeatureValue(CommonUsages.deviceRotation, out Quaternion rightRotation))
        {
            cube.transform.rotation = rightRotation;
        }
    }
}
