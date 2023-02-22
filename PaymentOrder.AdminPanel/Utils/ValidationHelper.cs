namespace PaymentOrder.AdminPanel.Utils
{
    public static class ValidationHelper
    {
        public static string GetRequiredMessage(string propName)
        {
            return $"{propName} mütləq daxil edilməlidir";
        }

        public static string GetProhibitedMessage(string propName)
        {
            return $"{propName} daxil edilə bilməz";
        }

        public static string GetRequiredLength(string propName, int length)
        {
            return $"{propName} mütləq {length} simvol olmalıdır";
        }

        public static string GetRequiredCount(string propName, int count)
        {
            return $"{propName}-dan sonra {count} rəqəm olmalıdır";
        }
        
        public static string GetUniqueMessage(string propName)
        {
            return $"{propName} artıq mövcuddur";
        }
        
        public static string GetLetterMessage(string propName)
        {
            return $"{propName} yalnız hərflərdən ibarət olmalıdır";
        }
        
        public static string GetSymbolMessage(string propName)
        {
            return $"{propName} üçün FİN-da simvol olmamalıdır";
        }
        
        public static string GetSpaceMessage(string propName)
        {
            return $"{propName} probelsiz olmalıdır";
        }
    }
}