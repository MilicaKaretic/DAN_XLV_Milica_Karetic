using System;
using System.IO;

namespace DAN_XLV_Milica_Karetic
{
    /// <summary>
    /// Logger clas for logging manager action to file
    /// </summary>
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
