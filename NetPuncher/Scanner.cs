using NetPuncher;
using System.Net.Sockets;

class Scanner
{
    string m_host;
    int m_port;

    public Scanner(string host, int port)
    {
        m_host = host;
        m_port = port;
    }
    public bool Scan()
    {
        TcpClient tc = new TcpClient();
        tc.SendTimeout = tc.ReceiveTimeout = 500;
        bool ret=false;
        try
        {
            tc.Connect(m_host, m_port);
            if (tc.Connected)
            {
                // Console.WriteLine("Port {0} is Open", m_port.ToString().PadRight(6));
                ret = true;
            }
        }
        catch
        {
            //Console.WriteLine("Port {0} is Closed", m_port.ToString().PadRight(6));
            ret = false;
        }
        finally
        {
            tc.Close();
            tc = null;

            ;
        }
        return ret;
    }

} 