using UnityEngine;
using System.Collections;
public class NewPlayerGuidLogic : MonoBehaviour
{

    public class PlayerGuideInfo
    {
        public PlayerGuideInfo(GameObject UIObj, int nWidth, int nHeight, string strText, string TextLocation, int nMotionType, bool bIsBoxEnable, bool bIsMaskEnable)
        {
            _UIObj = UIObj;
            _nWidth = nWidth;
            _nHeight = nHeight;
            _strText = strText;
            _TextLocation = TextLocation;
            _nMotionType = nMotionType;
            _bIsBoxEnable = bIsBoxEnable;
            _bIsMaskEnable = bIsMaskEnable;
        }
        public GameObject _UIObj;
        public int _nWidth;
        public int _nHeight;
        public string _strText;
        public string _TextLocation;
        public int _nMotionType;
        public bool _bIsBoxEnable;
        public bool _bIsMaskEnable;
    }

    private static NewPlayerGuidLogic m_Instance = null;
    public static NewPlayerGuidLogic Instance()
    {
        return m_Instance;
    }

    public GameObject m_NewPlayerGuideOffSet;   // 屏幕偏移
    public UISprite m_NewPlayerGuid;          // 新手教学框
    public BoxCollider m_BoxColliderLeft;           // 左挡板
    public BoxCollider m_BoxColliderRight;          // 右挡板
    public BoxCollider m_BoxColliderTop;            // 上挡板
    public BoxCollider m_BoxColliderBottom;       // 下挡板

    public UILabel m_RemindLabel;              // 提示文字
    public UISprite m_RemindBackground;        // 提示文字背景

    public GameObject m_HandObject;

    public float m_AutoCloseWindowTime = 5.0f;
    private static float m_CurCloseTime = 0;

    public float m_IntervalTime;
    private static float m_CurIntervalTime = 0;

    private int m_HandRadius = 0;
    private static float m_Angle = 0;
    private static float m_DisOffSet = 0;

    private int m_MotionType = 0;

    private bool m_OffSetCorrectFlag = false;
    private GameObject m_UIObj = null;

    public static bool IsOpenFlag = false;

    public string CurShowType = "";
    void Awake()
    {
        m_Instance = this;
    }

    
    void Update()
    {
        if (m_UIObj == null)
        {
            return;
        }
        // 开始来一次位置修正
        if (false == m_OffSetCorrectFlag
            || m_NewPlayerGuideOffSet.transform.position != m_UIObj.transform.position)
        {
            if (m_UIObj)
            {
                Vector3 Pos = Vector3.zero;
                if (m_MotionType == 3)
                {
                    Pos.x = -m_HandRadius / 2;
                }
                else
                {
                    Pos.x = m_HandRadius / 2;
                }
                m_HandObject.transform.localPosition = Pos;
                m_NewPlayerGuideOffSet.transform.position = m_UIObj.transform.position;
                m_NewPlayerGuideOffSet.SetActive(true);
            }
            m_OffSetCorrectFlag = true;
        }

        if (m_UIObj == null || m_UIObj.activeInHierarchy == false)
        {
            // NewPlayerGuidLogic.CloseWindow();
        }

    }

    void FixedUpdate()
    {
        // 自动关闭界面
        AutoCloseWindow();

        HandMoving();
    }

    void OnDestroy()
    {
        m_Instance = null;
    }

