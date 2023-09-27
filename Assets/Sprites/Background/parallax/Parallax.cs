using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private Camera cam;

    [SerializeField] private Transform subject;

    private Vector2 _startPosition;

    private float _startZ;

    private Vector2 Travel => (Vector2)cam.transform.position - _startPosition;

    private float DistanceFromSubject => transform.position.z - subject.position.z;

    private float ClippingPlane =>
        (cam.transform.position.z + (DistanceFromSubject > 0 ? cam.farClipPlane : cam.nearClipPlane));

    private float ParallaxVector => Mathf.Abs(DistanceFromSubject) / ClippingPlane;

    // Start is called before the first frame update
    void Start()
    {
        _startPosition = transform.position;
        _startZ = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 newPos = _startPosition + Travel * ParallaxVector;
        transform.position = new Vector3(newPos.x, newPos.y, _startZ);
    }
}