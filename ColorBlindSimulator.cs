namespace BrettMStory.Unity.ColorBlind {

    using UnityEditor;
    using UnityEngine;

    /// <summary>
    /// Various types of color blindness.
    /// </summary>
    public enum ColorBlindType {
        Normal = -1,
        Protanopia = 0,
        Protanomaly = 1,
        Deuteranopia = 2,
        Deuteranomaly = 3,
        Tritanopia = 4,
        Tritanomaly = 5,
        Monochromacy = 6,
        BlueCone = 7
    }

    /// <summary>
    /// Simulates color blindness on a camera.
    /// </summary>
    public class ColorBlindSimulator : MonoBehaviour {

        /// <summary>
        /// The various materials that can be used.
        /// </summary>
        private readonly Material[] _materials = new Material[8];

        /// <summary>
        /// The selected color blind type.
        /// </summary>
        private ColorBlindType _selectedColorBlindType = ColorBlindType.Normal;

        /// <summary>
        /// Gets or sets the type of the selected color blind.
        /// </summary>
        /// <value>
        /// The type of the selected color blind.
        /// </value>
        public ColorBlindType SelectedColorBlindType {
            get {
                return this._selectedColorBlindType;
            }

            set {
                this._selectedColorBlindType = value;
            }
        }

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
            if (this.SelectedColorBlindType == ColorBlindType.Normal) {
                return;
            }

            Graphics.Blit(source, destination, this._materials[(int)this._selectedColorBlindType]);
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
        /// The current index selected from the drop down menu.
        /// </summary>
        private int _currentIndex = 0;

        /// <summary>
        /// Called when [inspector GUI].
        /// </summary>
        public override void OnInspectorGUI() {
            this.DrawDefaultInspector();
            this._currentIndex = EditorGUILayout.Popup(this._currentIndex, this._choices);
            var colorBlindSimulator = this.target as ColorBlindSimulator;
            colorBlindSimulator.SelectedColorBlindType = (ColorBlindType)(this._currentIndex - 1);
            EditorUtility.SetDirty(target);
        }
    }
}