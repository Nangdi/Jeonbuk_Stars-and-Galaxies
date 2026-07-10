using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//컴포넌트 객체 
[System.Serializable]
public class ComponentVO {

    //컴포넌트 정보 (배열로 관리)
    public List<ComponentList> componentList;
    
    [System.Serializable]
    public class ComponentList {

        [SerializeField]
        private float x;

        [SerializeField]
        private float y;

        public float X { get => x; set => x = value; }
        public float Y { get => y; set => y = value; }
        
        private Vector2 vector2;

        //백터 2D 좌표 (get 전용)
        [Newtonsoft.Json.JsonIgnore]
        public Vector2 Vector2 { 
            get {
                return new Vector2(X, Y); 
            } 
        }
    }



}
