using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using XCharts.Runtime;
using System.Collections.Generic;

[RequireComponent(typeof(LineChart))]
public class ColorGradient : MonoBehaviour
{
    private LineChart chart;
    public List<float> redValues = new List<float>();
    public  List<float> greenValues = new List<float>();
    public List<float> blueValues = new List<float>();
    public Image redImage;
    public Image greenImage;
    public Image blueImage;
    public Image gradientImage;
    private Color[] rgbValues;
    private Vector3 _A = new Vector3(1.0f, 1.0f, 1.0f);
    private Vector3 _B = new Vector3(1.0f, 1.0f, 1.0f);
    private Vector3 _C = new Vector3(1.0f, 1.0f, 1.0f);
    private Vector3 _D = new Vector3(1.0f, 1.0f, 1.0f);

    [SerializeField, Range(0f, 1f)]
    private float A_x;
    [SerializeField, Range(0f, 1f)]
    private float A_y;
    [SerializeField, Range(0f, 1f)]
    private float A_z;

    [SerializeField, Range(0f, 1f)]
    private float B_x;
    [SerializeField, Range(0f, 1f)]
    private float B_y;
    [SerializeField, Range(0f, 1f)]
    private float B_z;

    [SerializeField, Range(0f, 1f)]
    private float C_x;
    [SerializeField, Range(0f, 1f)]
    private float C_y;
    [SerializeField, Range(0f, 1f)]
    private float C_z;

    [SerializeField, Range(0f, 1f)]
    private float D_x;
    [SerializeField, Range(0f, 1f)]
    private float D_y;
    [SerializeField, Range(0f, 1f)]
    private float D_z;

    private Line redSerie;
    private Line greenSerie;
    private Line blueSerie;

    private Vector3 prevA;
    private Vector3 prevB;
    private Vector3 prevC;
    private Vector3 prevD;
    public Vector3 rgb;

    void Start()
    {
        chart = GetComponent<LineChart>();

        // Setup chart components
        var title = chart.EnsureChartComponent<Title>();
        title.show = true;
        title.text = "test";

        var grid = chart.EnsureChartComponent<GridCoord>();
        grid.bottom = 30;
        grid.right = 30;
        grid.left = 50;
        grid.top = 80;

        // Remove components that are not needed
        chart.RemoveChartComponent<VisualMap>();
        chart.RemoveData();

        // Initialize series
        redSerie = chart.AddSerie<Line>("Red");
        greenSerie = chart.AddSerie<Line>("Green");
        blueSerie = chart.AddSerie<Line>("Blue");

        SetupSeries(redSerie);
        SetupSeries(greenSerie);
        SetupSeries(blueSerie);

        // Initialize previous values
        UpdatePreviousValues();
        GenerateData();
    }

    private void SetupSeries(Line serie)
    {
        serie.animation.enable = false;
        serie.symbol.show = false;
        serie.lineType = LineType.Smooth;
    }

    void Update()
    {
        // Update values and check for changes
        UpdateValues();
        if (ValuesChanged())
        {
            GenerateData();
            UpdatePreviousValues();
        }
    }

    private void UpdateValues()
    {
        _A = new Vector3(A_x, A_y, A_z);
        _B = new Vector3(B_x, B_y, B_z);
        _C = new Vector3(C_x, C_y, C_z);
        _D = new Vector3(D_x, D_y, D_z);
    }

    private void UpdatePreviousValues()
    {
        prevA = _A;
        prevB = _B;
        prevC = _C;
        prevD = _D;
    }

    private bool ValuesChanged()
    {
        return _A != prevA || _B != prevB || _C != prevC || _D != prevD;
    }

