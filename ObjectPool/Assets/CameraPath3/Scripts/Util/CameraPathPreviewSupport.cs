using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class CameraPathPreviewSupport 
{
    public static bool previewSupported
    {
        get
        {
#if UNITY_EDITOR
            if (!SystemInfo.supportsRenderTextures) return false;
#endif
            return true;
        }
    }
    public static string previewSupportedText
    {
        get
        {
#if UNITY_EDITOR
            if (!SystemInfo.supportsRenderTextures) return "Render Textures is not support now";
#endif
            return "";
        }
    }
    
#if UNITY_EDITOR
    public static void RenderPreview(CameraPath path, CameraPathAnimator animator, float percent)//, float viewSize)
    {
        if (path.realNumberOfPoints < 2)
            return;
        if (!previewSupported || path.editorPreview == null)
            return;

        //Get animation values and apply them to the preview camera
        Vector3 position = path.GetPathPosition(percent);
        Quaternion rotation = animator.GetAnimatedOrientation(percent, false);

        EditorGUILayout.Space();
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Preview");
        string showPreviewButtonLabel = (path.showPreview) ? "hide" : "show";
        if (GUILayout.Button(showPreviewButtonLabel, GUILayout.Width(50)))
            path.showPreview = !path.showPreview;
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.Space();

        if (!path.enablePreviews || !path.showPreview)
            return;

        GameObject editorPreview = path.editorPreview;
        if (previewSupported && !EditorApplication.isPlaying)
        {
            int width = Mathf.Clamp(path.previewResolution, 1, 1024);
            int height = Mathf.Clamp(Mathf.RoundToInt(path.previewResolution / path.aspect),1,1024);
            RenderTexture rt = RenderTexture.GetTemporary( width, height, 24, RenderTextureFormat.Default, RenderTextureReadWrite.sRGB, 1);

            editorPreview.SetActive(true);
            editorPreview.transform.position = position;
            editorPreview.transform.rotation = rotation;

            Camera previewCam = editorPreview.GetComponent<Camera>();
            previewCam.enabled = true;
            if(path.fovList.listEnabled)
            {
                if (previewCam.orthographic)
                    previewCam.orthographicSize = path.GetPathOrthographicSize(percent);
                else
                    previewCam.fieldOfView = path.GetPathFOV(percent);
            }
            else
            {
                previewCam.fieldOfView = 60;
            }
            previewCam.farClipPlane = path.drawDistance;

            previewCam.targetTexture = rt;
            previewCam.Render();
            previewCam.targetTexture = null;
            previewCam.enabled = false;
            editorPreview.SetActive(false);

            GUILayout.Label("", GUILayout.Width(400), GUILayout.Height(path.displayHeight));
            Rect guiRect = GUILayoutUtility.GetLastRect();
            GUI.DrawTexture(guiRect, rt, ScaleMode.ScaleToFit, false);
            RenderTexture.ReleaseTemporary(rt);
        }
        else
        {
            string errorMsg = (!previewSupported) ? previewSupportedText : "No Preview When Playing.";
            EditorGUILayout.LabelField(errorMsg, GUILayout.Height(path.displayHeight));
        }
        EditorGUILayout.Space();
    }

    public static Camera GetSceneCamera()
    {
        Camera[] cams = Camera.allCameras;
        bool sceneHasCamera = cams.Length > 0;
        Camera sceneCamera = null;
        if (Camera.main)
        {
            sceneCamera = Camera.main;
        }
        else if (sceneHasCamera)
        {
            sceneCamera = cams[0];
        }

        return sceneCamera;
    }
#endif

//
//        EditorGUILayout.BeginHorizontal();
//        EditorGUILayout.LabelField("Preview");
//        string showPreviewButtonLabel = (_animator.showPreview) ? "hide" : "show";
//        if (GUILayout.Button(showPreviewButtonLabel, GUILayout.Width(74)))
//            _animator.showPreview = !_animator.showPreview;
//        EditorGUILayout.EndHorizontal();
//
//        if (!_cameraPath.enablePreviews || !_animator.showPreview)
//            return;
//
//        GameObject editorPreview = _animator.editorPreview;
//        if (CameraPathPreviewSupport.previewSupported && !EditorApplication.isPlaying)
//        {
//            RenderTexture rt = RenderTexture.GetTemporary(PREVIEW_RESOLUTION, Mathf.RoundToInt(PREVIEW_RESOLUTION / _cameraPath.aspect), 24, RenderTextureFormat.Default, RenderTextureReadWrite.Default, 1);
//
//            editorPreview.SetActive(true);
//            editorPreview.transform.position = _previewCamPos;
//            editorPreview.transform.rotation = _previewCamRot;
//
//            Camera previewCam = editorPreview.GetComponent<Camera>();
//            if(previewCam.orthographic)
//                previewCam.orthographicSize = _cameraPath.GetPathOrthographicSize(_animator.editorPercentage);
//            else
//                previewCam.fieldOfView = _cameraPath.GetPathFOV(_animator.editorPercentage);
//
//            previewCam.enabled = true;
//            previewCam.targetTexture = rt;
//            previewCam.Render();
//            previewCam.targetTexture = null;
//            previewCam.enabled = false;
//            editorPreview.SetActive(false);
//
//            GUILayout.Label("", GUILayout.Width(400), GUILayout.Height(225));
//            Rect guiRect = GUILayoutUtility.GetLastRect();
//            GUI.DrawTexture(guiRect, rt, ScaleMode.ScaleToFit, false);
//            RenderTexture.ReleaseTemporary(rt);
//        }
//        else
//        {
//            string errorMsg = (!CameraPathPreviewSupport.previewSupported) ? CameraPathPreviewSupport.previewSupportedText : "No Preview When Playing.";
//            EditorGUILayout.LabelField(errorMsg, GUILayout.Height(225));
//        }
//    }
}
