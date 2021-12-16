namespace ScheduleGenerator.Traditions
{

    public struct TraditionMarkup
    {
        public int[] markUp;
        public string[] values;
        public static TraditionMarkup EmbededMarkup
        { 
            get;
        } = Embeded();

        static TraditionMarkup Embeded()
        {
            var mark = new TraditionMarkup();
               mark.markUp = new int[] { TraditionMarkup.Text_H1, TraditionMarkup.Text };
               mark.values = new string[] { "Даннная традица внедренная!", "Эту традицию нельзя отключить или изменить, она внедренна программу, а ее действия внедренны в интерфейс." };
            return mark;
        }

        public const int Text_H1 = 0;
        public const int Text_H2 = 1;
        public const int Text = 2;
        public const int TextBox = 3;
        public const int ComboxBox = 4;
        public const int SaveButton = 5;
        public const int BackButton = 6;
    }

}