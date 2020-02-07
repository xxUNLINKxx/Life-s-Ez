using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingPlatform : MonoBehaviour
{
     
    enum movingPlatform_Enum{ ROTATING, SIDETOSIDE };

    [SerializeField] private movingPlatform_Enum movingP;
    [SerializeField] private float speed,S2S_Distance;
    
    private void Start()
    {
         switch (movingP)
        {
            case movingPlatform_Enum.ROTATING:
                Rotate();
                break;
            case movingPlatform_Enum.SIDETOSIDE:
                Side2Side(S2S_Distance);
                break;
            default: Debug.Log("Add a function please");
                break;
        }
       
    }

    void Rotate()
    {
        
    }

    void Side2Side(float distance)
    {
        LeanTween.moveLocalX(this.gameObject, distance, speed).setEaseLinear().setLoopPingPong();
    }
}
