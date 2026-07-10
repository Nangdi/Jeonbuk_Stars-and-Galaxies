using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using static UserTableSettingControl;

public class UserTableSettingControl : UserTableSettingBase
{

    //사용자 테이블 셋팅
    [Header("사용자 테이블 셋팅")]
    public UserTableSetting userTableSetting;

    [System.Serializable]
    public class UserTableSetting
    {
        //사용자 화살표 컨트롤 렌더러
        public Renderer userArrowRenderer;

        public List<Texture2D> textureList;
        public MaterialPropertyBlock userMaterialPropertyBlock;

        [Header("사용자 화살표 에니메이션 설정")] 
        public Animator userArrowAnimator;
        [Header("사용자 상태 정보")]
        public UserState userState;
        public Coroutine userStateCoroutine = null;

    }

    //사용자 상태 
    public enum UserState { 
        watiting    /*대기*/, 
        Connection  /*연동*/,
        Playing     /*시작*/
    }


    //시작 컨트롤
    public override void OnInit()
    {
        base.OnInit();
        OnInitUserTableSetting(); 
        SetUserArrow();
    }

    //테이블 셋팅 설정
    public void OnInitUserTableSetting() {
        userTableSetting.userMaterialPropertyBlock = new MaterialPropertyBlock();
    }

    //사용자 화면 화살표 셋팅 구간
    public void SetUserArrow() {

        Texture2D _texture=userTableSetting.textureList[(int)starIndex-1];
        //userTableSetting.userMaterialPropertyBlock.SetTexture("_MainTex", _texture);
        userTableSetting.userMaterialPropertyBlock.SetTexture("_BaseMap", _texture);
        userTableSetting.userArrowRenderer.SetPropertyBlock(userTableSetting.userMaterialPropertyBlock);
       
    }


    private void UserStateCoroutineStart() { 
        
    }


    IEnumerator OnUserStateCoroutine() { 
        
        while (true) {
            yield return new WaitUntil(()=> userTableSetting.userState.Equals(UserState.Playing));
        }
    }

    //화살표 에니메이션 작동 시작
    private void AnimatorStart() {
        userTableSetting.userArrowAnimator.SetBool("IsPlay",true);
    }
    //화살표 에니메이션 작동 멈춤
    private void AnimatorStop() {
        userTableSetting.userArrowAnimator.SetBool("IsPlay", false);
    }


    

}
