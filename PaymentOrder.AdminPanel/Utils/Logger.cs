using System;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace PaymentOrder.AdminPanel.Utils
{
    public static class Logger
    {
        public static void Log(Exception exception)
        {
            Task.Run(() =>
            {
                Thread.Sleep(10000);

                MessageBox.Show("salam");

                if (Directory.Exists(Constants.LogFileFolder) == false)
                {
                    Directory.CreateDirectory(Constants.LogFileFolder);
                }

                using (var writer = File.AppendText(Constants.LogFilePath))
                {
                    writer.WriteLine(DateTime.Now.ToString(CultureInfo.InvariantCulture));
                    writer.WriteLine(exception.ToString());
                    writer.WriteLine();
                    writer.WriteLine();
                }
            });
        }
    }
}
