using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ObliqueProjection : MonoBehaviour
{
    public Camera camera;
    private void OnPreCull(){
        // First calculate the regular worldToCameraMatrix.
        // Start with transform.worldToLocalMatrix.
        var m = camera.transform.worldToLocalMatrix;
        // Then, since Unity uses OpenGL's view matrix conventions
        // we have to flip the z-value.
        print("worldToLocalMatrix:\n" + m);
        m.SetRow(2, -m.GetRow(2));

        // Now for the custom projection.
        // Set the world's up vector to always align with the camera's up vector.
        // Add a small amount of the original up vector to
        // ensure the matrix will be invertible.
        // Try changing the vector to see what other projections you can get.
        m.SetColumn(2, new Vector4(0, 0.5f, 0, 0));

        camera.worldToCameraMatrix = m;
    }
    private void OnEnable() {
        // Optional, only enable the callbacks when in the editor.
        if (Application.isEditor) {
            // These callbacks are invoked for all cameras including
            // the scene view and camera previews.
            Camera.onPreCull += ScenePreCull;
            Camera.onPostRender += ScenePostRender;
        }
    }

    private void OnDisable() {
        if (Application.isEditor) {
            Camera.onPreCull -= ScenePreCull;
            Camera.onPostRender -= ScenePostRender;
        }
    }

    private void ScenePreCull(Camera cam) {
        // If the camera is the scene view camera, call our pre cull method.
        if (cam.cameraType == CameraType.SceneView) OnPreCull();
    }

    private void ScenePostRender(Camera cam)
    {
        // Unity's gizmos don't like it when you change the worldToCameraMatrix.
        // The workaround is to reset it after rendering.
        if (cam.cameraType == CameraType.SceneView) cam.ResetWorldToCameraMatrix();
    }


    //用户输入坐标的转化
    public static Matrix4x4 ScreenToWorldMatrix(Camera cam) {
        // Make a matrix that converts from
        // screen coordinates to clip coordinates.
        var rect = cam.pixelRect;
        var viewportMatrix = Matrix4x4.Ortho(rect.xMin, rect.xMax, rect.yMin, rect.yMax, -1, 1);

        // The camera's view-projection matrix converts from world coordinates to clip coordinates.
        var vpMatrix = cam.projectionMatrix * cam.worldToCameraMatrix;

        // Setting column 2 (z-axis) to identity makes the matrix ignore the z-axis.
        // Instead you get the value on the xy plane!
        vpMatrix.SetColumn(2, new Vector4(0, 0, 1, 0));

        // Going from right to left:
        // convert screen coords to clip coords, then clip coords to world coords.
        return vpMatrix.inverse * viewportMatrix;
    }
    public Vector2 ScreenToWorldPoint(Vector2 point)
    {
        return ScreenToWorldMatrix(camera).MultiplyPoint(point);
    }
}
