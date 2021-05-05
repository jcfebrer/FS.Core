// create new folder
function newfolder() {
	if (document.frmFile.elements['targetfolder'].value == '') {
		alert('No se ha indicado una carpeta. Indica un nombre para la carpeta en la caja de texto.');
		document.frmFile.elements['targetfolder'].focus();
		return;
	}
	document.frmFile.action.value = 'newfolder';
	document.frmFile.submit();
}

function upload() {
	if (document.frmFile.elements['fileupload'].value == '') {
		alert('No se ha indicado un fichero. Selecciona un fichero para subir.');
		document.frmFile.elements['fileupload'].focus();
		return;
	}
	document.frmFile.action.value = 'upload';
	document.frmFile.submit();
}

// check or uncheck all file checkboxes
function checkall(ctl) {
	for (var i = 0; i < document.frmFile.elements.length; i++) {
	    if (document.frmFile.elements[i].name.indexOf('checked_') > -1) { 
	        document.frmFile.elements[i].checked = ctl.checked;
	        }
		}
}

// confirm file list and target folder
function confirmfiles(sAction) {
	var nMarked = 0;
	var sTemp = '';
	for (var i = 0; i < document.frmFile.elements.length; i++) {
		if (document.frmFile.elements[i].checked && 
		 document.frmFile.elements[i].name.indexOf('checked_') > -1) { 
			if (sAction == 'renombrar') {
				var sFilename = '';
    			var sNewFilename = '';
				sFilename = document.frmFile.elements[i].name;
				sFilename = sFilename.replace('checked_','');
				sNewFilename = prompt('Indica un nuevo nombre para: ' + sFilename, sFilename);
				if (sNewFilename != null) {
				    document.frmFile.elements[i].name = document.frmFile.elements[i].name + '_2_' + sNewFilename;
				}
			}
			nMarked = nMarked + 1;
		}
	}
	if (nMarked == 0) {
		alert('No se ha seleccionado ningún elemento. Para seleccionar, utiliza la caja de marcar de la izquierda.');
		return;
	}
	sTemp = 'Estás seguro que quieres ' + sAction + ' el/los elemento(s) ' + nMarked + ' marcado(s)?'
	if (sAction == 'copiar' || sAction == 'mover') {
		sTemp = 'Estás seguro que quieres ' + sAction + ' el/los elemento(s) ' + nMarked + ' marcado(s) a la carpeta: "' + document.frmFile.elements['targetfolder'].value + '"?'
		if (document.frmFile.elements['targetfolder'].value == '') {
		    document.frmFile.elements['targetfolder'].focus();
			alert('No se ha indicado una carpeta de destino. Indica un nombre de carpeta.');
			return;
		}
	}
	var confirmed = false;
	if (sAction == 'copiar' || sAction == 'renombrar') {
	    confirmed = true;
	} else {
	    confirmed = confirm(sTemp);
	}

	if (confirmed) { 
		document.frmFile.action.value = sAction;
		document.frmFile.submit();
	}

}

function download(file)
{
    document.frmFile.filedownload.value = file;
    document.frmFile.action.value = 'descargar';
    document.frmFile.submit();
}