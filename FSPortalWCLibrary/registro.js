var et_nombre = "Nombre";
var et_apellido1 = "Primer Apellido";
var et_apellido2 = "Segundo Apellido";
var et_fecha1 = "Fecha de nacimiento";
var et_dni = "DNI / Pasaporte";
var et_sexo = "Sexo";
var et_email = "E-mail";
var et_domicilio = "Domicilio";
var et_portal = "Nº Portal";
var et_piso = "Piso / Planta / Escalera / Puerta";
var et_poblacion = "Población";
var et_provincia = "Provincia (Estado, Departamento...)";
var et_codigopostal = "Código Postal";
var et_pais = "País";
var et_telefono1 = "Teléfono 1";
var et_telefono2 = "Teléfono 2";
var et_usuario = "Usuario";
var et_password = "Contraseña";
var et_confirmar = "Confirmar contraseña";
var et_recordatorio = "Recordatorio";
var Et_Error1 = "Con el fin de mejorar su seguridad, el nuevo registro de clientes de El Corte Inglés\nno permite que el nombre de usuario y la contraseña sean iguales, por lo que le\nagradeceríamos que al actualizar sus datos elija una nueva contraseña diferente\nde su nombre de usuario.\n\n Muchas gracias";
var Et_Error2 = "Debe rellenar obligatoriamente el campo %1";
var Et_Error3 = "No puede haber sólo números en el campo %1";
var Et_Error4 = "Sólo se permiten números en el campo %1";
var Et_Error5 = "La fecha de nacimiento está incompleta.";
var Et_Error6 = "Debe seleccionar el campo SEXO.";
var Et_Error7 = "La contraseña está mal confirmada.";
var Et_Error8 = "La contraseña debe tener entre 4 y 8 caracteres (a-z,A-Z,0-9).";
var Et_Error9 = "Por motivos de seguridad, la contraseña no puede coincidir con el nombre de usuario.";
var Et_Error10 = "Valor no válido en el campo E-Mail. Ej: nombre@servidor_correo.com";
var Et_Error11 = "Por favor, introduzca un nombre de usuario de al menos 8 caracteres (a-z,A-Z,0-9).";
var Et_Error12 = "Valor no válido en el campo Día de Fecha de Nacimiento.\n Introduzca un valor entre 1 y 31.";
var Et_Error13 = "Valor no válido en el campo Mes de Fecha de Nacimiento.\n Introduzca un valor entre 1 y 12.";
var Et_Error14 = "Valor no válido en el campo Año de Fecha de Nacimiento.\n Introduzca un valor entre 1900 y %2";
var Et_Error15 = "Día no válido para ese mes.";
var Et_Error16 = "Por favor, introduzca un número de CODIGO POSTAL correcto para España.";
var Et_Error17 = "Usted ha indicado que es residente fuera de España, \nsu pais de residencia no puede ser España.";
var Et_Error18 = "Usted ha indicado que es residente en España, \nsu pais de residencia debe ser España.";
var Et_Error19 = "Por favor, introduzca un valor numérico en el campo TELEFONO.";
var Et_Error20 = "El TELEFONO debe ser de 9 digitos obligatoriamente.";
var Et_Error21 = "Debe seleccionar su pais en el listado de PAISES.";
var Et_Error22 = "El CÓDIGO POSTAL de Portugal se compone de cuatro números, guión y tres números.";
var Et_Error23 = "El primer dígito del número de teléfono debe ser 9, 8 o un 6.";
var Et_Error24 = "El crédito mensual debe de ser un valor entre 1000 y 15000 y multiplo de 1000";
var Et_Error25 = "La fecha de nacimiento es incorrecta.";
var Et_Error27 = "El campo %1 debe de tener entre %3 y %4 caracteres/digitos.";
var Et_Error28 = "El campo %1 debe de tener %3 caracteres/digitos.";
var Et_Error29 = "No puede haber sólo letras en el campo %1";

	var codpohab_act = '';

		function inicia() {
			pondni();
		}

// ************************************************************************************ 

var num_errores;		


function Valor(campo) {

	var valor,tipo,valor_final;
	
	valor = "";
	tipo = document.registro[campo].type;
	
	if (tipo=="select-one") {
		valor = document.registro[campo].options[document.registro[campo].selectedIndex].value;
		}
	else {
		if ((tipo=="text") || (tipo=="password") || (tipo=="hidden")) {
			valor = document.registro[campo].value;
			if (valor!="") {
				valor = valor.replace(/^\s*/i,"").replace(/\s*$/i,"");
				document.registro[campo].value = valor;
			}
		}
		else {
			if (document.registro[campo][0].checked)
				valor = document.registro[campo][0].value;
			if (document.registro[campo][1].checked)
				valor = document.registro[campo][1].value;
			}
		}
	
	return (valor);
}


function damecookie(nombre) {

	var arr,i,cookies,j,valor;

	valor = "";
	cookies = document.cookie;
	arr = cookies.split(";");
	for (i=0;i<arr.length;i++) {
		j = arr[i].indexOf(nombre);
		if (j>=0) {
			valor = arr[i].substr(j+nombre.length+1).replace(/\s*$/i,"");
			return valor;
		}
	}
	
	return "";
}


