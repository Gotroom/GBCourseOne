using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections;

public class TransparentGridController : MonoBehaviour
{
    #region Fields

    private const float IN_TRIGGER_ALPHA = 0.5f;
    private const float OUT_TRIGGER_ALPHA = 1.0f;
    private const float FADE_TRANSPARENCY = 0.05f;
    private const float DELAY = 0.05f;

    [SerializeField] private Tilemap _tilemap;

    #endregion

    #region UnityMethods

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(MakeTransparent());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(MakeSolid());
        }
    }

    #endregion


    #region Methods

    private IEnumerator MakeTransparent()
    {
        var tilemap = gameObject.GetComponent<Tilemap>();
        var color = tilemap.color;
        while(tilemap.color.a > IN_TRIGGER_ALPHA)
        {
            color.a -= FADE_TRANSPARENCY;
            tilemap.color = color;
            yield return new WaitForSeconds(DELAY);
        }
        
    }

    private IEnumerator MakeSolid()
    {
        var tilemap = gameObject.GetComponent<Tilemap>();
        var color = tilemap.color;
        while (tilemap.color.a < OUT_TRIGGER_ALPHA)
        {
            color.a += FADE_TRANSPARENCY;
            tilemap.color = color;
            yield return new WaitForSeconds(DELAY);
        }
    }

    #endregion
}
