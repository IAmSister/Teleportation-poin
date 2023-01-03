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

    public GameObject m_NewPlayerGuideOffSet;   // ��Ļƫ��
    public UISprite m_NewPlayerGuid;          // ���ֽ�ѧ��
    public BoxCollider m_BoxColliderLeft;           // �󵲰�
    public BoxCollider m_BoxColliderRight;          // �ҵ���
    public BoxCollider m_BoxColliderTop;            // �ϵ���
    public BoxCollider m_BoxColliderBottom;       // �µ���

    public UILabel m_RemindLabel;              // ��ʾ����
    public UISprite m_RemindBackground;        // ��ʾ���ֱ���

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
        // ��ʼ��һ��λ������
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
        // �Զ��رս���
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

        // λ������
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
    /// �жϽ�ͼ��״
    /// </summary>
    void HandMoving()
    {
        if (null == m_HandObject)
        {
            return;
        }

        if (m_MotionType == 0)  // ��Ȧ
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
        else if (m_MotionType == 1) // ֱ�� �ҵ���
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
        else if (m_MotionType == 2) // ��ָ��˸
        {
            TweenAlpha tween = m_HandObject.GetComponent<TweenAlpha>();
            if (tween)
            {
                tween.enabled = true;
            }
        }
        else if (m_MotionType == 3)  // ��Ȧ����1/4
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
    /// ��ʾ���ֽ�ѧ
    /// </summary>
    /// <param name="UIObj">���ֽ�ѧ��λ�� ���ĵ�</param>
    /// <param name="nWidth">���ֽ�ѧ����</param>
    /// <param name="nHeight">���ֽ�ѧ��߶�</param>
    /// <param name="strText">���ֽ�ѧ����������</param>
    /// <param name="TextLocation">���ֽ�ѧ����������λ�ã�����Ϊ��top�� bottom�� left�� right��</param>
    /// <param name="nMotionType">���ֽ�ѧ���˶���ʽ������Ϊ��0--��Ȧ, 1--�ҵ���ֱ��, 2--��ָ��˸, 3--Բ����1/4��</param>
    /// <param name="bIsBoxEnable">���ֽ�ѧ���Ƿ���ʾ������Ϊ��0--����ʾ, 1--��ʾ��</param>
    /// <param name="bIsMaskEnable">���ֽ�ѧ�ɰ��Ƿ���ʾ������Ϊ��0--����ʾ, 1--��ʾ��</param>
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

            // ��
            m_Instance.UpdateHand(curInfo._nWidth, curInfo._nHeight, curInfo._nMotionType);
            // ���ֿ��ɰ�
            m_Instance.UpdateBoxMask(curInfo._UIObj, curInfo._nWidth, curInfo._nHeight, curInfo._bIsBoxEnable, curInfo._bIsMaskEnable);
            // ����������ʾ��λ��
            m_Instance.UpdateRemindLabelPos(curInfo._strText, curInfo._TextLocation);
        }
    }

    void UpdateHand(int nWidth, int nHeight, int nMotionType)
    {
        if (nMotionType == 2)
        {
            return;
        }
        // ��λ��
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
        // ָ����
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
            // ����
            m_NewPlayerGuid.GetComponent<UISprite>().GetComponent<UIWidget>().width = nWidth + 6;
            m_NewPlayerGuid.GetComponent<UISprite>().GetComponent<UIWidget>().height = nHeight + 6;
        }

        // �ɰ�
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
    /// �ر����ֽ�ѧ
    /// </summary>
    public static void CloseWindow()
    {

        // ��ʱ���
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
    /// ����������ʾ��λ��
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

            // ��������ƫ��
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
                default: // Ĭ�ϣ���ƫ��
                    Xoffset -= (Distance + nWidth / 2 + nGridWidth / 2);
                    break;
            }
            m_RemindLabel.transform.localPosition = new Vector3(Xoffset, Yoffset, 0);
            m_RemindBackground.transform.localPosition = new Vector3(Xoffset, Yoffset, 0);
        }
    }
}
