

using UnityEngine;
using System.Collections;

public class PlayerPreferenceData
{
    public static int DefalutInt = -1;

    private static string keyAccount = "UserAccount";           // 上次登录的用户名,两个作用，测试登陆，记住上次账号，正常登陆，记录上次的UID  
    private static string keyPsw = "UserPsw";                   // 上次登录的密码
    private static string keyLastServerID = "LastServerID";     // 上次登录的服务器
    private static string keyLastRoleID = "LastRoleID";         // 上次登录玩家的GUID
    private static string keyLeftTabChoose = "LeftTabChoose";   // 玩家左侧标签选择
    private static string keyLeftTabControl = "LeftTabControl"; // 任务追踪折叠标记
    private static string keyDailyMissionGuideFlag = "DailyMissionGuideFlag";   // 日常任务教学标记
    private static string keySystemNameBoard = "SystemNameBoard";               // 是否显示名字板
    private static string keySystemMusic = "SystemMusic";                       // 是否开启音乐
    private static string keySystemSoundEffect = "SystemSoundEffect";           // 是否开启音效
    private static string keySystemTableau = "SystemTableau";                               // 图像品质
    private static string keySystemFloodlight = "SystemFloodlight";                            // 全拼泛光
    private static string keySystemScreenMove = "SystemScreenMovet";                            // 全拼泛光
    private static string keySystemWallVision = "SystemWallVision";                 // 遮挡可见
    private static string keySystemSkillEffect = "SystemSkillEffect";                 // 技能特效
    private static string keySystemDamageBoard = "SystemDamageBoard";                 // 伤害板
    private static string keyChannelConfig_World = "keyChannelConfig_World";
    private static string keyChannelConfig_Tell = "keyChannelConfig_Tell";
    private static string keyChannelConfig_Normal = "keyChannelConfig_Normal";
    private static string keyChannelConfig_Team = "keyChannelConfig_Team";
    private static string keyChannelConfig_Guild = "keyChannelConfig_Guild";
    private static string keyChannelConfig_Master = "keyChannelConfig_Master";
    private static string keyChannelConfig_Friend = "keyChannelConfig_Friend";
    private static string keyChannelConfig_System = "keyChannelConfig_System";
    private static string keyChannelConfig_CloseFriendMenu = "keyChannelConfig_CloseFriendMenu";
    private static string keyAppFirstRun = "AppFirstRun";
    private static string keySystemHideOtherPlayer = "SystemHideOtherPlayer";
    private static string keyUserID = "userid";
    private static string keyCMBIUserID = "CYMG_KEY_userid";
    private static string keyShowFashion = "ShowFashion";
    private static string keyBelleActiveTip = "keyBelleActiveTip";
    private static string keyNewPlayerGuideClose = "keyNewPlayerGuideClose";
    private static string keyDeathPushSetting = "keyDeathPushSetting";
    private static string keyKillNpcExpSetting = "keyKillNpcExpSetting";

    private static string keyXPNewPlayerGuide = "keyXPNewPlayerGuide";    //Xp新手指引标记
    private static string keySystemIsPushRestaurant = "SystemIsPushRestaurant";                 // 遮挡可见

    public static string LastAccount
    {
        set
        {
            PlayerPrefs.SetString(keyAccount, value);
            PlayerPrefs.Save();
        }
        get
        {
            return PlayerPrefs.GetString(keyAccount, null);
        }
    }

    public static string LastPsw
    {
        set
        {
            PlayerPrefs.SetString(keyPsw, value);
            PlayerPrefs.Save();
        }
        get
        {
            return PlayerPrefs.GetString(keyPsw, null);
        }
    }

    public static int LastServer
    {
        set
        {
            PlayerPrefs.SetInt(keyLastServerID, value);
            PlayerPrefs.Save();
        }
        get
        {

            return PlayerPrefs.GetInt(keyLastServerID, 109);
        }
    }

    public static ulong LastRoleGUID
    {
        set
        {
            PlayerPrefs.SetString(keyLastRoleID, value.ToString());
            PlayerPrefs.Save();
        }
        get
        {
            string retStr = PlayerPrefs.GetString(keyLastRoleID, null);
            if (null == retStr) return 0;
            ulong retValue;
            if (ulong.TryParse(retStr, out retValue))
            {
                return retValue;
            }
            return 0;
        }
    }

    public static int LeftTabChoose
    {
        set
        {
            PlayerPrefs.SetInt(keyLeftTabChoose, value);
            PlayerPrefs.Save();
        }
        get
        {
            return PlayerPrefs.GetInt(keyLeftTabChoose, 0);
        }
    }

