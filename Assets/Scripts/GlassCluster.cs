using UnityEngine;

public class GlassCluster : MonoBehaviour, IGlass
{
    [SerializeField]
    private GlassScript glassScript;

    [SerializeField] private GameObject particle;
    
    public void Hit(RaycastHit hit)
    {
        glassScript.Hit(hit);
    }

    public void Break(RaycastHit hit)
    {
        GetComponent<MeshRenderer>().material.color = new Color(0, 0, 0, 0);
        
        var targetRotation = Quaternion.FromToRotation(transform.up, -hit.normal) * transform.rotation;
        
        Instantiate(particle, hit.point, targetRotation);
    }
}
