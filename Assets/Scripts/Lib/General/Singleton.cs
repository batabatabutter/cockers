using UnityEngine;
using System.Collections;

//=====================================================================================
// シングルトン
//=====================================================================================
public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    //-------------------------------------------------------------------------------------
    // インスタンス取得
    //-------------------------------------------------------------------------------------
    public static T GetInstance()
    {
        if (m_Instance == null)
        {
            m_Instance = (T)(GameObject.FindObjectOfType(typeof(T)));
        }
        return m_Instance;
    }
    //-------------------------------------------------------------------------------------
    // インスタンス取得（スレッドからシングルトンにアクセスする時用にFindをしない版）
    //-------------------------------------------------------------------------------------
    public static T GetInstanceWithoutFinding()
    {
        return m_Instance;
    }

    //-------------------------------------------------------------------------------------
    // 初期化処理（継承先で必ず記述）
    //-------------------------------------------------------------------------------------
    protected abstract void Initialize();

    //-------------------------------------------------------------------------------------
    // Awake
    //-------------------------------------------------------------------------------------
    private void Awake()
    {
        if (m_Instance != null && m_Instance != (T)this)
        {
            Destroy(gameObject);
            return;
        }

        // 初期化処理を呼ぶ
        Initialize();

        // オブジェクトを永続キープ
        DontDestroyOnLoad(gameObject);

        m_Instance = (T)this;
    }

    //=====================================================================================
    // property
    //=====================================================================================
    public static T I
    {
        get { return GetInstance(); }
    }
    public static T WFI
    {
        get { return GetInstanceWithoutFinding(); }
    }

    //=====================================================================================
    // member
    //=====================================================================================
    private static T m_Instance = null;
}