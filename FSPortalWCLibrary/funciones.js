// evito que se cargue en otro frame
// if (top.location != self.location)top.location = self.location;


function contar()
{
var file=directorioPortal + 'contar.aspx';
var d=new Date();
var s=d.getSeconds();
var m=d.getMinutes();
var x=s*m;
f='' + escape(document.referrer);
if (navigator.appName=='Netscape'){b='NS';}
if (navigator.appName=='Microsoft Internet Explorer'){b='MSIE';}
if (navigator.appVersion.indexOf('MSIE 3')>0) {b='MSIE';}
u='' + escape(document.URL); w=screen.width; h=screen.height;
v=navigator.appName;
fs = window.screen.fontSmoothingEnabled;
if (v != 'Netscape') {c=screen.colorDepth;}
else {c=screen.pixelDepth;}
j=navigator.javaEnabled();
info='w=' + w + '&amp;h=' + h + '&amp;c=' + c + '&amp;r=' + f + '&amp;u='+ u + '&amp;fs=' + fs + '&amp;b=' + b + '&amp;x=' + x;
document.write('<' + 'img src="' + file + '?'+info+ '" width="0" height="0" alt="" border="0"/>');
}


function Hoy(frm) {
            DiaActual = new Date();
            frm.Month.value=DiaActual.getMonth()+1;
            frm.Year.value=DiaActual.getYear();
            frm.submit();
            }
function ratonDentro(Origen, ColorCelda) {
//Origen.style.cursor = 'hand';
Origen.bgColor = ColorCelda;
}
function ratonFuera(Origen, ColorCelda) {
//Origen.style.cursor = 'default';
Origen.bgColor = ColorCelda;
}

function Right(str, n)
{
		if (n <= 0)
		   return "";
		else if (n > String(str).length)
		   return str;
		else {
		   var iLen = String(str).length;
		   return string(str).funcVB.Substring(iLen, iLen - n);
		}
}

function open_window(url)
{
var nueva=window.open(url,'window','toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=250,height=250');
return true;
}

function open_mediaplayer(url)
{   
var nueva=window.open(url, 'WMP','width=1,height=1');
return true;
}

function browse(url)
{
var nueva=window.open(url,'codigo','toolbar=no,top=20,left=20,width=400,height=400,titlebar=no,status=no,resizable=yes,scrollbars=yes');
return true;
}

function open_radio(idEmisora,carpeta)
{
window.open(carpeta + "radio/radio.aspx?idEmisora="+idEmisora,"codigo","width=255,height=165,toolbar=no,menubar=no,resizable=no,location=no,directories=no,status=no,scrollbars=no,copyhistory=no");
return true;
}

function checkField(field)
{
if(!field) return false;
return true;
}

function hideshow(which,img,carpeta){
	var now = new Date();
	now.setTime(now.getTime() + 1000 * 60 * 60 * 24 * 365)
	if(document.all && !document.getElementById) {
		document.getElementById = function(id) {
			 return document.all[id];
		}
	}
	if (document.getElementById){
		oWhich = document.getElementById(which).style;
		oImage = document.getElementById(img );}
	else
		if (document.layers) {
			oWhich = document.layers[which];
			oImage = document.layers[img];}
	window.focus();
	
	if (oWhich.display == "none"){
		oWhich.display = "";
		oImage.src = carpeta + "imagenes/menos.gif"
		oImage.alt = "Menos";
		setCookie(which,"show","/",now);
		}
	else{
		oWhich.display = "none";
		oImage.src = carpeta + "imagenes/mas.gif";
		oImage.alt = "Más";
		setCookie(which,"hide","/",now);
		}
}

function setCookie(name, value, path, expires, domain, secure){
	 document.cookie = name + "=" +escape(value) +
        ((expires) ? ";expires=" + expires.toGMTString() : "") +
        ((path) ? ";path=" + path : "") + 
        ((domain) ? ";domain=" + domain : "") +
        ((secure) ? ";secure" : "");	
		
}

function getCookie(name) {
  var nameq = name + "=";
  var c_ar = document.cookie.split(';');
  for (var i=0; i<c_ar.length; i++) {
    var c = c_ar[i];
    while (c.charAt(0)==' ') c = c.substr(1,c.length);
    if (c.indexOf(nameq) == 0) return unescape( c.substr(nameq.length, c.length) );
  }
  return null;
}

function deleteCookie(name, path, domain){
    if (getCookie(name)){
        document.cookie = name + "=" + 
            ((path) ? "; path=" + path : "") +
            ((domain) ? "; domain=" + domain : "") +
            "; expires=Thu, 01-Jan-70 00:00:01 GMT";}
}

function verifyUploadFields( form )
{
    if (form.name.value == "")
    {
    alert("Por favor, indica un nombre para el fichero.");
    return false;
    }

    if (form.filename.value == "")
    {
    alert("Por favor, selecciona una imagen para subir.");
    return false;
    }

    var illegalCharArray = new Array(12);
    illegalCharArray = new Array('#', '"', ':', '*', '?', '<', '>', '|', ';', '^', '/', '\\');
    var imageName = form.name.value;
    if (imageName.length != 0)
    {
        for (var i = 0; i < illegalCharArray.length; i++)
        {
            var offset = imageName.lastIndexOf(illegalCharArray[i]);
            if (offset != -1)
            {
                alert("Los siguientes caracteres no se pueden utilizar como nombre de fichero: #\"\\/:*?<>|;^");
                return false;
            }
        }
    }    
            
    var index = (form.filename.value).lastIndexOf(".");
    var ext = (form.filename.value).substr(index + 1).toUpperCase();
    if ((ext == "GIF") || (ext == "JPG") || (ext == "PNG"))
    {
    return true;
    }
            
    alert("Extensión incorrecta.  Por favor, selecciona un fichero .gif, .jpg o .png.");
    return false;
}