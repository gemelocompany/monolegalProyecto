using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ejerciocioMonolegal.Models
{
    public class Factura
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string CodigoFactura { get; set; }
        public string Cliente { get; set; }
        public string Ciudad { get; set; }
        public string Nit { get; set; }
        public int Totalfactura { get; set; }
        public int Subtotal { get; set; }
        public int Iva { get; set; }
        public string FechaCreacion { get; set; }
        public string Estado { get; set; }
        public bool Pagada { get; set; }
        public string FechaPago { get; set; }
        public string Correo { get; set; }

         
    }
}
