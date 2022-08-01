using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class Cube : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float movementSpeed;
    
    private Renderer _renderer;
    private Material _material;

    private Vector3 _randomRotationAxis;
    private Vector3 _randomScaleTarget;
    private Vector3 _positionTarget;
    private float _orthographicSize;
    private Color _targetColor;
    private float _colorLerpTimer; 
    
    void Start()
    {
        _orthographicSize = Camera.main.orthographicSize;
        _renderer = GetComponent<Renderer>();
        _material = _renderer.material;
        _randomRotationAxis = Random.insideUnitSphere;
        _randomScaleTarget = Random.insideUnitSphere * Random.Range(1, 3f);
        _positionTarget = Random.insideUnitSphere * Random.Range(-_orthographicSize / 2f, _orthographicSize / 2f);
    }
    
    void Update()
    {
        Rotate();
        Scale();
        Translate();
        ChangeColor();
    }

    private void Translate()
    {
        transform.position = Vector3.MoveTowards(transform.position, _positionTarget, movementSpeed * Time.deltaTime);
        if (Vector3.Distance(transform.position, _positionTarget) < 0.2f)
        {
            _positionTarget = Random.insideUnitSphere * Random.Range(-_orthographicSize / 2f, _orthographicSize / 2f);
        }
    }

    private void Scale()
    {
        transform.localScale = Vector3.Lerp(Vector3.one, _randomScaleTarget, Easing.InCubic(Mathf.PingPong(Time.time, 1)));
    }

    private void Rotate()
    {
        transform.Rotate(_randomRotationAxis * rotationSpeed * Time.deltaTime);
        rotationSpeed = Mathf.Abs(Mathf.Clamp(Mathf.Cos(Time.time),0.5f,1f)) * Random.Range(60, 300f);
    }

    private void ChangeColor()
    {
        if (_material.color != _targetColor)
        {
            _colorLerpTimer += Time.deltaTime;
            _material.color = Color.Lerp(_material.color, _targetColor, _colorLerpTimer);
        }
        else
        {
            _targetColor = Random.ColorHSV(0, 1f, 0.4f,1f,0.8f,1f);
            _colorLerpTimer = 0;
        }
    }
}
