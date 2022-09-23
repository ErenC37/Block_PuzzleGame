using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DOT.Utilities.Extensions
{
    public class Test : MonoBehaviour
    {
        public void Start()
        {
            this.gameObject.SetActive(false, 5f);
        }
    }
}