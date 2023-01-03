using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;
using Games.GlobeDefine;


public class StrDictionary
{
    /// <summary>
    /// �滻�ַ��������$R,$G,$N�������ַ���ע�����ַ�������Ϊ����������ȶ���
    /// ���ӣ�
    /// string clientStr = "���������ַ���"; //����ͨ���Ƕ�������������ֱ�����õ�����
    /// string dicStr = StrDictionary.GetClientString_WithNameSex(clientStr); 
    /// </summary>
    /// <param name="ClientStr"></param>
    /// <returns></returns>
    static public string GetClientString_WithNameSex(string ClientStr)
    {
        if (string.IsNullOrEmpty(ClientStr))
        {
            return "";
        }

        int goodNameID = GlobeVar.INVALID_ID;
        int badNameID = GlobeVar.INVALID_ID;
        if (LoginData.m_sbIsMale)
        {
            goodNameID = 1387;
            badNameID = 1389;
        }
        else
        {
            goodNameID = 1388;
            badNameID = 1390;
        }

        string goodName = "";

        //�ַ������ �ҳ���Ҫ�Ķ���
        string strFullDic = ClientStr.Replace("$R", goodName);
        strFullDic = strFullDic.Replace("$G", "");
        strFullDic = strFullDic.Replace("$N", LoginData.m_sRoleName);
        strFullDic = strFullDic.Replace("#r", "\n"); //֧�ֻ��з�

        return strFullDic;
    }

    /// <summary>
    /// ����$R,$G,$N�������ַ����ֵ��������������ȷ��Ҫ�ã�����ʹ��GetClientDictionaryString����
    /// ���ӣ�
    /// string dicStr = StrDictionary.GetClientDictionaryString_WithNameSex("#{2344}", element1, element2, element3 ... );   //����element1�����ַ�����������
    /// </summary>
    /// <param name="ClientDictionaryStr">
    ///  ClientDictionaryStrΪ�ͻ����ֵ�ţ�����#{5676}
    ///  </param>
    /// <param name="args"></param>
    /// <returns></returns>
    static public string GetClientDictionaryString_WithNameSex(string ClientDictionaryStr, params object[] args)
    {
        string strFullDic = GetClientDictionaryString(ClientDictionaryStr, args);
        return GetClientString_WithNameSex(strFullDic);
    }
    public class Tab_StrDictionary
    {

    }

    /// <summary>
    /// ���ط�������ֵ����ݣ�ֻ�����ڿͻ��˱����ֵ�Ľ���
    /// ʹ�����ӣ�
    /// string dicStr = StrDictionary.GetClientDictionaryString("#{2344}", element1, element2, element3 ... );   //����element1�����ַ�����������
    /// ����
    /// string dicStr2 = StrDictionary.GetClientDictionaryString("#{2344}");  //ֱ���ڿͻ���д�ֵ��
    /// </summary>
    /// <param name="ClientDictionaryStr"></param>
    /// ClientDictionaryStrΪ�ͻ����ֵ�ţ�����#{5676}
    /// <param name="args">
    /// ������ݣ�Ҫ�����ֵ�����д��{x}��ȷ���ж��ٸ����ݣ��������Ӧ���ᱨ����������Ҳ���ҵģ���
    /// </param>
    /// <returns>���ط�������ֵ����ݣ�����ֱ����ʾ</returns>
    static public string GetClientDictionaryString(string ClientDictionaryStr, params object[] args)
    {
        if (string.IsNullOrEmpty(ClientDictionaryStr))
        {
            return "EmptyString -- ClientDictionary ERROR0";
        }

        if (ClientDictionaryStr.Length < 3)
        {
            return ClientDictionaryStr + " -- ClientDictionary ERROR1 Length < 3";
        }
        //#{2221} 2_4= 222
        string dicIdStr = ClientDictionaryStr.Substring(2, ClientDictionaryStr.Length - 3);
        int dicID = Int32.Parse(dicIdStr);
        //����//֧�ֻ��з�

        return ClientDictionaryStr + " -- ClientDictionary ERROR2";
    }

    /// <summary>
    /// �����ɷ���������DictionaryFormat::FormatDictionary()���صİ����ֵ���ַ�����
    /// ���ط�������ֵ����ݣ�ֻ�����ڴӷ������������ֵ�Ľ���
    /// ʹ�����ӣ�
    /// string serverSendedStr = "#{12345}*hello*newWorld";  //Server���������ַ�������Server�˵�DictionaryFormat::FormatDictionary()���ɵġ�
    /// ����
    /// string serverSendedStr2 = "#{12345}";  //Server���������ַ���������һ���ֵ��
    /// string dicStr = StrDictionary.GetServerDictionaryFormatString(serverSendedStr); 
    /// string dicStr2 = StrDictionary.GetServerDictionaryFormatString(serverSendedStr2); 
    /// </summary>
    /// <param name="ServerSendedDictionaryStr">
    /// ServerSendedDictionaryStrΪ�������������İ����ֵ�ŵ��ַ���
    /// ��ʽ�������£�
    /// #{12345}*hello*newWorld
    /// �����ֵ�#{12345}������Ϊ��{0}{1}
    /// ��������������ַ����ֱ�Ϊhello,newWorld
    /// �����������ֵ䴮����ֵΪ��hello newWorld
    /// </param>
    /// <returns>���ط�������ֵ����ݣ�����ֱ����ʾ</returns>
    static public string GetServerDictionaryFormatString(string ServerSendedDictionaryStr)       //#{12345}*hello*newWorld
    {
        if (string.IsNullOrEmpty(ServerSendedDictionaryStr))
        {
            return "EmptyString -- ServerDictionaryFormat ERROR0";
        }

        char firstChar = ServerSendedDictionaryStr[0];
        if (firstChar != '#')
        {
            return ServerSendedDictionaryStr + " -- ServerDictionaryFormat ERROR1 FirstChar is not #";
        }

        int dicEndPos = ServerSendedDictionaryStr.IndexOf('*');
        if (dicEndPos > 0) //#{12345}*hello*newWorld         ���ָ�ʽ
        {
            string dictionaryStr = ServerSendedDictionaryStr.Substring(0, dicEndPos);
            string elementStr = ServerSendedDictionaryStr.Substring(dicEndPos + 1, ServerSendedDictionaryStr.Length - dicEndPos - 1);

            string[] allElements = elementStr.Split('*');

            return GetClientDictionaryString(dictionaryStr, allElements);
        }
        else // #{12345} ���ָ�ʽ
        {
            if (ServerSendedDictionaryStr.Length < 3)
            {
                return ServerSendedDictionaryStr + " -- ServerDictionaryFormat ERROR2 Length < 3";
            }
            string dicIdStr = ServerSendedDictionaryStr.Substring(2, ServerSendedDictionaryStr.Length - 3);
            int dicID = Int32.Parse(dicIdStr);

        }

        return ServerSendedDictionaryStr + " -- ServerDictionaryFormat ERROR3";
    }

