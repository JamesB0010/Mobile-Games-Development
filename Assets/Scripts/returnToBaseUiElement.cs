using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class returnToBaseUiElement : MonoBehaviour
{
    public void PlayAnimation()
    {
        GetComponent<Animation>().Play();
    }
}
