using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAN_XLV_Milica_Karetic
{
    class Logger
    {

        private string fileName = @"..\..\ManagerAction.txt";

        /// <summary>
        /// Log message to file
        /// </summary>
        /// <param name="action">Message action</param>
        public void WriteActionToFile(string action)
        {

            using (StreamWriter sw = new StreamWriter(fileName, append:true))
            {
                sw.WriteLine((DateTime.Now.ToString("dd.MM.yyyy HH:mm") + " ----> " + action).ToString());
            }
        }
    }
}
