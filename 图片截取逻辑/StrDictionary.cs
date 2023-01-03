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
    /// 替换字符串里面的$R,$G,$N等特殊字符，注意是字符串，多为剧情表格、任务等定制
    /// 例子：
    /// string clientStr = "各种中文字符等"; //这里通常是读剧情表格，里面是直接配置的中文
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

        //字符串替代 找出想要的东西
        string strFullDic = ClientStr.Replace("$R", goodName);
        strFullDic = strFullDic.Replace("$G", "");
        strFullDic = strFullDic.Replace("$N", LoginData.m_sRoleName);
        strFullDic = strFullDic.Replace("#r", "\n"); //支持换行符

        return strFullDic;
    }

    /// <summary>
    /// 解析$R,$G,$N等特殊字符的字典解析函数，除非确定要用，否则使用GetClientDictionaryString函数
    /// 例子：
    /// string dicStr = StrDictionary.GetClientDictionaryString_WithNameSex("#{2344}", element1, element2, element3 ... );   //其中element1等是字符串或者整数
    /// </summary>
    /// <param name="ClientDictionaryStr">
    ///  ClientDictionaryStr为客户端字典号，例如#{5676}
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
    /// 返回翻译完的字典内容，只能用于客户端本地字典的解析
    /// 使用例子：
    /// string dicStr = StrDictionary.GetClientDictionaryString("#{2344}", element1, element2, element3 ... );   //其中element1等是字符串或者整数
    /// 或者
    /// string dicStr2 = StrDictionary.GetClientDictionaryString("#{2344}");  //直接在客户端写字典号
    /// </summary>
    /// <param name="ClientDictionaryStr"></param>
    /// ClientDictionaryStr为客户端字典号，例如#{5676}
    /// <param name="args">
    /// 变参内容，要依据字典中填写的{x}来确定有多少个内容，如果不对应，会报错（解析出的也是乱的）；
    /// </param>
    /// <returns>返回翻译完的字典内容，可以直接显示</returns>
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
        //读表//支持换行符

        return ClientDictionaryStr + " -- ClientDictionary ERROR2";
    }

    /// <summary>
    /// 解析由服务器函数DictionaryFormat::FormatDictionary()返回的包含字典的字符串。
    /// 返回翻译完的字典内容，只能用于从服务器发过来字典的解析
    /// 使用例子：
    /// string serverSendedStr = "#{12345}*hello*newWorld";  //Server发过来的字符串，由Server端的DictionaryFormat::FormatDictionary()生成的。
    /// 或者
    /// string serverSendedStr2 = "#{12345}";  //Server发过来的字符串，就是一个字典号
    /// string dicStr = StrDictionary.GetServerDictionaryFormatString(serverSendedStr); 
    /// string dicStr2 = StrDictionary.GetServerDictionaryFormatString(serverSendedStr2); 
    /// </summary>
    /// <param name="ServerSendedDictionaryStr">
    /// ServerSendedDictionaryStr为服务器发过来的包含字典号的字符串
    /// 格式举例如下：
    /// #{12345}*hello*newWorld
    /// 其中字典#{12345}的内容为：{0}{1}
    /// 上面的两个变量字符串分别为hello,newWorld
    /// 解析完整个字典串返回值为：hello newWorld
    /// </param>
    /// <returns>返回翻译完的字典内容，可以直接显示</returns>
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
        if (dicEndPos > 0) //#{12345}*hello*newWorld         这种格式
        {
            string dictionaryStr = ServerSendedDictionaryStr.Substring(0, dicEndPos);
            string elementStr = ServerSendedDictionaryStr.Substring(dicEndPos + 1, ServerSendedDictionaryStr.Length - dicEndPos - 1);

            string[] allElements = elementStr.Split('*');

            return GetClientDictionaryString(dictionaryStr, allElements);
        }
        else // #{12345} 这种格式
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
    /// 解析由服务器函数DictionaryFormat::FormatDictionary()返回的包含字典的字符串。
    /// 返回翻译完的字典内容，只能用于从服务器发过来字典的解析
    /// 使用例子：
    /// string serverSendedStr = "#{12345}*hello*newWorld";  //Server发过来的字符串，由Server端的DictionaryFormat::FormatDictionary()生成的。
    /// 或者
    /// string serverSendedStr2 = "#{12345}";  //Server发过来的字符串，就是一个字典号
    /// string dicStr = StrDictionary.GetServerDictionaryFormatString(serverSendedStr); 
    /// string dicStr2 = StrDictionary.GetServerDictionaryFormatString(serverSendedStr2); 
    /// </summary>
    /// <param name="ServerSendedDictionaryStr">
    /// ServerSendedDictionaryStr为服务器发过来的包含字典号的字符串
    /// 格式举例如下：
    /// #{12345}*hello*newWorld
    /// 其中字典#{12345}的内容为：{0}{1}
    /// 上面的两个变量字符串分别为hello,newWorld
    /// 解析完整个字典串返回值为：hello newWorld
    /// </param>
    /// <returns>返回翻译完的字典内容，可以直接显示</returns>
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
        if (errorEndPos > 0) //#{12345}*hello*newWorld         这种格式
        {
            string errorStr = ServerErrorStr.Substring(0, errorEndPos);
            string elementStr = ServerErrorStr.Substring(errorEndPos + 1, ServerErrorStr.Length - errorEndPos - 1);

            string[] allElements = elementStr.Split('*');

            return GetClientErrorString(errorStr, allElements);
        }
        else // #{12345} 这种格式
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
    /// 返回翻译完的字典内容，只能用于客户端本地字典的解析
    /// 使用例子：
    /// string dicStr = StrDictionary.GetClientDictionaryString("#{2344}", element1, element2, element3 ... );   //其中element1等是字符串或者整数
    /// 或者
    /// string dicStr2 = StrDictionary.GetClientDictionaryString("#{2344}");  //直接在客户端写字典号
    /// </summary>
    /// <param name="ClientDictionaryStr"></param>
    /// ClientDictionaryStr为客户端字典号，例如#{5676}
    /// <param name="args">
    /// 变参内容，要依据字典中填写的{x}来确定有多少个内容，如果不对应，会报错（解析出的也是乱的）；
    /// </param>
    /// <returns>返回翻译完的字典内容，可以直接显示</returns>
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
