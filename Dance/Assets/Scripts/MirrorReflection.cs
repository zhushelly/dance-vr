using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class MirrorReflection : MonoBehaviour
{
    public Camera mainCamera;
    public Camera mirrorCamera;
    public RenderTexture mirrorTexture;
    private Renderer mirrorRenderer;

    void Start()
    {
        if (mirrorCamera == null)
        {
            GameObject mirrorCameraObj = new GameObject("Mirror Camera");
            mirrorCamera = mirrorCameraObj.AddComponent<Camera>();
            mirrorCamera.enabled = false;
        }

        mirrorRenderer = GetComponent<Renderer>();
        mirrorRenderer.material.SetTexture("_MainTex", mirrorTexture);
        mirrorCamera.targetTexture = mirrorTexture;
    }

    void LateUpdate()
    {
        if (mainCamera == null || mirrorCamera == null) return;

        Vector3 mirrorNormal = transform.up;
        Vector3 cameraToMirror = transform.position - mainCamera.transform.position;

        float distance = Vector3.Dot(mirrorNormal, cameraToMirror);
        Vector3 reflectionCameraPosition = mainCamera.transform.position - 2 * mirrorNormal * distance;
        mirrorCamera.transform.position = reflectionCameraPosition;

        Vector3 cameraDirection = mainCamera.transform.forward;
        Vector3 reflectedCameraDirection = Vector3.Reflect(cameraDirection, mirrorNormal);
        mirrorCamera.transform.rotation = Quaternion.LookRotation(reflectedCameraDirection, Vector3.up);

        Vector3 mirrorPos = transform.position;
        Vector3 mirrorNormalWorld = transform.up;

        Matrix4x4 reflectionMatrix = Matrix4x4.zero;

        reflectionMatrix.m00 = 1 - 2 * mirrorNormalWorld.x * mirrorNormalWorld.x;
        reflectionMatrix.m01 = -2 * mirrorNormalWorld.x * mirrorNormalWorld.y;
        reflectionMatrix.m02 = -2 * mirrorNormalWorld.x * mirrorNormalWorld.z;
        reflectionMatrix.m03 = 2 * Vector3.Dot(mirrorPos, mirrorNormalWorld) * mirrorNormalWorld.x;

        reflectionMatrix.m10 = -2 * mirrorNormalWorld.y * mirrorNormalWorld.x;
        reflectionMatrix.m11 = 1 - 2 * mirrorNormalWorld.y * mirrorNormalWorld.y;
        reflectionMatrix.m12 = -2 * mirrorNormalWorld.y * mirrorNormalWorld.z;
        reflectionMatrix.m13 = 2 * Vector3.Dot(mirrorPos, mirrorNormalWorld) * mirrorNormalWorld.y;

        reflectionMatrix.m20 = -2 * mirrorNormalWorld.z * mirrorNormalWorld.x;
        reflectionMatrix.m21 = -2 * mirrorNormalWorld.z * mirrorNormalWorld.y;
        reflectionMatrix.m22 = 1 - 2 * mirrorNormalWorld.z * mirrorNormalWorld.z;
        reflectionMatrix.m23 = 2 * Vector3.Dot(mirrorPos, mirrorNormalWorld) * mirrorNormalWorld.z;

        reflectionMatrix.m30 = 0;
        reflectionMatrix.m31 = 0;
        reflectionMatrix.m32 = 0;
        reflectionMatrix.m33 = 1;

        mirrorCamera.worldToCameraMatrix = mainCamera.worldToCameraMatrix * reflectionMatrix;

        mirrorCamera.projectionMatrix = mainCamera.projectionMatrix;

        mirrorCamera.Render();
    }
}
