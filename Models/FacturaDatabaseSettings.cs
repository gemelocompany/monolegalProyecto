using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ejerciocioMonolegal.Models
{
    public class FacturaDatabaseSettings : IFacturaDatabaseSettings
    {
        public string FacturaCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
    public interface IFacturaDatabaseSettings
    {
        string FacturaCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }

}
