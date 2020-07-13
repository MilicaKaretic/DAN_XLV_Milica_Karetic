using System.Windows;

namespace DAN_XLV_Milica_Karetic
{
    /// <summary>
    /// Show message class
    /// </summary>
    class ShowMessage
    {
        /// <summary>
        /// Notify user is product stored
        /// </summary>
        /// <param name="message">Message about stored product</param>
        public void ShowMessageToUser(string message)
        {
            MessageBox.Show(message);
        }
    }
}
