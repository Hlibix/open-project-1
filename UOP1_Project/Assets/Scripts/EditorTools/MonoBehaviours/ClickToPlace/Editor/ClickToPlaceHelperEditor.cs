using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ClickToPlaceHelper))]
public class ClickToPlaceHelperEditor : Editor
{
    private ClickToPlaceHelper _clickHelper => target as ClickToPlaceHelper;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Place at Mouse cursor") && !_clickHelper.IsTargeting)
        {
            _clickHelper.BeginTargeting();
            SceneView.duringSceneGui += DuringSceneGui;
        }
    }

    private void DuringSceneGui(SceneView sceneView)
    {
        var currentGUIEvent = Event.current;

        Vector3 mousePos       = currentGUIEvent.mousePosition;
        var     pixelsPerPoint = EditorGUIUtility.pixelsPerPoint;
        mousePos.y =  sceneView.camera.pixelHeight - mousePos.y * pixelsPerPoint;
        mousePos.x *= pixelsPerPoint;

        var ray = sceneView.camera.ScreenPointToRay(mousePos);

        if (Physics.Raycast(ray, out var hit))
        {
            _clickHelper.UpdateTargeting(hit.point);
        }

        switch (currentGUIEvent.type)
        {
            case EventType.MouseMove:
                HandleUtility.Repaint();
                break;
            case EventType.MouseDown:
                if (currentGUIEvent.button == 0) // Wait for Left mouse button down
                {
                    _clickHelper.EndTargeting();
                    SceneView.duringSceneGui -= DuringSceneGui;
                    currentGUIEvent.Use(); // This consumes the event, so that other controls/buttons won't be able to use it
                }

                break;
        }
    }
}