using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.NetworkInformation;

namespace WaitAllOneByOne
{    
    public static  class Tarea
    {
        public static Dictionary<string, string> obtenerIp()
        {
            Dictionary<string, string> listaIp = new Dictionary<string, string>();
            try
            {
                using (StreamReader leer = new StreamReader("ip.csv"))
                {
                    string linea = null;
                    while (null != (linea = leer.ReadLine()))
                    {

                        string[] valor = linea.Split(';');
                        if (!listaIp.ContainsKey(valor[0]))
                        {
                            listaIp.Add(valor[0], valor[1]);
                        }
                    }
                    // texto = File.ReadAllLines("ip.csv");
                }
            }
            catch(IOException e)
            {
                Console.WriteLine("Error: {}",e.Message);
            }
            return listaIp;
        }
        
        public static Boolean estado(string ip)
        {
            Boolean estado = false;
            Ping pings= new Ping();
            int timeout = 12;
            try
            {
                if (pings.Send(ip, timeout).Status == IPStatus.Success)
                {
                    
                    estado = true;
                }
                else
                {
               
                    estado = false;
                }
            }catch(PingException p)
            {
                Console.WriteLine("Error: {0}",p.Message);
            }
            return estado;

        }
    }
}