    public static int LeftTabControl
    {
        set
        {
            PlayerPrefs.SetInt(keyLeftTabControl, value);
            PlayerPrefs.Save();
        }
        get
        {
            return PlayerPrefs.GetInt(keyLeftTabControl, 0);
        }
    }

    public static int DailyMissionGuideFlag
    {
        set
        {
            PlayerPrefs.SetInt(keyDailyMissionGuideFlag, value);
            PlayerPrefs.Save();
        }
        get
        {
            return PlayerPrefs.GetInt(keyDailyMissionGuideFlag, 0);
        }
    }
    public static int SystemNameBoard
    {
        set
        {
            PlayerPrefs.SetInt(keySystemNameBoard, value);
            PlayerPrefs.Save();
        }
        get
        {
            return PlayerPrefs.GetInt(keySystemNameBoard, 1);
        }
    }
    public static int SystemMusic
    {
        set
        {
            PlayerPrefs.SetInt(keySystemMusic, value);
            PlayerPrefs.Save();
        }
        get
        {
            return PlayerPrefs.GetInt(keySystemMusic, 1);
        }
    }
    public static int SystemSoundEffect
    {
        set
        {
            PlayerPrefs.SetInt(keySystemSoundEffect, value);
            PlayerPrefs.Save();
        }
        get
        {
            return PlayerPrefs.GetInt(keySystemSoundEffect, 1);
        }
    }
    public static int SystemTableau
    {
        set
        {
            PlayerPrefs.SetInt(keySystemTableau, value);
            PlayerPrefs.Save();
        }
        get
        {
            //做ios和android处理
            //             #if !UNITY_EDITOR && UNITY_IPHONE   
            //                 return PlayerPrefs.GetInt(keySystemTableau, 1);
            //             #else
            return PlayerPrefs.GetInt(keySystemTableau, 0); //做特殊处理
                                                            //            #endif
        }
    }
    public static int SystemFloodlight
    {
        set
        {
            PlayerPrefs.SetInt(keySystemFloodlight, value);
            PlayerPrefs.Save();
        }
        get
        {
            // 10为开,11为关
            int nFloodlight = PlayerPrefs.GetInt(keySystemFloodlight, 1);


            return nFloodlight;
        }
    }
    public static int SystemScreenMove
    {
        set
        {
            PlayerPrefs.SetInt(keySystemScreenMove, value);
            PlayerPrefs.Save();
        }
        get
        {
            return PlayerPrefs.GetInt(keySystemScreenMove, 1);
        }
    }

    public static int SystemShowOtherPlayerCount
    {
        set
        {
            PlayerPrefs.SetInt(keySystemHideOtherPlayer, value);
            PlayerPrefs.Save();
        }
        get
        {
            return PlayerPrefs.GetInt(keySystemHideOtherPlayer, 15);
        }
    }

    public static bool SystemWallVisionEnable
    {
        set
        {
            PlayerPrefs.SetInt(keySystemWallVision, value ? 1 : 0);
            PlayerPrefs.Save();
        }
        get
        {
            return PlayerPrefs.GetInt(keySystemWallVision, 1) > 0;
        }
    }

    public static bool SystemSkillEffectEnable
    {
        set
        {
            PlayerPrefs.SetInt(keySystemSkillEffect, value ? 1 : 0);
            PlayerPrefs.Save();
        }
        get
        {
            return PlayerPrefs.GetInt(keySystemSkillEffect, 1) > 0;
        }
    }

    public static bool SystemDamageBoardEnable
    {
        set
        {
            PlayerPrefs.SetInt(keySystemDamageBoard, value ? 1 : 0);
            PlayerPrefs.Save();
        }
        get
        {
            return PlayerPrefs.GetInt(keySystemDamageBoard, 1) > 0;
        }
    }

    public static int ChannelConfig_World
    {
        set
        {
            PlayerPrefs.SetInt(keyChannelConfig_World, value);
            PlayerPrefs.Save();
        }
        get
        {
            return PlayerPrefs.GetInt(keyChannelConfig_World, 11000011);
        }
    }

    public static int ChannelConfig_Tell
    {
        set
        {
            PlayerPrefs.SetInt(keyChannelConfig_Tell, value);
            PlayerPrefs.Save();
        }
        get
        {
            return PlayerPrefs.GetInt(keyChannelConfig_Tell, 10);
        }
    }

    public static int ChannelConfig_Normal
    {
        set
        {
            PlayerPrefs.SetInt(keyChannelConfig_Normal, value);
            PlayerPrefs.Save();
        }
        get
        {
            return PlayerPrefs.GetInt(keyChannelConfig_Normal, 110);
        }
    }

