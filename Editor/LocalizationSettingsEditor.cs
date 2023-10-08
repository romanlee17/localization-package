using UnityEditor;
using UnityEngine;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

namespace romanlee17.UnityLocalization {
    [CustomEditor(typeof(LocalizationSettings))]
    public class LocalizationSettingsEditor : Editor {

        public override void OnInspectorGUI() {
            LocalizationSettings settings = (LocalizationSettings)target;

            // Detect table changes.
            EditorGUI.BeginChangeCheck();

            base.OnInspectorGUI();

            // Mark as dirty if changes occured.
            if (EditorGUI.EndChangeCheck()) {
                foreach (var entry in settings.languages) {
                    entry.name = entry.language.ToString();
                }
                EditorUtility.SetDirty(settings);
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