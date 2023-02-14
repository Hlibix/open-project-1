using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WaypointData
{
    public Vector3       waypoint;
    public List<Vector3> corners;
}

[CreateAssetMenu(fileName = "PathwayConfig", menuName = "EntityConfig/Pathway Config")]
public class PathwayConfigSO : NPCMovementConfigSO
{
    [HideInInspector]
    public List<WaypointData> Waypoints;

#if UNITY_EDITOR

    [SerializeField]
    private Color _lineColor = Color.black;

    [SerializeField]
    [Range(0, 100)]
    private int _textSize = 20;

    [SerializeField]
    private Color _textColor = Color.white;

    [SerializeField]
    [Range(0, 100)]
    [Tooltip("This function may reduce the frame rate if a large probe radius is specified. To avoid frame rate issues," +
        " it is recommended that you specify a max distance of twice the agent height.")]
    private float _probeRadius = 3;

    [HideInInspector]
    public bool DisplayProbes;

    [HideInInspector]
    public bool ToggledNavMeshDisplay;

    public const string FIELD_LABEL = "Point ";
    public const string TITLE_LABEL = "Waypoints";

    public Color         LineColor   => _lineColor;
    public Color         TextColor   => _textColor;
    public int           TextSize    => _textSize;
    public float         ProbeRadius => _probeRadius;
    public List<Vector3> Path        { get; set; }

    public List<bool> Hits { get; set; }

    public bool RealTimeEnabled;

#endif
}