    public static int ChannelConfig_Team
    {
        set
        {
            PlayerPrefs.SetInt(keyChannelConfig_Team, value);
            PlayerPrefs.Save();
        }
        get
        {
            return PlayerPrefs.GetInt(keyChannelConfig_Team, 1010);
        }
    }

    public static int ChannelConfig_Guild
    {
        set
        {
            PlayerPrefs.SetInt(keyChannelConfig_Guild, value);
            PlayerPrefs.Save();
        }
        get
        {
            return PlayerPrefs.GetInt(keyChannelConfig_Guild, 10010);
        }
    }
    public static int ChannelConfig_Master
    {
        set
        {
            PlayerPrefs.SetInt(keyChannelConfig_Master, value);
            PlayerPrefs.Save();
        }
        get
        {
            return PlayerPrefs.GetInt(keyChannelConfig_Master, 100010);
        }
    }

    public static int ChannelConfig_Friend
    {
        set
        {
            PlayerPrefs.SetInt(keyChannelConfig_Friend, value);
            PlayerPrefs.Save();
        }
        get
        {
            return PlayerPrefs.GetInt(keyChannelConfig_Friend, 1000000);
        }
    }

    public static int ChannelConfig_System
    {
        set
        {
            PlayerPrefs.SetInt(keyChannelConfig_System, value);
            PlayerPrefs.Save();
        }
        get
        {
            return PlayerPrefs.GetInt(keyChannelConfig_System, 10000010);
        }
    }

    public static int ChannelConfig_CloseFriendMenu
    {
        set
        {
            PlayerPrefs.SetInt(keyChannelConfig_CloseFriendMenu, value);
            PlayerPrefs.Save();
        }
        get
        {
            return PlayerPrefs.GetInt(keyChannelConfig_CloseFriendMenu, 1);
        }
    }

    public static bool IsAppFirstRun
    {
        set
        {
            PlayerPrefs.SetInt(keyAppFirstRun, value ? 1 : 0);
            PlayerPrefs.Save();
        }
        get
        {
            return PlayerPrefs.GetInt(keyAppFirstRun, 1) > 0;
        }
    }

    public static string UserID
    {
        set
        {
            PlayerPrefs.SetString(keyUserID, value);
            PlayerPrefs.SetString(keyCMBIUserID, value);
            PlayerPrefs.Save();
        }
    }

    public static bool ShowFashion
    {
        set
        {
            PlayerPrefs.SetInt(keyShowFashion, value ? 1 : 0);
            PlayerPrefs.Save();
        }
        get
        {
            return PlayerPrefs.GetInt(keyShowFashion, 0) > 0;
        }
    }

    public static int BelleActiveTipCount
    {
        set
        {
            PlayerPrefs.SetInt(keyBelleActiveTip, value);
            PlayerPrefs.Save();
        }
        get
        {
            return PlayerPrefs.GetInt(keyBelleActiveTip, 0);
        }
    }

    public static bool NewPlayerGuideClose
    {
        set
        {
            PlayerPrefs.SetInt(keyNewPlayerGuideClose, value ? 1 : 0);
            PlayerPrefs.Save();
        }
        get
        {
            return PlayerPrefs.GetInt(keyNewPlayerGuideClose, 0) > 0;
        }
    }

    public static bool DeathPushDisable
    {
        set
        {
            PlayerPrefs.SetInt(keyDeathPushSetting, value ? 1 : 0);
            PlayerPrefs.Save();
        }
        get
        {
            return PlayerPrefs.GetInt(keyDeathPushSetting, 1) > 0;
        }
    }
    public static bool KillNpcExp
    {
        set
        {
            PlayerPrefs.SetInt(keyKillNpcExpSetting, value ? 1 : 0);
            PlayerPrefs.Save();
        }
        get
        {
            return PlayerPrefs.GetInt(keyKillNpcExpSetting, 1) > 0;
        }
    }
    
    public static bool XPNewPlayerGuideFlag
    {
        set
        {
            PlayerPrefs.SetInt(keyXPNewPlayerGuide, value ? 1 : 0);
            PlayerPrefs.Save();
        }
        get
        {
            return PlayerPrefs.GetInt(keyXPNewPlayerGuide, 0) > 0;
        }
    }
    public static bool SystemIsPushRestaurant
    {
        set
        {
            PlayerPrefs.SetInt(keySystemIsPushRestaurant, value ? 1 : 0);
            PlayerPrefs.Save();
        }
        get
        {
            return PlayerPrefs.GetInt(keySystemIsPushRestaurant, 1) > 0;
        }
    }

 

}

