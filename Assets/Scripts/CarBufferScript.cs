using UnityEngine;

public class CarBufferScript : MonoBehaviour
{
    [SerializeField] private float mainSpeed;
    [SerializeField] private Animator animator;

    [SerializeField] private Vector3 bufferOffset;
    [SerializeField] private ParticleSystem particle;

    public void MoveCarBuffer(Vector3 target)
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(
            target.x + bufferOffset.x,
            target.y + bufferOffset.y,
            target.z + bufferOffset.z), mainSpeed * Time.deltaTime);
    }

    public void RotateCarBuffer(Vector3 target)
    {
        var targetRotation = Quaternion.FromToRotation(transform.up, target) * transform.rotation;
        transform.rotation = targetRotation;
    }

    public void StartBufferAnim()
    {
        if(animator.GetBool("isPolishing") && particle.isPlaying)
            return;

        animator.SetBool("isPolishing", true);
        particle.Play();
        
    }

    public void StopBufferAnim()
    {
        if(!animator.GetBool("isPolishing") && !particle.isPlaying)
            return;

        animator.SetBool("isPolishing", false);
        particle.Stop();
    }
}