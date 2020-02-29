using UnityEngine;


public class MovingPlatformController : MonoBehaviour
{
    #region Fields

    private const float FREQ_WITH_PLAYER = 0.1f;
    private const float FREQ_WITHOUT_PLAYER = 0.2f;

    [SerializeField] private SpringJoint2D _lift;
    
    #endregion

    #region UnityMethods

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            _lift.frequency = FREQ_WITH_PLAYER;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            _lift.frequency = FREQ_WITHOUT_PLAYER;
    }

    #endregion
}
