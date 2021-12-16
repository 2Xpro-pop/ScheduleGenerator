using System.Linq;
using System.IO;
using System.Collections.Generic;
using ScheduleGenerator.Models;

namespace ScheduleGenerator.Traditions
{
    public class AssemblyTraditions
    {
        private Dictionary<string, ITradition> Traditions { get; } = new Dictionary<string, ITradition>();

        public AssemblyTraditions()
        {
            
        }
        
        public void Load(string path)
        {
            var ext = Path.GetExtension(path);
            if(ext == ".dll" || ext == ".so")
            {
                var tradition = new CImplementTradition();
                tradition.Load(path);
                Traditions.Add(tradition.GetMetaInfo().UniqueId, tradition);
            }
        }

        public TraditionMarkup GetTraditionMarkUp(string unique)
        {
            if(unique.StartsWith("indule.emb"))
            {
                return TraditionMarkup.EmbededMarkup;
            }
            return Traditions[unique].GetMarkup();
        }

        public ITradition GetTradition(string unique)
        {
            return Traditions[unique];
        }

        public IEnumerable<Models.TraditionMetaInfo> GetTraditionsMetaInfos()
        {
            return Traditions.Select( f => f.Value.GetMetaInfo()).Concat
            (
                new TraditionMetaInfo[]
                {
                    new TraditionMetaInfo("Свобода Графика!", "Позволяет учителям выбирать не удобные для них часы(пары).", "indule.emb.teacher"),
                    new TraditionMetaInfo("Свобода Часа!", "Позволяет группам выбирать часы(пары) в которые они свободны.", "indule.emb.group"),
                }
            );
        } 

    }
}