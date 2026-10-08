using DataImporter;
using UnityEngine;
using UnityEngine.UI;

public class ImageTest : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private string _imagePath;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _image.sprite = GameDataContainer.GetImage(_imagePath);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
