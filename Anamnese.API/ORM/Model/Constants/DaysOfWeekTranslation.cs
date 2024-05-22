using System;
using System.Collections.Generic;

namespace Anamnese.API.Application.Utilities
{
    public static class DayOfWeekTranslator
    {
        private static readonly Dictionary<string, string> DayOfWeekTranslations = new Dictionary<string, string>
        {
            { "Monday", "Segunda-feira" },
            { "Tuesday", "Terça-feira" },
            { "Wednesday", "Quarta-feira" },
            { "Thursday", "Quinta-feira" },
            { "Friday", "Sexta-feira" },
            { "Saturday", "Sábado" },
            { "Sunday", "Domingo" }
        };

        public static string Translate(string dayOfWeek)
        {
            if (DayOfWeekTranslations.ContainsKey(dayOfWeek))
            {
                return DayOfWeekTranslations[dayOfWeek];
            }            
            return dayOfWeek;
        }
    }
}
