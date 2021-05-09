using UnityEngine;
using UnityEngine.Events;

public class CalculatePoints : MonoBehaviour
{

    [SerializeField] private Camera calcCamera;

    [SerializeField] private Vector3[] colors;

    [SerializeField] private UnityEvent onPointsCountedEvent;

    private Points _points;

    private void Awake()
    {
        _points = FindObjectOfType<Points>();
    }

    public void StartCalculating()
    {
        CalculatePixels(GetTexture2DFromRenderTexture(calcCamera));
    }

    public Texture2D GetTexture2DFromRenderTexture(Camera camera)
    {
        Texture2D tex = new Texture2D(camera.targetTexture.width, camera.targetTexture.height, TextureFormat.RGB24, false);
        RenderTexture.active = camera.targetTexture;
        tex.ReadPixels(new Rect(0, 0, camera.targetTexture.width, camera.targetTexture.height), 0, 0);
        tex.Apply();
        return tex;
    }

    private void CalculatePixels(Texture2D texture)
    {
        Color32[] pixels = texture.GetPixels32();

        foreach (var pixel in pixels)
        {
            if (pixel.Equals(new Color32((byte)colors[0].x, (byte)colors[0].y, (byte)colors[0].z, 255)))
            {
                _points.AddPoints(1, 1);
            }
            else if (pixel.Equals(new Color32((byte)colors[1].x, (byte)colors[1].y, (byte)colors[1].z, 255)))
            {
                _points.AddPoints(2, 1);
            }
        }
        onPointsCountedEvent?.Invoke();
    }
}
