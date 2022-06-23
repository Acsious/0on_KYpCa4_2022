namespace UlchenkoKursovoi
{
    class Uchenik
    {
        /// <summary>
        /// Имя ученика
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Категория прав
        /// </summary>
        public string Kateg { get; set; }
        /// <summary>
        /// Колличество посещений
        /// </summary>
        public int ?Posesh { get; set; }
        /// <summary>
        /// Колличество ошибок
        /// </summary>
        public int ?KolOshibok { get; set; }
        /// <summary>
        /// Результаты предварительных экзаменов
        /// </summary>
        public string ResPredEx { get; set; }
        /// <summary>
        /// Результаты финального экзамена
        /// </summary>
        public string ResEx { get; set; }
        /// <summary>
        /// Класс "ученик автошколлы"
        /// </summary>
        /// <param name="name">Имя ученика</param>
        /// <param name="kateg">Категория прав</param>
        /// <param name="posesh">Колличество посещений</param>
        /// <param name="kolOshibok">Колличество ошибок</param>
        /// <param name="resPredEx">Результаты предварительных экзаменов</param>
        /// <param name="resEx">Результаты финального экзамена</param>
        public Uchenik(string name, string kateg, int posesh, int kolOshibok, string resPredEx, string resEx)
        {
            Name = name;
            Kateg = kateg;
            Posesh = posesh;
            KolOshibok = kolOshibok;
            ResPredEx = resPredEx;
            ResEx = resEx;
        }
        /// <summary>
        /// Формирование строки для json из всех свойств класса
        /// </summary>
        /// <returns>Строка готовая для записи в json файл</returns>
        public string returnJsonObject()
        {
            return "{ 'Name': '" + Name + "', 'Kateg': '" + Kateg + "','Posesh': '" + Posesh +
                "','KolOshibok':'" + KolOshibok + "','ResPredEx':'" + ResPredEx+ "','ResEx':'" + ResEx + "'}";
        }
    }
}