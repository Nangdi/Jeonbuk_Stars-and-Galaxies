using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;

#if UNITY_EDITOR
using UnityEditor;
[CustomEditor(typeof(SetBoatAreaControl))]
public class SetBoatAreaControlEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        SetBoatAreaControl setBoatAreaControl = target as SetBoatAreaControl;
        if (GUILayout.Button("배 위치 선정")) {
            setBoatAreaControl.SetBoatAreaObject();
        }
    }
}
#endif

/// <summary>
/// 보트 배치에 대한 로직 
/// </summary>
public class SetBoatAreaControl : MonoBehaviour
{
    public List<ArrangementObject> arrangementObjectList;

    //배치 영역 텍스쳐 정보 
    public Texture2D areaTexture;

    //보트 영역 배치 오브젝트
    public void SetBoatAreaObject() {

        GetPos();

        
        if (posList1.Count > 0)
        {
            SetBotArea(0, posList1);
            SetBotArea(1, posList2);
        }
    }


    public void SetBotArea(int Vindex,List<Vector2> posList) {
        RectTransform rectTransform = arrangementObjectList[Vindex].GetComponent<RectTransform>();
        int index = UnityEngine.Random.Range(0, posList.Count);

        Vector2 pivot = rectTransform.pivot;
        pivot.x = 0.5f;
        pivot.y = 0.5f;
        rectTransform.pivot = pivot;

        rectTransform.anchorMax = Vector2.zero;
        rectTransform.anchorMin = Vector2.zero;

        Vector2 view = posList[index];
        rectTransform.anchoredPosition = view;
    }
    


    //생성 포지션 정보
    public List<Vector2> posList1;
    public List<Vector2> posList2;
    private void GetPos() {

        posList1.Clear();
        posList2.Clear();
        for (int i=0;i< areaTexture.width; i = i+10) { 
            for (int j = 0; j < areaTexture.height; j = j+10)
            {
                //컬러 정보
                Color _color = areaTexture.GetPixel(i, j);
                if (_color.r.Equals(1) && _color.g.Equals(1) && _color.b.Equals(1))
                {
                    Vector2 _pos = Vector2.zero;
                    _pos.Set(i, j);
                    posList1.Add(_pos);
                }
                else if (_color.r.Equals(1) && _color.g.Equals(0) && _color.b.Equals(0))
                {
                    Vector2 _pos = Vector2.zero;
                    _pos.Set(i, j);
                    posList2.Add(_pos);
                }
            }
        }



    }







}
