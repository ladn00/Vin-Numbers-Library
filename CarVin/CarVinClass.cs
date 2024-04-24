using System;
using System.IO;
using System.Linq;

namespace CarVin
{
    /// <summary>
    /// Класс для обработки сведений о vin номере автомобиля
    /// </summary>
    public class CarVinClass
    {


        string[] countries = { "ЮАР", "Кот-д’Ивуар", "не используется", "Ангола", "Кения", "Танзания", "не используется",
            "Бенин", "Мадагаскар", "Тунис", "не используется", "Египет", "Марокко", "Замбия", "не используется", "Эфиопия",
            "Мозамбик", "не используется", "Гана", "Нигерия", "не используется", "не используется", "не используется", "Япония",
            "Шри Ланка", "Израиль", "Южная Корея", "Казахстан", "Китай", "Индия", "Индонезия", "Таиланд", "Мьянма", "Иран", "Пакистан",
            "Турция", "не используется", "Филиппины", "Сингапур", "Малайзия", "не используется", "ОАЭ", "Тайвань", "Вьетнам", "Саудовская Аравия",
            "Великобритания", "Восточная Германия", "Польша", "Латвия", "Швейцария", "Чехия", "Венгрия", "Португалия", "не используется",
            "не используется", "Дания", "Ирландия", "Румыния", "не используется", "Словакия", "не используется", "Австрия", "Франция",
            "Испания", "Сербия", "Хорватия", "Эстония", "Германия", "Болгария", "Греция", "Нидерланды", " СССР/СНГ", "Люксембург",
            "Россия", "Бельгия", "Финляндия", "Мальта", "Швеция", "Норвегия", "Беларусь", "Украина", "Италия", "не используется",
            "Словения", "Литва", "Россия", "США", "Канада", "Мексика", "КостаРика", "Каймановы острова", "США", "США", "Австралия",
            "не используется", "Новая Зеландия", "не используется", "Аргентина", "Чили", "Эквадор", "Перу", "Венесуэла", "не используется",
            "Бразилия", "Колумбия", "Парагвай", "Уругвай", "Тринидад и Тобаго", "Бразилия", "не используется" };
        int[] years = { 1994, 1995, 1996, 1997, 1998, 1999, 2000, 2001, 2002, 2003, 2004, 2005, 2006, 2007, 2008, 2009, 2010, 2011,
            2012, 2013, 2014, 2015, 2016, 2017, 2018, 2019, 2020, 2021, 2022, 2023};
        string[] yearsCode = { "R", "S", "T", "V", "W", "X", "Y", "1", "2", "3", "4", "5", "6", "7", "8", "9", "A", "B", "C", "D", "E", "F", "G", "H", "K", "L", "M", "N", "P" };
        string[] combinations = new string[110];
        /// <summary>
        /// Метод проверяет кооректность ввода vin номера автомобиля
        /// </summary>
        public bool VinCheck(string vin)
        {
            bool itog = true;

            if (vin != vin.ToUpper() || vin.Length != 17 || CodeCountry(vin) == "не используется" || CodeCountry(vin) == "")
                itog = false;

            if (Char.IsDigit(vin[8]) == false && vin[8] != 'X')
                itog = false;

            if (!yearsCode.Contains(vin[9].ToString()))
                itog = false;

            for (int i = 0; i < vin.Length; i++)
            {
                if (!Char.IsLetterOrDigit(vin[i]))
                    itog = false;
            }
            return itog;
        }

        /// <summary> 
        /// Метод для определения страны производителя
        /// </summary>
        public string CodeCountry(string vin)
        {
            StreamReader sr = new StreamReader(@"file.txt");

            for (int i = 0; i < combinations.Length; i++)
                combinations[i] = sr.ReadLine();
            string country = "";
            string vinSub = vin.Substring(0, 2);
            int index = 0;
            if (vinSub[0] >= 'J' && vinSub[0] <= 'R') index = 23;
            if (vinSub[0] >= 'S' && vinSub[0] <= 'Z') index = 55;
            if (vinSub[0] >= '1' && vinSub[0] <= '5') index = 85;
            if (vinSub[0] >= '6' && vinSub[0] <= '7') index = 92;
            if (vinSub[0] >= '8' && vinSub[0] <= '9') index = 96;
            for (; index < combinations.Length; index++)
            {
                if (combinations[index].Contains(vinSub))
                    country = countries[index];
            }
            sr.Close();
            return country;
        }

        /// <summary>
        /// Метод для установления года производства двигателя
        /// </summary>
        public int Year(string vin)
        {
            string vinSub = vin.Substring(9, 1);
            int yearsIndex = 0;
            for (int i = 0; i < yearsCode.Length; i++)
            {
                yearsIndex++;
                if (vinSub == yearsCode[i])
                    i = yearsCode.Length - 1;
            }
            return years[yearsIndex - 1];
        }
    }
}
