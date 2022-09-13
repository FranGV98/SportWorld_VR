using UnityEngine;
public class BatCapsule : MonoBehaviour
{
    [SerializeField]
    private BatCapsuleFollower _batCapsuleFollowerPrefab;

    private void SpawnBatCapsuleFollower()
    {
        var follower = Instantiate(_batCapsuleFollowerPrefab);
        follower.transform.position = transform.position;
        follower.SetFollowTarget(this);
        follower.gameObject.layer = this.gameObject.layer;
    }

    private void Start()
    {
        SpawnBatCapsuleFollower();
    }
}