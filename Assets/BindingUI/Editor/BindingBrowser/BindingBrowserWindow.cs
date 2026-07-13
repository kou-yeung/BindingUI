#if UNITY_EDITOR

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace BindingUI.Editor
{
    public sealed class BindingBrowserWindow : EditorWindow
    {
        const float IdWidth = 150f;
        const float ComponentsWidth = 220f;
        const float CopyButtonWidth = 52f;
        const float SelectButtonWidth = 52f;
        bool showComponents = true;

        GameObject selectedRoot;
        IReadOnlyList<BindingBrowserEntry> entries =
            Array.Empty<BindingBrowserEntry>();

        Vector2 scrollPosition;

        [MenuItem("Window/BindingUI/Binding Browser")]
        static void Open()
        {
            GetWindow<BindingBrowserWindow>(
                title: "Binding Browser");
        }

        void OnEnable()
        {
            Selection.selectionChanged += HandleSelectionChanged;
            EditorApplication.hierarchyChanged += HandleHierarchyChanged;

            Refresh();
        }

        void OnDisable()
        {
            Selection.selectionChanged -= HandleSelectionChanged;
            EditorApplication.hierarchyChanged -= HandleHierarchyChanged;
        }

        void HandleSelectionChanged()
        {
            Refresh();
            Repaint();
        }

        void HandleHierarchyChanged()
        {
            Refresh();
            Repaint();
        }

        void Refresh()
        {
            selectedRoot = Selection.activeGameObject;

            entries = selectedRoot == null
                ? Array.Empty<BindingBrowserEntry>()
                : BindingBrowserResolver.Resolve(selectedRoot);
        }

        void OnGUI()
        {
            DrawToolbar();
            DrawSelectedRoot();

            EditorGUILayout.Space(4f);

            if (selectedRoot == null)
            {
                EditorGUILayout.HelpBox(
                    "Select a GameObject in the Hierarchy.",
                    MessageType.Info);

                return;
            }

            DrawSummary();

            EditorGUILayout.Space(4f);

            showComponents = EditorGUILayout.ToggleLeft("Show Components", showComponents);

            DrawHeader();
            DrawEntries();
            DrawFooter();
        }

        void DrawToolbar()
        {
            using (new EditorGUILayout.HorizontalScope(
                       EditorStyles.toolbar))
            {
                GUILayout.Label(
                    "Binding Browser",
                    EditorStyles.boldLabel);

                GUILayout.FlexibleSpace();

                if (GUILayout.Button(
                        "Refresh",
                        EditorStyles.toolbarButton))
                {
                    Refresh();
                }
            }
        }

        void DrawSelectedRoot()
        {
            using (new EditorGUI.DisabledScope(true))
            {
                EditorGUILayout.ObjectField(
                    "Selected Root",
                    selectedRoot,
                    typeof(GameObject),
                    allowSceneObjects: true);
            }
        }

        void DrawSummary()
        {
            var duplicateCount = entries.Count(
                entry => entry.IsDuplicate);

            EditorGUILayout.LabelField(
                $"Bindings: {entries.Count}");

            if (duplicateCount > 0)
            {
                EditorGUILayout.HelpBox(
                    $"{duplicateCount} entries have duplicate Binding IDs.",
                    MessageType.Error);
            }
        }

        void DrawHeader()
        {
            using (new EditorGUILayout.HorizontalScope(
                       EditorStyles.toolbar))
            {
                GUILayout.Label(
                    "Binding ID",
                    EditorStyles.boldLabel,
                    GUILayout.Width(IdWidth));

                if (showComponents)
                {
                    GUILayout.Label(
                        "Components",
                        EditorStyles.boldLabel,
                        GUILayout.Width(ComponentsWidth));
                }

                GUILayout.Label(
                    "Code",
                    EditorStyles.boldLabel);

                GUILayout.Space(
                    CopyButtonWidth + SelectButtonWidth + 8f);
            }
        }

        void DrawEntries()
        {
            scrollPosition =
                EditorGUILayout.BeginScrollView(scrollPosition);

            foreach (var entry in entries)
            {
                DrawEntry(entry);
            }

            EditorGUILayout.EndScrollView();
        }

        void DrawEntry(BindingBrowserEntry entry)
        {
            var previousBackgroundColor = GUI.backgroundColor;

            if (entry.IsDuplicate)
            {
                GUI.backgroundColor =
                    new Color(1f, 0.55f, 0.55f);
            }

            using (new EditorGUILayout.VerticalScope(
                       EditorStyles.helpBox))
            {
                using (new EditorGUILayout.HorizontalScope())
                {
                    DrawId(entry);
                    if (showComponents)
                    {
                        DrawComponents(entry);
                    }
                    DrawCode(entry);

                    if (GUILayout.Button(
                            "Copy",
                            GUILayout.Width(CopyButtonWidth)))
                    {
                        Copy(entry.BindCode);
                    }

                    //if (GUILayout.Button(
                    //        "Select",
                    //        GUILayout.Width(SelectButtonWidth)))
                    //{
                    //    Selection.activeGameObject =
                    //        entry.GameObject;
                    //    EditorGUIUtility.PingObject(
                    //        entry.GameObject);
                    //}
                }

                if (entry.IsDuplicate)
                {
                    EditorGUILayout.LabelField(
                        $"Duplicate ID — {entry.HierarchyPath}",
                        EditorStyles.miniLabel);
                }
            }

            GUI.backgroundColor = previousBackgroundColor;
        }

        static void DrawId(BindingBrowserEntry entry)
        {
            var content = new GUIContent(
                entry.Id,
                entry.HierarchyPath);

            GUILayout.Label(
                content,
                GUILayout.Width(IdWidth));
        }

        static void DrawComponents(BindingBrowserEntry entry)
        {
            var content = new GUIContent(
                entry.ComponentsText,
                entry.ComponentsText);

            GUILayout.Label(
                content,
                GUILayout.Width(ComponentsWidth));
        }

        static void DrawCode(BindingBrowserEntry entry)
        {
            EditorGUILayout.SelectableLabel(
                entry.BindCode,
                EditorStyles.textField,
                GUILayout.Height(
                    EditorGUIUtility.singleLineHeight));
        }

        void DrawFooter()
        {
            EditorGUILayout.Space(4f);

            using (new EditorGUILayout.HorizontalScope())
            {
                GUILayout.FlexibleSpace();

                using (new EditorGUI.DisabledScope(
                           entries.Count == 0))
                {
                    if (GUILayout.Button(
                            "Copy All",
                            GUILayout.Width(100f),
                            GUILayout.Height(24f)))
                    {
                        CopyAll();
                    }
                }
            }
        }

        void CopyAll()
        {
            var code = string.Join(
                Environment.NewLine,
                entries.Select(entry => entry.BindCode));

            Copy(code);
        }

        void Copy(string text)
        {
            EditorGUIUtility.systemCopyBuffer = text;

            ShowNotification(
                new GUIContent("Copied"));
        }
    }
}

#endif