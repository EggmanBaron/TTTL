using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
namespace UnityEditor
{
    [CustomEditor(typeof(RoleView))]
    public class RoleViewEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            RoleView roleView = (RoleView)target;
            DrawDefaultInspector();
            if (!roleView.gameRoles) { return; }
            List<string> roles = roleView.gameRoles.roles;
            int index = (roleView.RoleIndex == -1) ? 0 : roleView.RoleIndex;
            int selectedRoleIndex = EditorGUILayout.Popup(index, roles.ToArray());
            if (roles[selectedRoleIndex] != roleView.Role)
            {
                roleView.Role = roles[selectedRoleIndex];
                EditorUtility.SetDirty(target);
            }
        }
    }
}
