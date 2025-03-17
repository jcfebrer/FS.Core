using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FSParser;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FSTests.FSParser
{
    [TestClass()]
    public class HtmlRenderTest
    {
        [TestMethod()]
        public void TestHtmlRender()
        {
            Documento _documento = new Documento();
            _documento.DocumentoTipo = "A";
            _documento.SerieFactura = "serie";
            _documento.NumFactura = "111";
            _documento.TotalSinImpuestos = 3456;
            _documento.Moneda = "EUR";
            _documento.CuotaImpuestos = 123;
            _documento.CuotaImpuestosRetenidos = 321;
            _documento.TotalFactura = 12345;
            _documento.CodigoIdentificativo = "COD1";
            _documento.UrlQRTBAI = "http://www.qrtbail.com";

            _documento.Emisor = new Emisor();
            _documento.Emisor.CorreoElectronico = "prueba@prueba.com";
            _documento.Emisor.IdentificadorFiscal = "1234567X";
            _documento.Emisor.Telefono = "943320222";
            _documento.Emisor.Nombre = "Emisor";
            _documento.Emisor.Pais = "España";

            _documento.Emisor.Domicilio = new Domicilio();
            _documento.Emisor.Domicilio.Provincia = "Segovia";
            _documento.Emisor.Domicilio.CodigoPostal = "48600";
            _documento.Emisor.Domicilio.Direccion = "dirección n1 10 1B";
            _documento.Emisor.Domicilio.Municipio = "municipio";

            _documento.Destinatario = new Destinatario();
            _documento.Destinatario.CorreoElectronico = "destinatario@prueba.com";
            _documento.Destinatario.IdentificadorFiscal = "234321111X";
            _documento.Destinatario.Telefono = "9498787665";
            _documento.Destinatario.Nombre = "Destinatario";
            _documento.Destinatario.Pais = "España";

            _documento.Destinatario.Domicilio = new Domicilio();
            _documento.Destinatario.Domicilio.Provincia = "Burgos";
            _documento.Destinatario.Domicilio.CodigoPostal = "48600";
            _documento.Destinatario.Domicilio.Direccion = "dirección n1 10 1B";
            _documento.Destinatario.Domicilio.Municipio = "municipio";

            DocumentoLinea documentoLinea = new DocumentoLinea();
            documentoLinea.ProductoDescripcion = "Descripción de prueba.";
            documentoLinea.Cantidad = 4;
            documentoLinea.ProductoIdentificador = "PROD1";
            documentoLinea.IdentificadorImpuestos = "IVA21";
            documentoLinea.TotalSinImpuestos = 100;
            documentoLinea.Precio = 50;
            documentoLinea.Descuento = 10;
            documentoLinea.IdentificadorImpuestosRetenidos = "RET1";

            DocumentoLinea documentoLinea2 = new DocumentoLinea();
            documentoLinea2.ProductoDescripcion = "Descripción de prueba 2.";
            documentoLinea2.Cantidad = 2;
            documentoLinea2.ProductoIdentificador = "PROD2";
            documentoLinea2.IdentificadorImpuestos = "IVA21";
            documentoLinea2.TotalSinImpuestos = 200;
            documentoLinea2.Precio = 100;
            documentoLinea2.Descuento = 5;
            documentoLinea2.IdentificadorImpuestosRetenidos = "RET2";

            _documento.DocumentoLineas = new List<DocumentoLinea>();
            _documento.DocumentoLineas.Add(documentoLinea);
            _documento.DocumentoLineas.Add(documentoLinea2);


            DocumentoImpuesto documentoImpuesto = new DocumentoImpuesto();
            documentoImpuesto.CuotaImpuestos = 1000;
            documentoImpuesto.TipoImpuestos = 10;
            documentoImpuesto.CuotaImpuestosRecargo = 5;
            documentoImpuesto.BaseImpuestos = 500;
            documentoImpuesto.IdentificadorImpuestos = IdentificadorImpuestos.IRAC2100;

            DocumentoImpuesto documentoImpuesto2 = new DocumentoImpuesto();
            documentoImpuesto2.CuotaImpuestos = 1500;
            documentoImpuesto2.TipoImpuestos = 20;
            documentoImpuesto2.CuotaImpuestosRecargo = 15;
            documentoImpuesto2.BaseImpuestos = 50;
            documentoImpuesto2.IdentificadorImpuestos = IdentificadorImpuestos.SINVALOR;

            _documento.DocumentoImpuestos = new List<DocumentoImpuesto>();
            _documento.DocumentoImpuestos.Add(documentoImpuesto);
            _documento.DocumentoImpuestos.Add(documentoImpuesto2);


            HtmlRender html = new HtmlRender(_documento, FSTests.Properties.Resources.factura);
            string result = html.Renderiza();

            if (!result.StartsWith("<!DOCTYPE"))
                Assert.Fail();
        }
    }

    public class Documento
    {
        public Documento()
        {
        }

        public string DocumentoTipo { get; set; }
        public string SerieFactura { get; set; }
        public string NumFactura { get; set; }
        public int TotalSinImpuestos { get; set; }
        public string Moneda { get; set; }
        public decimal CuotaImpuestos { get; set; }
        public decimal CuotaImpuestosRetenidos { get; set; }
        public decimal TotalFactura { get; set; }
        public string CodigoIdentificativo { get; set; }
        public string UrlQRTBAI { get; set; }
        

        public Emisor Emisor { get; set; }
        public Destinatario Destinatario { get; set; }

        public List<DocumentoLinea> DocumentoLineas { get; set; }
        public List<DocumentoImpuesto> DocumentoImpuestos { get; set; }
    }

    public class DocumentoLinea
    {
        public DocumentoLinea()
        {
        }

        public string ProductoIdentificador { get; set; }
        public string ProductoDescripcion { get; set; }
        public int Cantidad { get; set; }
        public int Precio { get; set; }
        public int Descuento { get; set; }
        public int TotalSinImpuestos { get; set; }
        public string IdentificadorImpuestos { get; set; }
        public string IdentificadorImpuestosRetenidos { get; set; }
    }

    public class Domicilio
    {
        public Domicilio()
        {
        }

        public string Direccion { get; set; }
        public string CodigoPostal { get; set; }
        public string Municipio { get; set; }
        public string Provincia { get; set; }
    }

    public class Emisor
    {
        public Emisor()
        {
        }

        public string Nombre { get; set; }
        public string IdentificadorFiscal { get; set; }
        public Domicilio Domicilio { get; set; }
        public string Pais { get; set; }
        public string CorreoElectronico { get; set; }
        public string Telefono { get; set; }
    }

    public class Destinatario
    {
        public Destinatario()
        {
        }

        public string Nombre { get; set; }
        public string IdentificadorFiscal { get; set; }
        public Domicilio Domicilio { get; set; }
        public string Pais { get; set; }
        public string CorreoElectronico { get; set; }
        public string Telefono { get; set; }
    }

    public class DocumentoImpuesto
    {
        public DocumentoImpuesto() { }

        public IdentificadorImpuestos IdentificadorImpuestos { get; set; }
        public decimal BaseImpuestos { get; set; }
        public decimal TipoImpuestos { get; set; }
        public decimal CuotaImpuestos { get; set; }
        public IdentificadorImpuestos IdentificadorImpuestosRecargo { get; set; }
        public decimal TipoImpuestosRecargo { get; set; }
        public decimal CuotaImpuestosRecargo { get; set; }
    }

    public enum IdentificadorImpuestos
    {
        SINVALOR,
        IRAC0400,
        IRAC1000,
        IRAC2100
    }
}