    void InitWindow(GameObject UIObj)
    {
        //gameObject.SetActive(true);
        //gameObject.layer = UIObj.layer;
        if (UIObj == null)
        {
            return;
        }
        m_NewPlayerGuideOffSet.transform.position = UIObj.transform.position;

        m_NewPlayerGuid.gameObject.SetActive(true);
        m_BoxColliderLeft.gameObject.SetActive(true);
        m_BoxColliderRight.gameObject.SetActive(true);
        m_BoxColliderBottom.gameObject.SetActive(true);
        m_BoxColliderTop.gameObject.SetActive(true);

        m_NewPlayerGuideOffSet.SetActive(false);

        // 位置重置
        m_HandObject.transform.localPosition = Vector3.zero;
        m_BoxColliderLeft.gameObject.transform.localPosition = Vector3.zero;
        m_BoxColliderRight.gameObject.transform.localPosition = Vector3.zero;
        m_BoxColliderTop.gameObject.transform.localPosition = Vector3.zero;
        m_BoxColliderBottom.gameObject.transform.localPosition = Vector3.zero;

        TweenAlpha tween = m_HandObject.GetComponent<TweenAlpha>();
        if (tween)
        {
            tween.enabled = false;
        }

        m_HandRadius = 0;
        m_CurCloseTime = 0;
        m_CurIntervalTime = 0;
        m_Angle = 0;
        m_MotionType = 0;
        m_OffSetCorrectFlag = false;
        m_UIObj = UIObj;
    }
    /// <summary>
    /// 判断截图形状
    /// </summary>
    void HandMoving()
    {
        if (null == m_HandObject)
        {
            return;
        }

        if (m_MotionType == 0)  // 画圈
        {
            if (m_Angle >= 360 && m_CurIntervalTime < m_IntervalTime)
            {
                m_CurIntervalTime += Time.deltaTime;
                return;
            }
            else if (m_CurIntervalTime >= m_IntervalTime)
            {
                m_Angle = 0;
                m_CurIntervalTime = 0;
            }

            m_Angle += 180 * Time.deltaTime;
            m_HandObject.transform.RotateAround(m_NewPlayerGuideOffSet.transform.position, Vector3.forward, 180 * Time.deltaTime);
            m_HandObject.transform.localRotation = Quaternion.identity;

        }
        else if (m_MotionType == 1) // 直线 右到左
        {
            if (m_DisOffSet >= m_HandRadius / 2 && m_CurIntervalTime < m_IntervalTime)
            {
                m_CurIntervalTime += Time.deltaTime;
                return;
            }
            else if (m_CurIntervalTime >= m_IntervalTime)
            {
                m_DisOffSet = 0;
                m_CurIntervalTime = 0;
                m_HandObject.transform.localPosition += new Vector3(m_HandRadius / 2, 0, 0);
            }
            m_DisOffSet += 100 * Time.deltaTime;
            m_HandObject.transform.localPosition -= new Vector3(100 * Time.deltaTime, 0, 0);
        }
        else if (m_MotionType == 2) // 手指闪烁
        {
            TweenAlpha tween = m_HandObject.GetComponent<TweenAlpha>();
            if (tween)
            {
                tween.enabled = true;
            }
        }
        else if (m_MotionType == 3)  // 画圈左上1/4
        {
            if (m_Angle >= 90 && m_CurIntervalTime < m_IntervalTime)
            {
                m_CurIntervalTime += Time.deltaTime;
                m_HandObject.transform.localPosition = new Vector3(-m_HandRadius / 2, 0, 0);
                return;
            }
            else if (m_CurIntervalTime >= m_IntervalTime)
            {
                m_Angle = 0;
                m_CurIntervalTime = 0;
            }

            m_Angle += 90 * Time.deltaTime;
            m_HandObject.transform.RotateAround(m_NewPlayerGuideOffSet.transform.position, Vector3.back, 90 * Time.deltaTime);
            m_HandObject.transform.localRotation = Quaternion.identity;

        }
    }


    /// <summary>
    /// 显示新手教学
    /// </summary>
    /// <param name="UIObj">新手教学框位置 中心点</param>
    /// <param name="nWidth">新手教学框宽度</param>
    /// <param name="nHeight">新手教学框高度</param>
    /// <param name="strText">新手教学框文字描述</param>
    /// <param name="TextLocation">新手教学框文字描述位置，参数为“top， bottom， left， right”</param>
    /// <param name="nMotionType">新手教学手运动方式，参数为“0--画圈, 1--右到左直线, 2--手指闪烁, 3--圆左上1/4”</param>
    /// <param name="bIsBoxEnable">新手教学框是否显示，参数为“0--不显示, 1--显示”</param>
    /// <param name="bIsMaskEnable">新手教学蒙版是否显示，参数为“0--不显示, 1--显示”</param>
    public static void OpenWindow(GameObject UIObj, int nWidth, int nHeight, string strText, string TextLocation, int nMotionType = 0, bool bIsBoxEnable = false, bool bIsMaskEnable = false)
    {
        if (PlayerPreferenceData.NewPlayerGuideClose)
        {
            return;
        }

        if (null == UIObj)
        {
          
            return;
        }

        PlayerGuideInfo curInfo = new PlayerGuideInfo(UIObj, nWidth, nHeight, strText, TextLocation, nMotionType, bIsBoxEnable, bIsMaskEnable);
        IsOpenFlag = true;

       // UIManager.ShowUI(UIInfo.NewPlayerGuidRoot, OnOpenWindow, curInfo);

    }

    static void OnOpenWindow(bool bSuccess, object info)
    {
        if (bSuccess == false)
        {
            return;
        }

        PlayerGuideInfo curInfo = info as PlayerGuideInfo;
        if (null != m_Instance && null != curInfo)
        {
            m_Instance.InitWindow(curInfo._UIObj);

            // 手
            m_Instance.UpdateHand(curInfo._nWidth, curInfo._nHeight, curInfo._nMotionType);
            // 新手框、蒙版
            m_Instance.UpdateBoxMask(curInfo._UIObj, curInfo._nWidth, curInfo._nHeight, curInfo._bIsBoxEnable, curInfo._bIsMaskEnable);
            // 更新文字提示的位置
            m_Instance.UpdateRemindLabelPos(curInfo._strText, curInfo._TextLocation);
        }
    }

    void UpdateHand(int nWidth, int nHeight, int nMotionType)
    {
        if (nMotionType == 2)
        {
            return;
        }
        // 手位置
        m_MotionType = nMotionType;
        if (nWidth > nHeight)
        {
            m_HandRadius = nHeight;
        }
        else
            m_HandRadius = nWidth;

        if (m_HandObject && m_NewPlayerGuid)
        {
            Vector3 Pos = Vector3.zero;
            if (nMotionType == 3)
            {
                Pos.x = -m_HandRadius / 2;
            }
            else
            {
                Pos.x = m_HandRadius / 2;
            }
            m_HandObject.transform.localPosition = Pos;
        }
    }

