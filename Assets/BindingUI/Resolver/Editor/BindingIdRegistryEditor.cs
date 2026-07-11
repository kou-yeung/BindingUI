using System;
using UnityEditor;
using UnityEngine;

namespace BindingUI.Editor
{
    [CustomEditor(typeof(BindingIdRegistry))]
    public sealed class BindingIdRegistryEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            DrawDefaultInspector();

            EditorGUILayout.Space();

            if (GUILayout.Button("Refresh Targets"))
            {
                RefreshTargets();
            }

            serializedObject.ApplyModifiedProperties();
        }

        void RefreshTargets()
        {
            var registry = (BindingIdRegistry)target;

            Undo.RecordObject(
                registry,
                "Refresh Binding Targets");

            try
            {
                registry.RefreshTargets();

                EditorUtility.SetDirty(registry);

                PrefabUtility
                    .RecordPrefabInstancePropertyModifications(
                        registry);

                serializedObject.Update();

                Debug.Log(
                    $"Binding targets refreshed: " +
                    $"'{registry.name}'.",
                    registry);
            }
            catch (Exception exception)
            {
                Debug.LogException(
                    exception,
                    registry);
            }
        }
    }
}