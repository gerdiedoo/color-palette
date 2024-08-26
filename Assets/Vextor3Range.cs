using UnityEngine;

[System.Serializable]
public class Vector3Range
{
    public Vector3 min;
    public Vector3 max;
    
    //For slider limits (Dont touch)
    [SerializeField]
    private float minWindow;
    [SerializeField]
    private float maxWindow;

    public Vector3Range(Vector3 min, Vector3 max)
    {
        this.min = min;
        this.max = max;
    }

    public Vector3 Lerp(float t)
    {
        return Vector3.Lerp(min, max, t);
    }
    public Vector3 RandomNumberInRange(){
        return Extensions.RandomRangeVector(min,max);
    }

    public void Invert()
    {
        Vector3 temp = this.max;
        this.max = this.min;
        this.min = temp;
    }
    
    //You can add more functions of your own
}
