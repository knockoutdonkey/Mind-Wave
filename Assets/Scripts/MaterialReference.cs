using UnityEngine;

public class MaterialReference : MonoBehaviour {

    private static MaterialReference instance;

    public Material WavyMaterial;

    void Awake() {
        instance = this;
    }

    public static Material GetWavyMaterial() {
        if (instance == null) return null;

        return instance.WavyMaterial;
    }
}
