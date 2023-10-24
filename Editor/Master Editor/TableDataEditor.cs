namespace romanlee17.LocalizationEditor {
    using UnityEngine;
    using UnityEditor;
    using UnityEditor.SceneManagement;
    using UnityEngine.SceneManagement;
    using romanlee17.Localization.Master;

    [CustomEditor(typeof(TableData))]
    public class TableDataEditor : Editor {

        private const int _keyWidth = 240;
        private const int _valueWidth = 240;
        private const int _buttonWidth = 40;
        private const int _indexWidth = 40;
        private const int _space = 10;
        private const int _indent = 1;

        private GUIStyle Wrapper {
            get {
                return new(EditorStyles.textField) {
                    wordWrap = true
                };
            }
        }

        private Vector2 _scrollPosition = default;

        public override void OnInspectorGUI() {
            // Detect table changes.
            EditorGUI.BeginChangeCheck();

            TableData tableData = (TableData)target;

            // Editor field for table key.
            GUILayout.BeginHorizontal();
            GUILayout.Label("Table key");
            EditorGUI.indentLevel += _indent;
            tableData.Key = EditorGUILayout.TextField(tableData.Key);
            EditorGUI.indentLevel -= _indent;
            GUILayout.EndHorizontal();

            // Check if table key is empty or white space.
            if (string.IsNullOrWhiteSpace(tableData.Key)) {
                EditorGUILayout.HelpBox("Table key can't be empty.", MessageType.Error);
                return;
            }

            _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition);

            // Header part.
            GUILayout.BeginHorizontal();
            GUILayout.Label("Index", GUILayout.Width(_indexWidth), GUILayout.ExpandWidth(false));
            GUILayout.Label("Key", GUILayout.Width(_keyWidth), GUILayout.ExpandWidth(false));
            LanguageData[] languagesData = Localization.Settings.Languages;
            for (int x = 0; x < languagesData.Length; x++) {
                GUILayout.Label($"{languagesData[x].Language}", GUILayout.ExpandWidth(true));
            }
            GUILayout.Space(_buttonWidth);
            GUILayout.EndHorizontal();

            // Table content.
            if (tableData.Count > 0) {
                for (int x = 0; x < tableData.Count; x++) {
                    GUILayout.BeginHorizontal();
                    // Display current element index.
                    GUILayout.Label($"{x}", GUILayout.ExpandWidth(false), GUILayout.Width(_indexWidth));
                    // Display current element key.
                    tableData.Entries[x].Key = EditorGUILayout.TextField(tableData.Entries[x].Key,
                        GUILayout.Width(_keyWidth), GUILayout.ExpandWidth(false));
                    // Display current element in languages listed in settings.
                    for (int y = 0; y < languagesData.Length; y++) {
                        LanguageData languageData = languagesData[y];
                        StringData languagePair = tableData.Entries[x].Get(languageData.Language);
                        languagePair.Value = EditorGUILayout.TextArea(languagePair.Value, Wrapper,
                            GUILayout.Width(_valueWidth), GUILayout.ExpandWidth(true));
                    }
                    // Display trash button to delete entry from table.
                    if (GUILayout.Button(EditorGUIUtility.IconContent("d_TreeEditor.Trash"),
                        GUILayout.Width(_buttonWidth), GUILayout.ExpandWidth(false), GUILayout.ExpandHeight(true))) {
                        tableData.RemoveAt(x);
                    }
                    GUILayout.EndHorizontal();
                }
            }

            EditorGUILayout.Space(_space);
            EditorGUILayout.EndScrollView();
            EditorGUILayout.Space(_space);

            // Button to add new entry.
            if (GUILayout.Button("Create new entry")) {
                tableData.Create(string.Empty);
            }

            // Mark as dirty if changes occured.
            if (EditorGUI.EndChangeCheck()) {
                EditorUtility.SetDirty(tableData);
                // Since Unity has problems with SetDirty, mark currently active
                // scene as dirty too, so changes will be saved for sure.
                EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
            }

            // Unselect anything when clicking on window background.
            if (GUI.Button(new Rect(0, 0, Screen.width, Screen.height), string.Empty, GUIStyle.none)) {
                GUI.FocusControl(null);
            }
        }

    }
}