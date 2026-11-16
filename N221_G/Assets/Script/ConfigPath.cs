using System;
using System.IO;
using UnityEngine;

/// <summary>
/// 운영 설정 JSON 경로 도우미.
///
/// - 실행 중(빌드, 에디터 플레이 모드)에는 Application.persistentDataPath 의 파일을 읽고 쓴다.
///   빌드를 새로 덮어써도 현장에서 바꾼 값이 남게 하기 위해서다.
/// - StreamingAssets 의 같은 이름 파일은 "기본값 원본"이다. persistent 쪽에 파일이 없을 때만 복사해 오고,
///   이미 있으면 절대 덮어쓰지 않는다.
/// - 에디터에서 플레이 중이 아닐 때(에디터 도구로 기본값을 편집할 때)는 StreamingAssets 원본 경로를 돌려준다.
///
/// 새로 추가된 설정 항목은 각 로더가 파일을 읽은 뒤 기본값을 채워 다시 저장하므로 persistent 파일에 자동으로 추가된다.
/// </summary>
public static class ConfigPath
{
    /// <summary>StreamingAssets 의 기본값 원본 경로</summary>
    public static string Default(string fileName)
    {
        return Path.Combine(Application.streamingAssetsPath, fileName);
    }

    /// <summary>persistentDataPath 의 실제 운영 파일 경로 (복사하지 않음)</summary>
    public static string Persistent(string fileName)
    {
        return Path.Combine(Application.persistentDataPath, fileName);
    }

    /// <summary>
    /// 읽고 쓸 설정 파일 경로. 실행 중이면 persistent 경로를 돌려주고,
    /// 그곳에 파일이 없으면 StreamingAssets 원본을 먼저 복사해 둔다.
    /// </summary>
    public static string Get(string fileName)
    {
#if UNITY_EDITOR
        if (!Application.isPlaying) return Default(fileName);
#endif
        string target = Persistent(fileName);
        if (File.Exists(target)) return target;

        string source = Default(fileName);
        if (!File.Exists(source))
        {
            // 원본도 없으면 로더가 기본값으로 새로 만든다.
            return target;
        }

        try
        {
            string dir = Path.GetDirectoryName(target);
            if (!string.IsNullOrEmpty(dir)) Directory.CreateDirectory(dir);
            File.Copy(source, target, false);
            Debug.Log("[ConfigPath] 기본 설정 복사: " + source + " → " + target);
            return target;
        }
        catch (Exception e)
        {
            // 복사에 실패하면 원본을 그대로 읽게 해 앱이 멈추지 않게 한다.
            Debug.LogError("[ConfigPath] 기본 설정 복사 실패 (" + fileName + "): " + e.Message + " → StreamingAssets 원본 사용");
            return source;
        }
    }
}
