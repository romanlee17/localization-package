#if TextMeshPro
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

namespace romanlee17.Localization {
    [CustomEditor(typeof(LocalizeTextMeshPro))]
    public class LocalizeTextMeshProEditor : Editor {

        private string tableSearchPrompt = default;
        private string entrySearchPrompt = default;
        private Vector2 tableScrollValue = default;
        private Vector2 entryScrollValue = default;

        private LocalizationTable[] searchTablesResult = null;
        private LocalizationTable.LocalizationEntry[] searchEntriesResult = null;

        public override void OnInspectorGUI() {
            LocalizeTextMeshPro localizeTextMeshPro = target as LocalizeTextMeshPro;

            // Detect table changes.
            EditorGUI.BeginChangeCheck();

            bool isTableEmpty = string.IsNullOrEmpty(localizeTextMeshPro.tableKey);
            bool isEntryEmpty = string.IsNullOrEmpty(localizeTextMeshPro.entryKey);

            // Display current table.
            GUILayout.BeginHorizontal();
            GUILayout.Label("Table", GUILayout.Width(240), GUILayout.ExpandHeight(true));
            GUILayout.Label(isTableEmpty ? "<not selected>" : localizeTextMeshPro.tableKey, GUI.skin.box, GUILayout.ExpandWidth(true));
            if (GUILayout.Button(EditorGUIUtility.IconContent("d_TreeEditor.Trash"),
                        GUILayout.Width(40), GUILayout.ExpandWidth(false), GUILayout.ExpandHeight(true))) {
                localizeTextMeshPro.tableKey = string.Empty;
                localizeTextMeshPro.entryKey = string.Empty;
                isTableEmpty = true;
                isEntryEmpty = true;
            }
            GUILayout.EndHorizontal();

            // Display current entry.
            GUILayout.BeginHorizontal();
            GUILayout.Label("Entry", GUILayout.Width(240), GUILayout.ExpandHeight(true));
            GUILayout.Label(isEntryEmpty ? "<not selected>" : localizeTextMeshPro.entryKey, GUI.skin.box, GUILayout.ExpandWidth(true));
            if (GUILayout.Button(EditorGUIUtility.IconContent("d_TreeEditor.Trash"),
                        GUILayout.Width(40), GUILayout.ExpandWidth(false), GUILayout.ExpandHeight(true))) {
                localizeTextMeshPro.entryKey = string.Empty;
                isEntryEmpty = true;
            }
            GUILayout.EndHorizontal();

            if (isTableEmpty) {
                // Enter table key as search prompt.
                GUILayout.BeginHorizontal();
                GUILayout.Label("Search table", GUILayout.Width(240));
                tableSearchPrompt = GUILayout.TextField(tableSearchPrompt);
                GUILayout.EndHorizontal();
                // Select table from search result.
                tableScrollValue = GUILayout.BeginScrollView(tableScrollValue, EditorStyles.helpBox, GUILayout.MinHeight(160));
                // Sort all tables and add only relevant to search prompt.
                if (string.IsNullOrEmpty(tableSearchPrompt) == false) {
                    searchTablesResult = Localization.Tables.OrderByDescending(table => table.key.SimilarityScore(tableSearchPrompt)).ToArray();
                    // Display relative tables in editor.
                    foreach (var table in searchTablesResult) {
                        if (GUILayout.Button(table.key)) {
                            localizeTextMeshPro.tableKey = table.key;
                        }
                    }
                }
                GUILayout.EndScrollView();
            }
            else if (isEntryEmpty) {
                // Enter entry key as search prompt.
                GUILayout.BeginHorizontal();
                GUILayout.Label("Search entry", GUILayout.Width(240));
                entrySearchPrompt = GUILayout.TextField(entrySearchPrompt);
                GUILayout.EndHorizontal();
                // Select entry from search result.
                entryScrollValue = GUILayout.BeginScrollView(entryScrollValue, EditorStyles.helpBox, GUILayout.MinHeight(160));
                if (string.IsNullOrEmpty(entrySearchPrompt) == false) {
                    searchEntriesResult = Localization.GetTable(localizeTextMeshPro.tableKey).entries.OrderByDescending(entry => entry.key.SimilarityScore(entrySearchPrompt)).ToArray();
                    foreach (var entry in searchEntriesResult) {
                        if (GUILayout.Button(entry.key)) {
                            localizeTextMeshPro.entryKey = entry.key;
                        }
                    }
                }
                GUILayout.EndScrollView();
                if (GUILayout.Button("Create new entry")) {
                    localizeTextMeshPro.entryKey = Localization.GetTable(localizeTextMeshPro.tableKey).CreateNewLocalizationEntry().key;
                }
            }

            if (isTableEmpty == false && isEntryEmpty == false) {
                GUILayout.Space(10);
                // Editor for selected table entry.
                GUILayout.Label("Entry editor", EditorStyles.boldLabel);
                GUILayout.BeginHorizontal();
                GUILayout.Label("Key", GUILayout.Width(240));
                LocalizationTable.LocalizationEntry activeEntry = Localization.GetTable(
                    localizeTextMeshPro.tableKey)[localizeTextMeshPro.entryKey];
                activeEntry.key = GUILayout.TextField(activeEntry.key);
                localizeTextMeshPro.entryKey = activeEntry.key;
                GUILayout.EndHorizontal();
                foreach (var language in Localization.Settings.languages) {
                    GUILayout.BeginHorizontal();
                    GUILayout.Label(language.language.ToString(), GUILayout.Width(240));
                    activeEntry[language.language] = GUILayout.TextField(activeEntry[language.language]);
                    GUILayout.EndHorizontal();
                }
            }

            GUILayout.Space(10);
            EditorGUI.BeginDisabledGroup(true);
            GUILayout.Label("Debug hidden serialized fields", EditorStyles.boldLabel);
            base.OnInspectorGUI();
            EditorGUI.EndDisabledGroup();

            // Mark as dirty if changes occured.
            if (EditorGUI.EndChangeCheck()) {
                EditorUtility.SetDirty(localizeTextMeshPro);
                // Since Unity has problems with SetDirty, mark currently active
                // scene as dirty too, so changes will be saved for sure.
                EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
            }

            // Unselect anything when clicking on window background.
            if (GUI.Button(new Rect(0, 0, Screen.width, Screen.height), "", GUIStyle.none)) {
                GUI.FocusControl(null);
            }

        }

    }
}
#endif