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

            HtmlRender html = new HtmlRender(_documento, Properties.Resources.factura);
            string result = html.Renderiza();
        }
    }

    class Documento
    {
        public Documento()
        {
        }

        public string DocumentoTipo;
        public string SerieFactura;
        public string NumFactura;

        public Emisor Emisor;
        public Destinatario Destinatario;

        public List<DocumentoLinea> DocumentoLineas;
    }

    class DocumentoLinea
    {
        public DocumentoLinea()
        {
        }

        public string ProductoIdentificador;
        public string ProductoDescripcion;
        public int Cantidad;
        public int Precio;
        public int Descuento;
        public int TotalSinImpuestos;
        public string IdentificadorImpuestos;
        public string IdentificadorImpuestosRetenidos;
    }

    class Domicilio
    {
        public Domicilio()
        {
        }

        public string Direccion;
        public string CodigoPostal;
        public string Municipio;
        public string Provincia;
    }

    class Emisor
    {
        public Emisor()
        {
        }

        public string Nombre;
        public string IdentificadorFiscal;
        public Domicilio Domicilio;
        public string Pais;
        public string CorreoElectronico;
        public string Telefono;
    }

    class Destinatario
    {
        public Destinatario()
        {
        }

        public string Nombre;
        public string IdentificadorFiscal;
        public Domicilio Domicilio;
        public string Pais;
        public string CorreoElectronico;
        public string Telefono;
    }
}