    private void GenerateData()
    {
        float start = 0f;
        float end = 2 * Mathf.PI;
        float step = 0.08f;

        // Clear existing data
        redSerie.ClearData();
        greenSerie.ClearData();
        blueSerie.ClearData();

        redValues.Clear();
        greenValues.Clear();
        blueValues.Clear();
        for (float x = start; x <= end; x += step)
        {
            // rgb.x = _A.x + _B.x * Mathf.Cos(2 * Mathf.PI*(_C.x * x + _D.x));
            // rgb.y = _A.y + _B.y * Mathf.Cos(2 * Mathf.PI*(_C.y * x + _D.y));
            // rgb.z = _A.z + _B.z * Mathf.Cos(2 * Mathf.PI*(_C.z * x + _D.z));
            rgb = new Vector3(
                Mathf.Clamp(_A.x + _B.x * Mathf.Cos(2 * Mathf.PI * (_C.x * x + _D.x)), 0f, 1f),
                Mathf.Clamp(_A.y + _B.y * Mathf.Cos(2 * Mathf.PI * (_C.y * x + _D.y)), 0f, 1f),
                Mathf.Clamp(_A.z + _B.z * Mathf.Cos(2 * Mathf.PI * (_C.z * x + _D.z)), 0f, 1f)
            );
            // Add data points to the series
            chart.AddData(redSerie.index, x, rgb.x);
            redValues.Add(rgb.x);
            chart.AddData(greenSerie.index, x, rgb.y);
            greenValues.Add(rgb.y);
            chart.AddData(blueSerie.index, x, rgb.z);
            blueValues.Add(rgb.z);
        }
        rgbValues = new Color[redValues.Count];
        for(int i=0;i<redValues.Count;i++){
            rgbValues[i] = new Color(0f,0f,0f,1f);
        }
        // Refresh the chart to display the data
        chart.RefreshChart();
        UpdateGrayscaleImage(redImage,redValues, "red");
        UpdateGrayscaleImage(greenImage,greenValues, "green");
        UpdateGrayscaleImage(blueImage,blueValues,"blue");
        gradient(gradientImage, rgbValues);
    }
    public void UpdateGrayscaleImage(Image grayscaleImage, List<float> chartValues, string color)
    {
        RectTransform img = grayscaleImage.GetComponent<RectTransform>();
        img.sizeDelta = new Vector2(chartValues.Count, img.sizeDelta.y);
        int width = (int)img.rect.width;

        Texture2D texture;
        Color[] colors;
        texture = new Texture2D(width, 1);
        colors = new Color[width];
        // Make sure the chart values match the width of the image
        if (chartValues.Count != width)
        {
            Debug.LogWarning("Chart values count does not match the width of the image.");
            return;
        }

        for (int i = 0; i < width; i++)
        {
            float value = chartValues[i];
            // As the value approaches 1, it gets darker; as it approaches 0, it gets lighter
            float intensity = 1f - value;
            if(color == "red"){
                colors[i] = new Color(intensity, 0f, 0f, 1f); // Grayscale color
                rgbValues[i].r = intensity;
            }else if(color == "green"){
                colors[i] = new Color(0f, intensity, 0f, 1f); // Grayscale color
                rgbValues[i].g = intensity;
            }else{
                colors[i] = new Color(0f, 0f, intensity, 1f); // Grayscale color
                rgbValues[i].b = intensity;
            }
        }

        // Set the colors to the texture
        texture.SetPixels(colors);
        texture.Apply();
        grayscaleImage.sprite = Sprite.Create(texture, new Rect(0, 0, width, 1), new Vector2(0.5f, 0.5f));
    }
    public void gradient(Image grad, Color[] colors){
        RectTransform img = grad.GetComponent<RectTransform>();
        img.sizeDelta = new Vector2(colors.Length, img.sizeDelta.y);
        int width = (int)img.rect.width;
        Texture2D texture;
        texture = new Texture2D(width, 1);
        if (colors.Length != width)
        {
            Debug.LogWarning("Chart values count does not match the width of the image.");
            return;
        }
        // Set the colors to the texture
        texture.SetPixels(colors);
        texture.Apply();
        grad.sprite = Sprite.Create(texture, new Rect(0, 0, width, 1), new Vector2(0.5f, 0.5f));
    }
}
