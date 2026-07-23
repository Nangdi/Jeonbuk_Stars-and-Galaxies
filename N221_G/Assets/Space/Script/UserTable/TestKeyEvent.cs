using System.Collections;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.IO;
using UnityEngine;

public class TestKeyEvent : MonoBehaviour
{
    public CommandConnection commandConnection;
    public CommandConnection commandConnection2;
    public CommandConnection commandConnection3;
    public CommandConnection commandConnection4;
    public CommandConnection commandConnection5;

    public enum CommandType { BallControl,OnReset}
    public CommandType commandType;
    // Update is called once per frame
    void Update()
    {
        if (DebugInputLock.Locked) return; // 프로젝터 설정창 열림 중 디버그 입력 잠금
        switch (commandType) {

            case CommandType.BallControl:
                if (Input.GetKeyUp(KeyCode.Alpha1))
                {
                    commandConnection.OnInsertBall();
                }

                if (Input.GetKeyUp(KeyCode.Alpha2))
                {
                    commandConnection.OnRotatingDevice();
                }


                if (Input.GetKeyUp(KeyCode.Alpha3))
                {
                    commandConnection2.OnInsertBall();
                }
                if (Input.GetKeyUp(KeyCode.Alpha4))
                {
                    commandConnection2.OnRotatingDevice();
                }


                if (Input.GetKeyUp(KeyCode.Alpha5))
                {
                    commandConnection3.OnInsertBall();
                }
                if (Input.GetKeyUp(KeyCode.Alpha6))
                {
                    commandConnection3.OnRotatingDevice();
                }


                if (Input.GetKeyUp(KeyCode.Alpha7))
                {
                    commandConnection4.OnInsertBall();
                }
                if (Input.GetKeyUp(KeyCode.Alpha8))
                {
                    commandConnection4.OnRotatingDevice();
                }

                if (Input.GetKeyUp(KeyCode.Alpha9))
                {
                    commandConnection5.OnInsertBall();
                }
                if (Input.GetKeyUp(KeyCode.Alpha0))
                {
                    commandConnection5.OnRotatingDevice();
                }

                break;


            case CommandType.OnReset:
                if (Input.GetKeyDown(KeyCode.Alpha1)) {
                    commandConnection.userTableController.ReEndPointBallStartCall();
                }
                if (Input.GetKeyUp(KeyCode.Alpha1))
                {
                    commandConnection.userTableController.OnStopReEndPointBall();
                }

                if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    commandConnection2.userTableController.ReEndPointBallStartCall();
                }
                if (Input.GetKeyUp(KeyCode.Alpha2))
                {
                    commandConnection2.userTableController.OnStopReEndPointBall();
                }

                if (Input.GetKeyDown(KeyCode.Alpha3))
                {
                    commandConnection3.userTableController.ReEndPointBallStartCall();
                }
                if (Input.GetKeyUp(KeyCode.Alpha3))
                {
                    commandConnection3.userTableController.OnStopReEndPointBall();
                }

                if (Input.GetKeyDown(KeyCode.Alpha4))
                {
                    commandConnection4.userTableController.ReEndPointBallStartCall();
                }
                if (Input.GetKeyUp(KeyCode.Alpha4))
                {
                    commandConnection4.userTableController.OnStopReEndPointBall();
                }

                if (Input.GetKeyDown(KeyCode.Alpha5))
                {
                    commandConnection5.userTableController.ReEndPointBallStartCall();
                }
                if (Input.GetKeyUp(KeyCode.Alpha5))
                {
                    commandConnection5.userTableController.OnStopReEndPointBall();
                }


                /* if (Input.GetKeyUp(KeyCode.Alpha1))
                 {
                     commandConnection.OnEndPointBallStart();
                 }
                 if (Input.GetKeyUp(KeyCode.Alpha2))
                 {
                     commandConnection2.OnEndPointBallStart();
                 }
                 if (Input.GetKeyUp(KeyCode.Alpha3))
                 {
                     commandConnection3.OnEndPointBallStart();
                 }
                 if (Input.GetKeyUp(KeyCode.Alpha4))
                 {
                     commandConnection4.OnEndPointBallStart();
                 }
                 if (Input.GetKeyUp(KeyCode.Alpha5))
                 {
                     commandConnection5.OnEndPointBallStart();
                 }*/
                break;
        }
        
    }
}
