using UnityEngine;

public static class Extensions
{
    public static Color HexToColor(string hex)
    {
        Color c = Color.black;
        ColorUtility.TryParseHtmlString(hex, out c);
        return c;
    }
    public static Vector3 RandomRangeVector(Vector3 min, Vector3 max)
    {
        return new Vector3(
            Random.Range(min.x, max.x),
            Random.Range(min.y, max.y),
            Random.Range(min.z, max.z)
        );
    }
}