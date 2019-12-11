using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colorChanger : MonoBehaviour
{

    public Material playerUnlit;
    public Material playerMiddle;
    public Material playerLit;
    public Material swordUnlit;
    public Material swordLit;


    private void Start()
    {
        GameObject.Find("Sword").GetComponent<MeshRenderer>().material = swordLit;
    }
    public void changePlayerColor()
    {
        GetComponent<SkinnedMeshRenderer>().material = playerUnlit;
        GameObject.Find("Crown").GetComponent<MeshRenderer>().material = playerUnlit;
        GameObject.Find("Dress").GetComponent<SkinnedMeshRenderer>().material = playerUnlit;
        GameObject.Find("Sword").GetComponent<MeshRenderer>().material = swordUnlit;
    }

    public void changePlayerColorBack()
    {
        GetComponent<SkinnedMeshRenderer>().material = playerLit;
        GameObject.Find("Sword").GetComponent<MeshRenderer>().material = swordLit;
        GameObject.Find("Dress").GetComponent<SkinnedMeshRenderer>().material = playerLit;
        GameObject.Find("Crown").GetComponent<MeshRenderer>().material = playerLit;
    }

    public void changePlayerColorMiddle()
    {
        GetComponent<SkinnedMeshRenderer>().material = playerLit;
        GameObject.Find("Sword").GetComponent<MeshRenderer>().material = swordLit;
        GameObject.Find("Dress").GetComponent<SkinnedMeshRenderer>().material = playerMiddle;
        GameObject.Find("Crown").GetComponent<MeshRenderer>().material = playerMiddle;
        GetComponent<SkinnedMeshRenderer>().material = playerMiddle;
    }
}