namespace ScheduleGenerator.Traditions
{
    public interface ITradition
    {
        void Load(string path);
        Models.TraditionMetaInfo GetMetaInfo();
        TraditionMarkup GetMarkup();
        bool SaveOptions(string[] Options);
    }
}