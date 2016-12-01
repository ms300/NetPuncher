using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.NetworkInformation;

namespace NetPuncher
{
    class ip
    {
        public int IPToNumber(string strIPAddress)
        {
            //将目标IP地址字符串strIPAddress转换为数字
            string[] arrayIP = strIPAddress.Split('.');
            int sip1 = Int32.Parse(arrayIP[0]);
            int sip2 = Int32.Parse(arrayIP[1]);
            int sip3 = Int32.Parse(arrayIP[2]);
            int sip4 = Int32.Parse(arrayIP[3]);
            int tmpIpNumber;
            tmpIpNumber = sip1 * 256 * 256 * 256 + sip2 * 256 * 256 + sip3 * 256 + sip4;
            return tmpIpNumber;
        }


        /// <summary>
        /// 将int型表示的IP还原成正常IPv4格式。
        /// </summary>
        /// <param name="intIPAddress">int型表示的IP</param>
        /// <returns></returns>
        public string NumberToIP(int intIPAddress)
        {
            int tempIPAddress;
            //将目标整形数字intIPAddress转换为IP地址字符串
            //-1062731518 192.168.1.2 
            //-1062731517 192.168.1.3 
            if (intIPAddress >= 0)
            {
                tempIPAddress = intIPAddress;
            }
            else
            {
                tempIPAddress = intIPAddress + 1;
            }
            int s1 = tempIPAddress / 256 / 256 / 256;
            int s21 = s1 * 256 * 256 * 256;
            int s2 = (tempIPAddress - s21) / 256 / 256;
            int s31 = s2 * 256 * 256 + s21;
            int s3 = (tempIPAddress - s31) / 256;
            int s4 = tempIPAddress - s3 * 256 - s31;
            if (intIPAddress < 0)
            {
                s1 = 255 + s1;
                s2 = 255 + s2;
                s3 = 255 + s3;
                s4 = 255 + s4;
            }
            string strIPAddress = s1.ToString() + "." + s2.ToString() + "." + s3.ToString() + "." + s4.ToString();
            return strIPAddress;
        }

        public string ping(string ipaddr)
        {

            Ping p1 = new Ping(); //只是演示，没有做错误处理
            string ret;
            PingReply reply = p1.Send(ipaddr, 500);
            if (reply.Status == IPStatus.Success)
            {
                ret = reply.RoundtripTime.ToString() + "ms";
            }
            else
            {
                ret = "超时";
            }
            //StringBuilder sbuilder;
            //if (reply.Status == IPStatus.Success)
            //{
            //    sbuilder = new stringbuilder();
            //    sbuilder.append(string.format("address: {0} ", reply.address.tostring()));
            //    sbuilder.append(string.format("roundtrip time: {0} ", reply.roundtriptime));
            //    sbuilder.append(string.format("time to live: {0} ", reply.options.ttl));
            //    sbuilder.append(string.format("don't fragment: {0} ", reply.options.dontfragment));
            //    sbuilder.append(string.format("buffer size: {0} ", reply.buffer.length));
            //    response.write(sbuilder.ToString());
            //}
            //else if (reply.Status == IPStatus.TimeOut)
            //{
            //    response.write("超时");
            //}
            //else
            //{
            //    response.write("失败");
            //}
            return ret;
        }
    }
}