function enviar(modo) {

	var codpohab, concentroant, bol;

	num_errores = 0;
	
	validar(modo);
	
	if (num_errores == 0) {
		document.registro.submit();
	}

}


function tratarerror(campo,texto) {
	tratarerrorminmax(campo,texto,0,0);
}

	
function tratarerrorminmax(campo,texto,longmin,longmax) {

	var txt,valor,i,fecha,campo_txt;
	
	fecha = new Date();
	if (campo.substr(0,7)=="tarjeta")
		campo_txt = "ntarjeci";
	else {
		if (campo.substr(0,5)=="fecha")
			campo_txt = "fecha1";
		else
			campo_txt = campo;
	}
		
	num_errores = num_errores + 1;
	if (num_errores == 1) {
		txt = eval(texto);
		valor = eval("et_"+campo_txt);
		txt = txt.replace("%1",valor);
		txt = txt.replace("%2",fecha.getYear());
		txt = txt.replace("%3",longmin);
		txt = txt.replace("%4",longmax);
		alert(txt);
		if ((campo=="sexo") || (campo=="xtarjeci"))
			document.registro[campo][0].focus();
		else
			document.registro[campo].focus();
	}
}


function validarcampo(campo,obligatorio,numerico,longmin,longmax,expr,error) {

	var val,re;
	
	val = Valor(campo);
	
	if (val=="") {
		if (obligatorio) {
			tratarerror(campo,"Et_Error2");
			return;
		}
		else return;
	}			
	if (numerico) {
		re = new RegExp("^\\d+$","i");
		if (!re.test(val)) {
			tratarerror(campo,"Et_Error4");
			return;
		}
	}
	if ((val.length<longmin) || (val.length>longmax)) {
		if (longmin==longmax)
			tratarerrorminmax(campo,"Et_Error28",longmin,longmax);
		else
			tratarerrorminmax(campo,"Et_Error27",longmin,longmax);
		return;
	}
	if ((val!="") && (expr!="")) {
		re = new RegExp(expr,"i");
		if (!re.test(val))
			tratarerror(campo,error);
	}

}

function EsFecha(s) 
{ 
var Tentativa = new Date(s);
if (isNaN(Tentativa)) 
	{ return false; }
else 
	{ return true; } 
}


function validar(modo) {

	var pais,expr;

	validarcampo("nombre",true,false,0,20,"\\D","Et_Error3");
	validarcampo("apellido1",true,false,0,20,"\\D","Et_Error3");
	validarcampo("apellido2",false,false,0,20,"\\D","Et_Error3");
	
	if (EsFecha(Valor("fechaNacimiento"))==false)
	{
		alert(Et_Error25);
		document.registro["fechaNacimiento"].focus();
		num_errores=1;
		return;
	}

	pais = Valor("pais");
	
	if (pais=="")
		tratarerror("pais","Et_Error2");
		
	if (pais!="es")
		validarcampo("dni",false,false,0,15,"\\d","Et_Error29");
	else
		validarcampo("dni",true,false,0,15,"\\d","Et_Error29");
		
	
	validarcampo("sexo",true,false,0,1,"","");
	validarcampo("email",true,false,0,50,"","");
	
	
	validarcampo("tipodomicilio",true,false,0,35,"\\D","Et_Error3");
	validarcampo("portal",true,true,0,4,"","");
	validarcampo("domicilio",false,false,0,20,"","");
	validarcampo("poblacion",true,false,0,30,"\\D","Et_Error3");
	validarcampo("provincia",true,false,0,20,"\\D","Et_Error3");
	validarcampo("pais",true,false,0,3,"","");
	
	if (pais=="es") {
		validarcampo("codigopostal",true,true,5,5,"","");
		if ((Valor("codigopostal")>53000) || (Valor("codigopostal")<1000))
			tratarerror("codigopostal","Et_Error16");
		validarcampo("telefono1",true,true,9,9,"^[968].*$","Et_Error23");
		validarcampo("telefono2",false,true,9,9,"^[968].*$","Et_Error23");
	}
	else {
		if (pais=="010")
			validarcampo("codigopostal",true,false,0,8,"^\\d{4}-\\d{3}$","Et_Error22");
		else
			validarcampo("codigopostal",true,false,0,10,"","");
		validarcampo("telefono1",true,true,9,12,"","");
		validarcampo("telefono2",false,true,9,12,"","");
	}

	if (modo != 'Modificar') {

		validarcampo("usuario",true,false,8,16,"^[A-Za-z0-9]*$","Et_Error11");
		validarcampo("password",true,false,4,8,"^\\w*$","Et_Error8");
		
		if (Valor("password")!="") {
		
			expr = new RegExp("^"+Valor("password")+"$","i");
			if (expr.test(Valor("usuario")))
				tratarerror("password","Et_Error9");
			if (!expr.test(Valor("confirmar")))
				tratarerror("confirmar","Et_Error7");

		}
			
		validarcampo("recordatorio",false,false,0,35,"","");
	}

	return true;
}