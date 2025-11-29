using UnityEngine;
using System.Collections;

public class ErrorLineAnimation : MonoBehaviour
{
    public LineRenderer lr;
    public float duration = 0.5f;

    private Vector3 startPos;
    private Vector3 endPos;

    private void Awake()
    {
        if (lr == null)
        {
            lr = GetComponent<LineRenderer>();
            if (lr == null)
                Debug.LogError("Aucun LineRenderer trouvé sur " + gameObject.name);
        }
    }

    public void PlayError()
    {

        if (lr == null || lr.positionCount < 2)
        {
            Debug.LogError("LineRenderer invalide pour animation d'erreur !");
            return;
        }

        gameObject.SetActive(true);

        startPos = lr.GetPosition(0);
        endPos = lr.GetPosition(1);

        lr.startColor = Color.red;
        lr.endColor = Color.red;

        StopAllCoroutines();
        StartCoroutine(ErrorRoutine());
    }

    public void Destroy()
    {
        if (lr == null || lr.positionCount < 2)
        {
            Debug.LogError("LineRenderer invalide pour animation d'erreur !");
            return;
        }

        gameObject.SetActive(true);

        startPos = lr.GetPosition(0);
        endPos = lr.GetPosition(1);

        StopAllCoroutines();
        StartCoroutine(ErrorRoutine());
    }

    private IEnumerator ErrorRoutine()
    {
        float t = 0;
        while (t < 1f)
        {
            t += Time.deltaTime / duration;

            Vector3 currentEnd = Vector3.Lerp(endPos, startPos, t);
            lr.SetPosition(1, currentEnd);

            float width = Mathf.Lerp(0.1f, 0f, t);
            lr.startWidth = lr.endWidth = width;

            yield return null;
        }
        Destroy(gameObject);
    }
}
