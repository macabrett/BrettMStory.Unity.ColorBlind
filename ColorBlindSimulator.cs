namespace BrettMStory.Unity.ColorBlind {

    using UnityEditor;
    using UnityEngine;

    /// <summary>
    /// Various types of color blindness.
    /// </summary>
    public enum ColorBlindType {
        Normal = 0,
        Protanopia = 1,
        Protanomaly = 2,
        Deuteranopia = 3,
        Deuteranomaly = 4,
        Tritanopia = 5,
        Tritanomaly = 6,
        Monochromacy = 7,
        BlueCone = 8
    }

    /// <summary>
    /// Simulates color blindness on a camera.
    /// </summary>
    public class ColorBlindSimulator : MonoBehaviour {

        /// <summary>
        /// The selected color blind type.
        /// </summary>
        [SerializeField]
        public int SelectedColorBlindType;

        /// <summary>
        /// The various materials that can be used.
        /// </summary>
        private readonly Material[] _materials = new Material[8];

        /// <summary>
        /// Awakes this instance.
        /// </summary>
        private void Awake() {
            this._materials[0] = new Material(Shader.Find("Hidden/Protanopia"));
            this._materials[1] = new Material(Shader.Find("Hidden/Protanomaly"));
            this._materials[2] = new Material(Shader.Find("Hidden/Deuteranopia"));
            this._materials[3] = new Material(Shader.Find("Hidden/Deuteranomaly"));
            this._materials[4] = new Material(Shader.Find("Hidden/Tritanopia"));
            this._materials[5] = new Material(Shader.Find("Hidden/Tritanomaly"));
            this._materials[6] = new Material(Shader.Find("Hidden/Monochromacy"));
            this._materials[7] = new Material(Shader.Find("Hidden/BlueCone"));
        }

        /// <summary>
        /// Called when [render image].
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="destination">The destination.</param>
        private void OnRenderImage(RenderTexture source, RenderTexture destination) {
            if ((ColorBlindType)this.SelectedColorBlindType == ColorBlindType.Normal) {
                return;
            }

            Graphics.Blit(source, destination, this._materials[this.SelectedColorBlindType - 1]);
        }
    }

    /// <summary>
    /// Editor for the <see cref="ColorBlindSimulator"/> class.
    /// </summary>
    [CustomEditor(typeof(ColorBlindSimulator))]
    public class ColorBlindSimulatorEditor : Editor {

        /// <summary>
        /// The possible choices for color blindness.
        /// </summary>
        private readonly string[] _choices = new string[9] {
            "Normal",
            "Protanopia (Red-Blind)",
            "Protanomaly (Red-Weak)",
            "Deuteranopia (Green-Blind)",
            "Deuteranomaly (Green-Weak)",
            "Tritanopia (Blue-Blind)",
            "Tritanomaly (Blue-Weak)",
            "Monochromacy (Black and White)",
            "Blue Cone Monochromacy",
        };

        /// <summary>
        /// Called when [inspector GUI].
        /// </summary>
        public override void OnInspectorGUI() {
            serializedObject.Update();
            var colorBlindType = serializedObject.FindProperty("SelectedColorBlindType");
            colorBlindType.intValue = EditorGUILayout.Popup("Color Blind Type", colorBlindType.intValue, this._choices);
            serializedObject.ApplyModifiedProperties();
        }
    }
}