import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { error } from '@angular/compiler/src/util';

@Component({
  selector: 'app-factura',
  templateUrl: './factura.component.html'
})
export class FacturaComponent {
  public facturas: Factura[];
  public httpCliente: HttpClient;
  public url: string;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.httpCliente = http;
    this.url = baseUrl;
    http.get<Factura[]>(baseUrl + 'api/factura').subscribe(result => {
      this.facturas = result;
    }, error => console.error(error));
  }
  public EnviarCorreos()
  {
    this.httpCliente.post(this.url + "api/factura/enviarCorreo", "").subscribe(result => { this.refreshData() },
      error => console.error(error));
  }
  public EnviarCorreo(id: string)
  {
    this.httpCliente.post(this.url + "api/factura/enviarCorreo/" + id, "").subscribe(result => { this.refreshData() },
      error => console.error(error));
  }
  public refreshData()
  {
    this.httpCliente.get<Factura[]>(this.url + 'api/factura').subscribe(result => {
      this.facturas = result;
    }, error => console.error(error));
  }

}

interface Factura {
  id: string;
  codigoFactura: string;
  cliente: string;
  ciudad: string;
  nit: string;
  totalFactura: number;
  subtotal: number;
  iva: number;
  fechaCreacion: string;
  estado: string;
  pagada: boolean;
  fechaPago: string
}