    /// <summary>
    /// �����ɷ���������DictionaryFormat::FormatDictionary()���صİ����ֵ���ַ�����
    /// ���ط�������ֵ����ݣ�ֻ�����ڴӷ������������ֵ�Ľ���
    /// ʹ�����ӣ�
    /// string serverSendedStr = "#{12345}*hello*newWorld";  //Server���������ַ�������Server�˵�DictionaryFormat::FormatDictionary()���ɵġ�
    /// ����
    /// string serverSendedStr2 = "#{12345}";  //Server���������ַ���������һ���ֵ��
    /// string dicStr = StrDictionary.GetServerDictionaryFormatString(serverSendedStr); 
    /// string dicStr2 = StrDictionary.GetServerDictionaryFormatString(serverSendedStr2); 
    /// </summary>
    /// <param name="ServerSendedDictionaryStr">
    /// ServerSendedDictionaryStrΪ�������������İ����ֵ�ŵ��ַ���
    /// ��ʽ�������£�
    /// #{12345}*hello*newWorld
    /// �����ֵ�#{12345}������Ϊ��{0}{1}
    /// ��������������ַ����ֱ�Ϊhello,newWorld
    /// �����������ֵ䴮����ֵΪ��hello newWorld
    /// </param>
    /// <returns>���ط�������ֵ����ݣ�����ֱ����ʾ</returns>
    static public string GetServerErrorString(string ServerErrorStr)       //#{12345}*hello*newWorld
    {
        if (string.IsNullOrEmpty(ServerErrorStr))
        {
            return "EmptyString -- ServerDictionaryFormat ERROR0";
        }

        char firstChar = ServerErrorStr[0];
        if (firstChar != '#')
        {
            return ServerErrorStr + " -- ServerDictionaryFormat ERROR1 FirstChar is not #";
        }

        int errorEndPos = ServerErrorStr.IndexOf('*');
        if (errorEndPos > 0) //#{12345}*hello*newWorld         ���ָ�ʽ
        {
            string errorStr = ServerErrorStr.Substring(0, errorEndPos);
            string elementStr = ServerErrorStr.Substring(errorEndPos + 1, ServerErrorStr.Length - errorEndPos - 1);

            string[] allElements = elementStr.Split('*');

            return GetClientErrorString(errorStr, allElements);
        }
        else // #{12345} ���ָ�ʽ
        {
            if (ServerErrorStr.Length < 3)
            {
                return ServerErrorStr + " -- ServerDictionaryFormat ERROR2 Length < 3";
            }
            string errorIdStr = ServerErrorStr.Substring(2, ServerErrorStr.Length - 3);
            int errID = Int32.Parse(errorIdStr);

        }

        return ServerErrorStr + " -- ServerDictionaryFormat ERROR3";
    }

    /// <summary>
    /// ���ط�������ֵ����ݣ�ֻ�����ڿͻ��˱����ֵ�Ľ���
    /// ʹ�����ӣ�
    /// string dicStr = StrDictionary.GetClientDictionaryString("#{2344}", element1, element2, element3 ... );   //����element1�����ַ�����������
    /// ����
    /// string dicStr2 = StrDictionary.GetClientDictionaryString("#{2344}");  //ֱ���ڿͻ���д�ֵ��
    /// </summary>
    /// <param name="ClientDictionaryStr"></param>
    /// ClientDictionaryStrΪ�ͻ����ֵ�ţ�����#{5676}
    /// <param name="args">
    /// ������ݣ�Ҫ�����ֵ�����д��{x}��ȷ���ж��ٸ����ݣ��������Ӧ���ᱨ����������Ҳ���ҵģ���
    /// </param>
    /// <returns>���ط�������ֵ����ݣ�����ֱ����ʾ</returns>
    static public string GetClientErrorString(string ClientErrorStr, params object[] args)
    {
        if (string.IsNullOrEmpty(ClientErrorStr))
        {
            return "EmptyString -- ClientError ERROR0";
        }

        if (ClientErrorStr.Length < 3)
        {
            return ClientErrorStr + " -- ClientError ERROR1 Length < 3";
        }
        string errIdStr = ClientErrorStr.Substring(2, ClientErrorStr.Length - 3);
        int errID = Int32.Parse(errIdStr);


        return ClientErrorStr + " -- ClientError ERROR2";
    }


}
