using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WaitAllOneByOne
{
    class  Program
    {        
        static void Main(string[] args)
        {
            Dictionary<string, string> lista = Tarea.obtenerIp();
            List<Task<Boolean>> tareas = new List<Task<Boolean>>();
            try
            {
                foreach (KeyValuePair<string, string> par in lista)
                {
                    Task<Boolean> t = Task.Factory.StartNew<Boolean>(() =>
                    {
                        return Tarea.estado(par.Key);
                    });
                    tareas.Add(t);
                }
            }catch(AggregateException e)
            {
                
                Console.WriteLine(e.Message);
            
            }
            int cantTrue = 0;
            int cantFalse = 0;            
            while (tareas.Count>0)
            {
                int indice = Task.WaitAny(tareas.ToArray());
                Task<Boolean> primera = tareas[indice];
                if (primera.Result)
                {
                    cantTrue++;
                }else
                {
                    cantFalse++;
                }

                Console.WriteLine("Indice {0} Id {1} Resultado {2}",indice,primera.Id,primera.Result);
                tareas.RemoveAt(indice);
             
            }
            Console.WriteLine("Cant conexiones exitosas: {0}",cantTrue);
            Console.WriteLine("Cant conexiones fallidas: {0}", cantFalse);

            Console.ReadKey();
        }




    }

    
    
}