    void UpdateBoxMask(GameObject UIObj, int nWidth, int nHeight, bool bIsBoxEnable, bool bIsMaskEnable)
    {
        // 指引框
        if (m_NewPlayerGuid == null)
        {
            return;
        }
        if (false == bIsBoxEnable)
        {
            m_NewPlayerGuid.gameObject.SetActive(false);
        }
        else
        {
            // 方框
            m_NewPlayerGuid.GetComponent<UISprite>().GetComponent<UIWidget>().width = nWidth + 6;
            m_NewPlayerGuid.GetComponent<UISprite>().GetComponent<UIWidget>().height = nHeight + 6;
        }

        // 蒙版
        if (m_NewPlayerGuid == null)
        {
            return;
        }
        if (false == bIsMaskEnable)
        {
            m_BoxColliderLeft.gameObject.SetActive(false);
            m_BoxColliderRight.gameObject.SetActive(false);
            m_BoxColliderBottom.gameObject.SetActive(false);
            m_BoxColliderTop.gameObject.SetActive(false);
        }
        else
        {
            m_BoxColliderLeft.gameObject.transform.localPosition = new Vector3(-1000 - nWidth / 2, -1000 + nHeight / 2, 0);
            m_BoxColliderRight.gameObject.transform.localPosition = new Vector3(1000 + nWidth / 2, 1000 - nHeight / 2, 0);
            m_BoxColliderTop.gameObject.transform.localPosition = new Vector3(-1000 + nWidth / 2, 1000 + nHeight / 2, 0);
            m_BoxColliderBottom.gameObject.transform.localPosition = new Vector3(1000 - nWidth / 2, -1000 - nHeight / 2, 0);


        }
    }


    public event System.Action TutorialHandClose;
    /// <summary>
    /// 关闭新手教学
    /// </summary>
    public static void CloseWindow()
    {

        // 临时添加
        if (IsOpenFlag == false)
        {
            return;
        }

        m_CurCloseTime = 0;
        m_CurIntervalTime = 0;
        m_Angle = 0;
        m_DisOffSet = 0;
        IsOpenFlag = false;
        if (m_Instance)
        {
            m_Instance.m_HandRadius = 0;
            m_Instance.m_UIObj = null;
            m_Instance.m_HandObject.transform.localPosition = Vector3.zero;
        }
        if (NewPlayerGuidLogic.Instance() != null && NewPlayerGuidLogic.Instance().TutorialHandClose != null)
        {
            NewPlayerGuidLogic.Instance().TutorialHandClose();
        }
        //UIManager.CloseUI(UIInfo.NewPlayerGuidRoot);
    }


    void AutoCloseWindow()
    {
        if (m_CurCloseTime >= m_AutoCloseWindowTime)
        {
            CloseWindow();
            return;
        }
        m_CurCloseTime += Time.deltaTime;
    }

    /// <summary>
    /// 更新文字提示的位置
    /// </summary>
    void UpdateRemindLabelPos(string strText, string TextLocation)
    {
        if (strText == "")
        {
            m_RemindLabel.gameObject.SetActive(false);
            m_RemindBackground.gameObject.SetActive(false);
        }
        else
        {
            m_RemindLabel.gameObject.SetActive(true);
            m_RemindBackground.gameObject.SetActive(true);

            m_RemindLabel.text = strText;
            m_RemindLabel.GetComponent<UILabel>().AssumeNaturalSize();

            int nWidth = m_RemindLabel.GetComponent<UILabel>().GetComponent<UIWidget>().width;
            int nHeight = m_RemindLabel.GetComponent<UILabel>().GetComponent<UIWidget>().height;

            m_RemindBackground.GetComponent<UISprite>().GetComponent<UIWidget>().width = nWidth + 10;
            m_RemindBackground.GetComponent<UISprite>().GetComponent<UIWidget>().height = nHeight + 20;

            // 描述文字偏移
            int Xoffset = 0;
            int Yoffset = 0;
            int Distance = 20;
            int nGridWidth = m_NewPlayerGuid.GetComponent<UISprite>().GetComponent<UIWidget>().width;
            int nGridHeight = m_NewPlayerGuid.GetComponent<UISprite>().GetComponent<UIWidget>().height;

            switch (TextLocation)
            {
                case "top":
                    Yoffset += (Distance + nHeight / 2 + nGridHeight / 2);
                    break;
                case "bottom":
                    Yoffset -= (Distance + nHeight / 2 + nGridHeight / 2);
                    break;
                case "left":
                    Xoffset -= (Distance + nWidth / 2 + nGridWidth / 2);
                    break;
                case "right":
                    Xoffset += (Distance + nWidth / 2 + nGridWidth / 2);
                    break;
                default: // 默认，左偏移
                    Xoffset -= (Distance + nWidth / 2 + nGridWidth / 2);
                    break;
            }
            m_RemindLabel.transform.localPosition = new Vector3(Xoffset, Yoffset, 0);
            m_RemindBackground.transform.localPosition = new Vector3(Xoffset, Yoffset, 0);
        }
    }
}
