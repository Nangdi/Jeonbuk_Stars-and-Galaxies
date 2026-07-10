using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Unity.Burst.Intrinsics;

using UnityEngine;
using static ObjectCreationManager;
using static UserTableSettingBase;
#if UNITY_EDITOR
using UnityEditor;
[CustomEditor(typeof(UserTableAddElementParentObject))]
public class UserTableAddElementParentObjectEditor : Editor
{

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        UserTableAddElementParentObject userTableAddElementParentObject = (UserTableAddElementParentObject)target;
        if (GUILayout.Button("이미지 교체 진행&스크립트 등록"))
        {
            userTableAddElementParentObject.elementIndexList.Clear();
            for (int i = 0; i < 118; i++) {
                userTableAddElementParentObject.elementIndexList.Add(i);
            }
        }
    }

}
#endif

public class UserTableAddElementParentObject : MonoBehaviour
{
    //텍스트 리스트
    public List<Texture2D> texturtList;

    [Header("생성 원자 오브젝트")]
    public UserTableAddElementObject createElementObject;

    public Transform elementParentObject;
    public List<int> elementIndexList;

    private Coroutine onInsertElement = null;

    public enum PlayState { 
        None,
        RightAnswer/*정답*/,
        TimeOut/*시간 타임아웃*/ }

    [Header("플레이 상태 정보")]
    public PlayState playState;

    //스테이션 상태 정보
    public UserTableSettingBase.StationState _stationState;

    public ObjectCreationManager objectCreationManager;

    //리셋 
    public void OnReset() {
        playState = PlayState.None;
    }

    //원자 진행
    public void OnInsertElementStart() {
        OnRemoveInsertElement();
        onInsertElement = StartCoroutine(OnInsertElement());
    }

    private void OnRemoveInsertElement() {
        if (onInsertElement != null) {
            StopCoroutine(onInsertElement);
            onInsertElement = null;
        }
    }

    //원자 생성 
    IEnumerator OnInsertElement() {
        yield return null;
        //정답일경우 작동
        yield return new WaitUntil(() => (int)playState>0);
        int maxCount = 0;
        switch (playState) {
            case PlayState.RightAnswer://정답일경우
                maxCount = elementIndexList.Count;
                break;
            case PlayState.TimeOut://타임아웃일경우
                maxCount = elementIndexList.Count;
                //maxCount = 6;
                break;
        }

        if (_stationState.Equals(UserTableSettingBase.StationState.mission2))
        {
            objectCreationManager.UserRectAreaDataViewResetActive();
            TargetSelectArea();
        }
        for (int i=0;i< maxCount; i++) {
            yield return new WaitForSeconds(0.01f);
            Transform targetTransform = elementParentObject.GetChild(i);
            UserTableAddElementObject userTableAddElementObject =
                GameObject.Instantiate<UserTableAddElementObject>(createElementObject, this.transform);
            userTableAddElementObject.index = i;
            if (_stationState.Equals(UserTableSettingBase.StationState.mission1))
            {
                userTableAddElementObject.motionType = UserTableAddElementObject.MotionType.mission1Motion;
                userTableAddElementObject.moveTargetObject = targetTransform;
            }
            else if (_stationState.Equals(UserTableSettingBase.StationState.mission2))
            {
                //ObjectCreationManager
                userTableAddElementObject.motionType = UserTableAddElementObject.MotionType.mission2Motion;
                GameObject gameObject=new GameObject();
                gameObject.transform.parent = GameObjectControl.Instance.solarSystemObject.transform;
                gameObject.transform.position = Vector3.zero;
                gameObject.transform.localEulerAngles = Vector3.zero;

                Vector3 position = gameObject.transform.localPosition;
                position.x = UnityEngine.Random.Range(-0.2f, 0.2f);
                position.y = UnityEngine.Random.Range(-0.2f, 0.2f);
                position.z = -0.07f;
                gameObject.transform.localPosition = position;
                
                userTableAddElementObject.moveTargetObject = gameObject.transform;


            }
            userTableAddElementObject.SeMaterial(texturtList[i]);
            userTableAddElementObject.OnInitStart();
        }
        playState = PlayState.None;
    }

    public UserTableController userTableController;
    // 미션2의 생성 표적 위치 지정
    // 접속한 인원수 대비 작동 여부 변경 해야함
    public Transform TargetSelectArea() {
        //가까운 오브젝트 검출
        Transform targetTransform = null;

        bool[] isConnections = GameObjectControl.Instance.connectionUser;
        bool[] isConnectionsTrue = Array.FindAll(isConnections, item => item);
        UserRectAreaDataView userRectAreaDataView = CurrentUser(isConnectionsTrue.Length);
        userRectAreaDataView.createObject.SetActive(true);
         //objectCreationManager.GUILayoutLoad(isConnectionsTrue.Length);
        RectAreaDataView[] rectAreaDataViewList = userRectAreaDataView.rectAreaDataViewList.ToArray();
        int index = -1;
        float saveDis = Vector3.Distance(rectAreaDataViewList[0].transform.position, this.transform.position);
        index = 0;

        for (int j = 0; j < rectAreaDataViewList.Length; j++)
        {
            float dis = Vector3.Distance(rectAreaDataViewList[j].transform.position, this.transform.position);
            if (saveDis < dis)
            {
                index = j;
            }
        }
       
        objectCreationManager.CreateUserObject(userRectAreaDataView,
            objectCreationManager.playIndex);
        ++objectCreationManager.playIndex;

        return targetTransform;
    }



    private UserRectAreaDataView CurrentUser(int userCount) {
        UserRectAreaDataView userRectAreaDataView = null;
        switch (userCount) {
            case 1:
                userRectAreaDataView = objectCreationManager.user1Data;
                break;
            case 2:
                userRectAreaDataView = objectCreationManager.user2Data;
                break;
            case 3:
                userRectAreaDataView = objectCreationManager.user3Data;
                break;
            case 4:
                userRectAreaDataView = objectCreationManager.user4Data;
                break;
            case 5:
                userRectAreaDataView = objectCreationManager.user5Data;
                break;
        }
        return userRectAreaDataView;
    }


    private void OnEnable()
    {
        
        OnInsertElementStart();
    }

    private void OnDisable()
    {
        OnReset();
        _stationState = UserTableSettingBase.StationState.waiting;
        OnRemoveInsertElement();
    }


    public void CreateElement() {
        
        
       // PrrtElmntObject prrtElmntObject= GameObject.Instantiate<PrrtElmntObject>(createElementObject, this.transform);
       // prrtElmntObject.
    }



}
