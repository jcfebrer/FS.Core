using FSDatabase;
using System.Collections.Generic;
using System.Data;

namespace FSPortal
{
    public class ParserData
    {
        public string tabla = "";
        public string seleccion = "";
        public string sql = "";
        public string orderby = "";
        public DataTable dataTable;
        public long position = 0;
    }
    public class ParserRepeat
    {
        public int id;
        public string data;

        public string linkNext = "";
        public string linkPrevious = "";
        public string linkFirst = "";
        public string linkLast = "";
    }

    public class ParserYesNo
    {
        public string condition;
        public string datayes;
        public string datano;
    }

    public class ParserVariable
    {
        public string name;
        public string value;
    }

    public class VariablesParser
    {
        public Variables.FormMod frmModo = Variables.FormMod.Nada;
        public string frmEmailTo = "";
        public string frmPagina = "";
        public string frmEmailSubject = "";
        public string frmRequest = "";
        public int frmPaginas = 0;
        public int frmPaginaActual = 0;
        public int frmMaxRegistrosPagina = 0;
        public long frmTotalRegistros = 0;

        public int frmDataPos = 0;

        public Register frmCampos = null;
        public int frmFileMaxSize = 100;
        public string frmFileTypes = "jpg,gif,bmp,jpeg,png";
        public string frmFileUploadPath = "";
        public string frmMensajeSinRegistros = "No hay registros.";
        public string frmMensajeCombo = "Selecciona ...";
        public string frmComboOnChange = "";
        public string frmComboSelected = "";
        public string frmConn = "";
        public string frmRedirige = "";
        public bool frmTruncar = true;
        public string frmMensajeOK = "Formulario procesado correctamente.";
        public string frmMensajeNoOK = "Problemas al procesar el formulario.";
        public string frmVolverAtras = @"<a href=""javascript:history.back();"">Volver atrás</a>";
        public long frmIdentity = 0;
        public bool frmRandom = false;
        public bool frmCaptcha = false;
        public bool frmRotatePdf = false;

        public List<ParserRepeat> contenidoRepite = new List<ParserRepeat>();
        public List<ParserYesNo> contenidoSi = new List<ParserYesNo>();
        public List<ParserData> data = new List<ParserData>();
        public List<ParserVariable> variable = new List<ParserVariable>();
    }
}
