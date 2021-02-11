using ejerciocioMonolegal.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ejerciocioMonolegal.Controllers
{
    public class FacturaService
    {
        private readonly IMongoCollection<Factura> _facturas;
        public FacturaService(IFacturaDatabaseSettings settings)
        {
            var client =new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _facturas = database.GetCollection<Factura>(settings.FacturaCollectionName);
        }
        public List<Factura> Get() 
        {
            return _facturas.Find(factura => true).ToList<Factura>();
        }
        public Factura Get(string id) =>
            _facturas.Find<Factura>(factura => factura.Id == id).FirstOrDefault();

        public void Update(string id, Factura nuevaFactura) =>
          _facturas.ReplaceOne(factura => factura.Id == id, nuevaFactura);
    }
}
