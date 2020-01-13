using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils : MonoBehaviour
{
    private static int _maxHeight = World._columnHeight * 16;
    private static float _smooth = 0.0025f;
    private static float _cavesSmooth = 0.05f;
    private static int _octaves = 4;
    private static float _persistence = 0.5f;

    public static int GenerateStoneHeight(float x, float z)
    {
        float height = Map(0, _maxHeight * 0.9f, 0, 1, fBM(x * _smooth * 2, z * _smooth * 2, _octaves + 1, _persistence));
        return (int)height;
    }

    public static int GenerateHeight (float x, float y)
    {
        float height = Map(0, _maxHeight, 0, 1, fBM(x * _smooth, y * _smooth, _octaves, _persistence));
        return (int)height;
    }

    public static float fBM3D(float x, float y, float z, float sm, int oct)
    {
        float XY = fBM(x * sm, y * sm, oct, 0.5f);
        float YZ = fBM(y * sm, z * sm, oct, 0.5f);
        float XZ = fBM(x * sm, z * sm, oct, 0.5f);

        float YX = fBM(y * sm, x * sm, oct, 0.5f);
        float ZY = fBM(z * sm, y * sm, oct, 0.5f);
        float ZX = fBM(z * sm, x * sm, oct, 0.5f);

        return (XY + YZ + XZ + YX + ZY + ZX) / 6.0f; 
    }

    private static float Map(float newmin, float newmax, float origmin, float origmax, float value)
    {
        return Mathf.Lerp(newmin, newmax, Mathf.InverseLerp(origmin, origmax, value));
    }

    static float fBM(float x, float z, int oct, float pers)
    {
        float total = 0;
        float frequency = 1;
        float amplitude = 1;
        float maxValue = 0;
        for (int i = 0; i < oct; i++)
        {
            total += Mathf.PerlinNoise(x * frequency, z * frequency) * amplitude;

            maxValue += amplitude;

            amplitude *= pers;
            frequency *= 2;
        }

        return total / maxValue; 
    }

    public static string GetChunk(float x, float y, float z)
    {
        int xChunk = FloatToInt(x) / World._chunkSize * 16;
        int yChunk = FloatToInt(y) / World._chunkSize * 16;
        int zChunk = FloatToInt(z) / World._chunkSize * 16;

        Vector3 chunkPos = new Vector3(xChunk, yChunk, zChunk);

        return World.BuildChunkName(chunkPos);
    }

    public static int GetPositionInChunk(float f)
    {
        int i = (FloatToInt(f) % World._chunkSize);

        return i;
    }

    private static int FloatToInt(float f)
    {
        return Mathf.FloorToInt(f);
    }

    public static int GenerateTree(float f)
    {
        int yPos = GetPositionInChunk(f);

        int treeRounds = World._chunkSize - yPos;
        return treeRounds;
    }
}
