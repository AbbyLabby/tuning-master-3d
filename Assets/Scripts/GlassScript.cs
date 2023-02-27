using System.Collections.Generic;
using UnityEngine;

public class GlassScript : MonoBehaviour, IGlass
{
    [SerializeField] private int health;
    [SerializeField] private GameObject particle;
    [SerializeField] private List<GlassCluster> glassClusters;

    private int index = 0;
    private bool isBroken = false;

    public void Hit(RaycastHit hit)
    {
        health--;

        if (health <= 0 && !isBroken)
        {
            if (glassClusters.Capacity > 0)
            {
                foreach (var glass in glassClusters)
                {
                    glass.Break(hit);
                }
            }
            
            BreakGlass();
            
            var targetRotation = Quaternion.FromToRotation(transform.up, -hit.normal) * transform.rotation;

            Instantiate(particle, hit.point, targetRotation);
        }
    }

    private void BreakGlass()
    {
        GetComponent<MeshRenderer>().material.color = new Color(0, 0, 0, 0);
        EventsData.GlassBrokenEvent.Invoke();
        isBroken = true;
    }
}
