using System.Collections;
using DG.Tweening;
using UnityEngine;

public class HammerScript : MonoBehaviour
{
    private Vector3 startPos = new Vector3();
    private bool isRunning = false;

    [SerializeField] private Vector3 offset;

    public void Kick(RaycastHit hit)
    {
        if (isRunning)
            return;

        isRunning = true;

        startPos = transform.position;

        transform.DOMove(new Vector3(hit.point.x + offset.x, hit.point.y + offset.y, hit.point.z + offset.z), .2f)
            .OnComplete(KickBack);
        StartCoroutine(AnimDelay());
    }

    private IEnumerator AnimDelay()
    {
        yield return new WaitForSeconds(.1f);
        GetComponent<Animator>().SetTrigger("KickTrigger");
    }

    private void KickBack()
    {
        transform.DOMove(startPos, .05f).OnComplete(() => isRunning = false);
    }
}