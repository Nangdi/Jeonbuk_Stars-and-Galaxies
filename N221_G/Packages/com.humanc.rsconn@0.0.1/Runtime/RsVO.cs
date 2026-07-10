using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace com.humanc.rsconn
{

    public class RsVO
    {
        private string comName = "";
        private int bit = 0;


        public string ComName { get => comName; set => comName = value; }
        public int Bit { get => bit; set => bit = value; }
    }

}