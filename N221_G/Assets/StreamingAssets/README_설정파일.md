# 설정 파일 안내

## 실제로 쓰는 설정은 persistent 폴더에 있다

실행 중에 읽고 쓰는 운영 설정은 아래 폴더에 있다.

```
%USERPROFILE%\AppData\LocalLow\DefaultCompany\R_Test\
```

(회사명/제품명은 Unity `Player Settings`의 Company Name / Product Name. 바꾸면 폴더도 바뀐다.)

이 폴더(StreamingAssets)에 있는 같은 이름의 json은 **기본값 원본**이다.

- 실행했을 때 persistent 폴더에 파일이 **없을 때만** 이 원본을 복사해 간다.
- persistent 폴더에 파일이 **이미 있으면 절대 덮어쓰지 않는다.** 빌드를 새로 덮어써도 현장 설정이 남는다.
- 새 버전에서 설정 항목이 추가되면, 실행할 때 기존 값은 그대로 두고 새 항목만 기본값으로 파일에 추가된다.
- 파일이 깨져 있으면 덮어쓰지 않고 기본값으로 실행하며 로그(Player.log)에 오류를 남긴다.

| 파일 | 내용 | 바꾸는 곳 |
|---|---|---|
| `MissionText.json` | 미션 문구, 정답 범위, 타이머, 점수, 사운드 켜기/끄기, 별만들기→미션1 전환 시간 | ESC 설정창 |
| `port.json` | RS232 포트(COM, 속도, 사용 여부) | 파일 직접 편집 |
| `tcp.json` | TCP 접속 주소/포트 | 파일 직접 편집 |
| `Setting.json` | (현재 항목 없음) | - |
| `field_adjust.json` | 화면 중심/배율/늘리기 보정 | 프로젝터 보정 화면 |
| `table_adjust.json` | 사용자 테이블 위치 보정 | 프로젝터 보정 화면 |
| `default_calibration.json` | 프로젝터 워프(모서리/격자) 보정 | 프로젝터 보정 화면 |

## 소프트웨어 업데이트

- **persistent 저장 방식이 들어간 버전부터는** 새 빌드를 그대로 덮어쓰면 된다. 설정은 persistent 폴더에 남는다.
- **처음 이 방식으로 바꾸는 업데이트 1회만**: 예전 버전은 설정을 이 폴더(StreamingAssets)에 저장했으므로,
  업데이트 전에 예전 빌드의 `<앱>_Data/StreamingAssets/` 에서 위 표의 json을 백업해 두고,
  새 빌드를 설치한 뒤 같은 위치에 다시 넣는다. 처음 실행할 때 persistent 폴더로 복사되고, 새 항목은 자동으로 추가된다.
  (이미 persistent 폴더에 파일이 있으면 복사되지 않으니, 그때는 persistent 폴더 쪽 파일을 바꾼다.)

## 기본값으로 되돌리기

persistent 폴더에서 해당 json을 지우고 다시 실행하면 이 폴더의 원본이 다시 복사된다.

## 개발 시 참고

- 에디터 플레이 모드도 persistent 폴더를 쓴다. 이 폴더의 원본을 고쳐도 persistent에 파일이 이미 있으면 반영되지 않는다.
- 에디터 편집 모드(플레이 중이 아님)의 에디터 도구(GameSettingsPanel 인스펙터, 프로젝터 워프 저장/불러오기)는 이 폴더의 원본을 편집한다.
- 경로 규칙은 `Assets/Script/ConfigPath.cs` 에 있다.
