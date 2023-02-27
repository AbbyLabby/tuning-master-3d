using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    public List<GameObject> tires;
    public List<MeshRenderer> glasses;
    public SpringJoint springJoint;
    public GameObject body;
    public Rigidbody rigidbody;
    public Transform spoilerSetPoint;
    public GameObject spoilerObj;
    public GameObject spoilerParent;
    public GameObject changeCounter;
    public GameObject changeCounterRust;
    public GameObject vSpoilerCam;
    public MeshRenderer bodyRenderer;
    public GameObject exhaustParticle;
}
