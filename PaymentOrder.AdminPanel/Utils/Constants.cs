using System;

namespace PaymentOrder.AdminPanel.Utils
{
    class Constants
    {
        public const string DateFormat = "dd.MM.yyyy";

        public const string OperationSuccessMessage = "Operation completed successfully";
        public const string OperationFaultMessage = "Operation completed unsuccessfully";
        public const string ErrorMessage = "Unknown error occured.";
        public const string SureDeleteMessage = "Are you sure you want to delete?";

        public static string LogFileFolder = $@"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\PaymentOrder\";
        public static string LogFilePath = $@"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\PaymentOrder\log.txt";
    }
}
