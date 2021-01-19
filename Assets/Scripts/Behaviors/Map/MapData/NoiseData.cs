using UnityEngine;

[CreateAssetMenu()]
public class NoiseData : ScriptableObject
{ 
    public int mapWidth;

    public int mapHeight;

    public float noiseScale;

    public int seed;

    [Range (0,10)]
    public int octaves;

    [Range(0,1)]
    public float persistance;

    public float lacunarity;

    public Vector2 offset;
    public bool falloff;


	void OnValidate() {
		if (lacunarity < 1) {
			lacunarity = 1;
		}
		if (octaves < 0) {
			octaves = 0;
		}
	}

}