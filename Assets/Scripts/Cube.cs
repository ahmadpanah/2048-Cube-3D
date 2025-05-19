using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Cube : MonoBehaviour
{
   static int staticID = 0;
   [SerializeField] private TMP_Text[] numbersText;

   [HideInInspector] public int CubeID;
   [HideInInspector] public Color CubeColor;
   [HideInInspector] public int CubeNumber;

   [HideInInspector] public Rigidbody CubeRigidbody;
   [HideInInspector] public bool IsMainCube;

   private MeshRenderer cubeMeshRenderer;

   private void Awake()
   {
      CubeID = staticID++;
      CubeRigidbody = GetComponent<Rigidbody>();
      cubeMeshRenderer = GetComponent<MeshRenderer>();
   }

   public void SetColor(Color color){
        CubeColor = color;
        cubeMeshRenderer.material.color = color;
   }
   public void SetNumber(int number){
        CubeNumber = number;
        for (int i = 0; i < 6; i++){
            numbersText[i].text = number.ToString();
        }
   }
}
