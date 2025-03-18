using UnityEngine;

public class PetMove: MonoBehaviour
{
    [SerializeField] public TrailRenderer _playerTrailRenderer;
    [SerializeField] private float _trailSpeed = 2f;
    [SerializeField] private float _trailableMinDistance = 1f;
    private void Awake()
    {
        
    }
    private void Update()
    {
        TrailPlayer();
    }

    public void TrailPlayer()
    {
        if (!ReferenceEquals(_playerTrailRenderer, null) && 1 < _playerTrailRenderer.positionCount &&
            _trailableMinDistance < Vector2.Distance(transform.position, _playerTrailRenderer.transform.position))
        {
            Vector2 trailEndPosition = _playerTrailRenderer.GetPosition(_playerTrailRenderer.positionCount - 1);
            transform.position = Vector2.MoveTowards(transform.position, trailEndPosition, _trailSpeed * Time.deltaTime);
        }
    }
}

