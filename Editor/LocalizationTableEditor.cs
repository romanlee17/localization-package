using UnityEngine;
using UnityEditor;

namespace romanlee17.Utils {
    [CustomEditor(typeof(LocalizationTable))]
    public class LocalizationTableEditor : Editor {

        const int entryKeyWidth = 240;
        const int entryValueWidth = 240;
        const int entryButtonWidth = 40;
        const int entryIndexWidth = 20;

        GUIStyle TextAreaWrap {
            get => textAreaWrap ??= new(EditorStyles.textField) {
                wordWrap = true
            };
        }
        GUIStyle textAreaWrap = null;

        public override void OnInspectorGUI() {

            LocalizationTable table = (LocalizationTable)target;

            // Detect table changes.
            EditorGUI.BeginChangeCheck();

            GUILayout.BeginHorizontal();
            GUILayout.Label("Table key", EditorStyles.boldLabel, GUILayout.ExpandWidth(false));
            table.key = EditorGUILayout.TextField(table.key);
            GUILayout.EndHorizontal();
            if (string.IsNullOrWhiteSpace(table.key)) {
                EditorGUILayout.HelpBox("Table key can't be empty.", MessageType.Error);
            }
            GUILayout.Space(20);
            GUILayout.BeginHorizontal();
            GUILayout.Label("#", EditorStyles.boldLabel,
                   GUILayout.Width(entryIndexWidth), GUILayout.ExpandWidth(false));
            GUILayout.Label("Entries", EditorStyles.boldLabel,
                   GUILayout.MinWidth(entryKeyWidth), GUILayout.ExpandWidth(false));
            foreach (var language in Localization.Settings.languages) {
                GUILayout.Label(language.ToString(), EditorStyles.boldLabel,
                    GUILayout.Width(entryValueWidth), GUILayout.ExpandWidth(true));
            }
            GUILayout.Space(entryButtonWidth);
            GUILayout.EndHorizontal();

            if (table.entries != null && table.entries.Count > 0) {
                // Display all entries of the table.
                for (int x = 0; x < table.entries.Count; x++) {
                    GUILayout.BeginHorizontal();
                    // Display current element index.
                    GUILayout.Label($"{x}", GUILayout.ExpandWidth(false), GUILayout.Width(entryIndexWidth));
                    // Display entry keys.
                    table.entries[x].key = EditorGUILayout.TextField(table.entries[x].key,
                        GUILayout.Width(entryKeyWidth), GUILayout.ExpandWidth(false));
                    foreach (var language in Localization.Settings.languages) {
                        table.entries[x][language] = EditorGUILayout.TextArea(table.entries[x][language], TextAreaWrap,
                            GUILayout.Width(entryValueWidth), GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));
                    }
                    if (GUILayout.Button(EditorGUIUtility.IconContent("d_TreeEditor.Trash"),
                        GUILayout.Width(entryButtonWidth), GUILayout.ExpandWidth(false), GUILayout.ExpandHeight(true))) {
                        table.entries.RemoveAt(x);
                    }
                    GUILayout.EndHorizontal();
                }
            }
            // Add new entries by button.
            if (GUILayout.Button("New Entry")) {
                table.entries.Add(new());
            }
            // Mark as dirty if changes occured.
            if (EditorGUI.EndChangeCheck()) {
                EditorUtility.SetDirty(table);
            }
            
            // Unselect anything when clicking on window background.
            if (GUI.Button(new Rect(0, 0, Screen.width, Screen.height), "", GUIStyle.none)) {
                GUI.FocusControl(null);
            }

        }

    }
}