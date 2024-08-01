using System.Collections.Generic;
using UnityEditor;
namespace Assets.Scripts.Editor
{
    [CustomEditor(typeof(RoleView))]
    public class RoleViewEditor : UnityEditor.Editor
    {
        SerializedProperty m_role;
        private void OnEnable()
        {
            m_role = serializedObject.FindProperty("m_role");
        }
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUI.BeginChangeCheck();
            RoleView roleView = (RoleView)target;
            int index = (roleView.RoleIndex == -1) ? 0 : roleView.RoleIndex;
            List<string> roles = roleView.gameRoles.roles;
            int selectedRoleIndex = EditorGUILayout.Popup(index, roles.ToArray());
            m_role.stringValue = roles[selectedRoleIndex];
            if (roles[selectedRoleIndex] != roleView.Role)
            {
                serializedObject.ApplyModifiedProperties();
                Undo.RecordObject(target, "Role Changed");
                EditorUtility.SetDirty(target);
            }
        }
    }
}
