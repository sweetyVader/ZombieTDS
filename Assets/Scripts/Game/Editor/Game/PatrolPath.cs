using TDS.Game.Enemy;
using UnityEditor;

namespace TDS.Game
{
    [CustomEditor(typeof(PatrolPath))]
    public class PatrolPathEditor : Editor
    {
        [DrawGizmo(GizmoType.Selected | GizmoType.Active)]
        private static void DrawGizmoForMyScript(PatrolPath path, GizmoType gizmoType)
        {
        }
    }
}