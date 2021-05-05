//CSS Section
IncludeCSS('estilos/estiloAdmin.css');
IncludeCSS('javascript/jquery/jquery-ui-1.11.4/jquery-ui.min.css');
IncludeCSS('javascript/jquery/fancybox/jquery.fancybox-1.3.1.css');
IncludeCSS('javascript/jquery/jquery.alerts/jquery.alerts.css');
//IncludeCSS('javascript/jquery/jquery.contextMenu/jquery.contextMenu.css');
IncludeCSS('javascript/jquery/Guriddo_jqGrid_JS_5.2.1/css/ui.jqgrid.css');
IncludeCSS('javascript/jquery/tree/jquery.treeview.css');

//JS Section
if (!window.jQuery) {
	IncludeJS('javascript/jquery/jquery-1.11.3.min.js');
	}

if(window.htmlEditor=="CKeditor") {
	IncludeJS('javascript/ckeditor/ckeditor.js');
	IncludeJS('javascript/ckeditor/adapters/jquery.js');
	}
if(window.htmlEditor=="TinyMce") {
	IncludeJS('javascript/tiny_mce/tinymce.min.js');
}

IncludeJS('javascript/jquery/Guriddo_jqGrid_JS_5.2.1/src/jquery.jqGrid.js');
IncludeJS('javascript/jquery/Guriddo_jqGrid_JS_5.2.1/js/i18n/grid.locale-es.js');

IncludeJS('javascript/jquery/jquery.form.min.js');
IncludeJS('javascript/jquery/jquery.history.js');
IncludeJS('javascript/jquery/tree/jquery.treeview.js');

//Fancybox
IncludeJS('javascript/jquery/fancybox/jquery.fancybox-1.3.1.pack.js');
IncludeJS('javascript/jquery/fancybox/jquery.mousewheel-3.0.2.pack.js');
IncludeJS('javascript/jquery/fancybox/jquery.easing-1.3.pack.js');

//Magnify
IncludeJS('javascript/jquery/magnify/jquery.magnify.min.js');

IncludeJS('javascript/jqueryFunc.js');
IncludeJS('javascript/jquery/jquery.alerts/jquery.alerts.js');
//IncludeJS('javascript/jquery/jquery.contextMenu/jquery.contextMenu.js');
IncludeJS('javascript/jquery/jquery-ui-1.11.4/jquery-ui.min.js');
IncludeJS('javascript/jquery/jquery.localScroll/jquery.scrollTo.js');
IncludeJS('javascript/funciones.js');
IncludeJS('javascript/filemanager.js');


function IncludeJS(jsFile) {
    document.write('<script type="text/javascript" src="' + window.directorioPortal + jsFile + '"></script>');
}

function IncludeCSS(cssFile) {
    document.write('<link rel="stylesheet" type="text/css" href="' + window.directorioPortal + cssFile + '" />');
}