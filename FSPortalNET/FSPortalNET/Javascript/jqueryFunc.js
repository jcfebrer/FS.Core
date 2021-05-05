
//desactivamos las animaciones
//jQuery.fx.off = true;

// Control de errores Ajax
$().ajaxError(function(ev, xhr, o, err) {
    if (err != null) {
        alert(err);
        if (window.console && window.console.log) console.log(err);
    }
});

function IsJson(obj) {
       try {
          var json = $.parseJSON(obj);
       } catch(e) {
           return false;
       }
	   return true;       
}

function IsJsonString(str) {
    try {
        JSON.parse(str);
    } catch(e) {
        return false;
    }
    return true;
}


function processShow(data) {
    if (typeof data.link != 'undefined' & data.link!='') {
        jAlert(data.message, 'Formulario.', function() {
            document.location.href = data.link;
        });
    } else {
        jAlert(data.message, 'Formulario.');
    }
}

function processLogin(data) {
    // si hay problemas en la validación, data.message, tiene la descripción del error.
    if (data.message != '')
        jAlert(data.message, 'Error en validación');
    else {
        jAlert('Bienvenido al portal ' + nombreWeb, 'Validación correcta',
            function() {
            	var dest = getQuery("comebackto");
            	if(typeof dest!='undefined')
                	document.location.href = dest;
                else
                	document.location.href = directorioPortal + 'default.aspx';
            }
        );
    }
}


function errorShow(response, status, err) 
{
	//form.submit();
	alert(err);      
}

function showRequest(formData, jqForm, options) { 
    // formData is an array; here we use $.param to convert it to a string to display it 
    // but the form plugin does this for you automatically when it submits the data 
    var queryString = $.param(formData); 
 
    // jqForm is a jQuery object encapsulating the form element.  To access the 
    // DOM element for the form do this: 
    // var formElement = jqForm[0]; 
 
    alert('About to submit: \n\n' + queryString); 
 
    // here we could return false to prevent the form from being submitted; 
    // returning anything other than false will allow the form submit to continue 
    return true; 
}


// procesamos el formulario de login
$(function() {

	var options = { 
		dataType: 'json',
        //beforeSubmit: showRequest,
        success: processShow,
        resetForm: false,
        error: errorShow
	};

    // Formulario generico (submit)
    $('.frmSubmit').submit(function()
    {
    	$(this).ajaxSubmit(options);
    	return false;
    });
    
    
    $('.frmFormJson').submit(function(e){
    
    	//e.preventDefault();
    	
    	$.ajax({
		  type: 'POST',
		  data: $(this).serialize(),
		  dataType: "json",
		  success: function(data){
		        jAlert( data.message, 'Formulario.' );  
		  },
		  error: function(err) {
		  	 //en caso de error hacemos un submit normal
		     //alert(err);
		     //$('.frmForm').submit();
		     
		     var newDoc = document.open("text/html", "replace");
			 newDoc.write(err.responseText);
			 newDoc.close();
			 
			 //window.location.href = '';
		  }
		});
    	
    	
    	//$.post($(this).attr('action'), $(this).serialize(), function(data) {
    	
    		//if(IsJson(data))
    		//	{
    	//		jAlert(data.message, 'Formulario.');
    		//	return false;
    		//	}
    		//else
    		//	return true;

    	//}, 'json');
    	
    	return false;
  	});
    
    
    // Formulario generico (form)
    $('.frmForm').ajaxForm(options);


    // Formulario de login en caja
    $('.frmLogin').ajaxForm({
        dataType: 'json',
        success: processLogin,
        resetForm: true,
        error: errorShow
    });

    //   // Gestión de enlaces
    //   $("a").each(function (){
    //      var href = $(this).attr("href");
    //      
    //      if( href.indexOf('?',0) > 0)
    //        href += '&';
    //      else
    //        href += '?';
    //        
    //      href += 'simple=1';  // con simple=1 cargamos la página sin cabecera, ni pie, ni columnas.
    //      
    //      //var pre = '<img src="imagenes/loading.gif" id="loading"/>';
    //      $(this).click(function(){
    //         $.scrollTo('#top',800);
    //         //$(".columnCenter").hide().after(pre).load(href, function() {$("#loading").remove();$(this).fadeIn('slow')});
    //         $(".columnCenter").load(href);
    //         return false;
    //      });
    //    });


    // Mensaje cargando cuando ajax este procesando
    $('<div id="busy">Cargando...</div>')
        .ajaxStart(function() { $(this).show(); })
        .ajaxStop(function() { $(this).hide(); })
        .ajaxSend(function() { $(this).show(); })
        .ajaxComplete(function() { $(this).hide(); })
        .appendTo('body');

    //inicializamos History
    $.history.init(function(url) {
        if (url != '') {
            var data = url.split('|');
            loadHtml(data[0], data[1]);
        }
    });

    //activamoes la visualización jquery.magnify (permiter zoom y rotar imágenes)
    $('.magnify').magnify({
        resizable: false,
        initMaximized: true,
        headerToolbar: [
            'close'
        ],
    });

    //activamos la visualización fancybox (para imagenes)
    $(".fancybox").fancybox();

    //activamos la visualización fancybox para el formulario de edición (showrecord.aspx)
    $(".editbox").fancybox({
        'hideOnContentClick': false,
        'autoDimensions': false,
        'frameWidth': 700,
        'frameHeight': 450,
        'onComplete': function() {
            editorActions();
        }
    });


    // // mostramos el menu contextual
    // $(".pagina").contextMenu({
    //     menu: 'contextAdmin'
    // }, function(action, el, pos) {

    //     var pageaction = '';
    //     switch (action) {
    //         case 'edit':
    //             pageaction = 'showrecord';
    //             break;
    //         case 'delete':
    //             pageaction = 'deleterecord';
    //             break;
    //         case 'quit':
    //             return;
    //             break;
    //     }

    //     var id = $(el).attr('id');
    //     var t = id.split(":")[0];
    //     var n = id.split(":")[1];
    //     var table = '';
    //     var idName = '';
    //     switch (t) {
    //         case 'P':
    //             idName = 'idPagina';
    //             table = 'Paginas';
    //             break;
    //         case 'M':
    //             idName = 'idModulo';
    //             table = 'Modulos';
    //             break;
    //     }

    //     document.location.href = directorioPortal + 'admin/editor/' + pageaction + '.aspx?q=&tablename=' + // table + '&fld=' + idName + '&val=' + n + '&fldtype=System.Integer&page=1';
    // });

});

/*
function editorActions() {
    //al ser una pantalla que se carga en un DIV, con la función callbackOnShow,
    //ejecutamos comandos al realizarse la carga.
    //activamos las pestañas
    $("#tabs").tabs();

    loadEditors();

    //Activamos el procesamiento del formulario mediante json
    $(".frmForm").ajaxForm({
        dataType: 'json',
        success: processShow
    });

    //Formulario de login (cuando se pierde la sesión,
    //ocurre que el formulario de login (conectar.aspx) es cargado en la ventana del fancybox,
    $(".frmLogin").ajaxForm({
        dataType: 'json',
        success: processLogin
    });
}
*/

function getQuery(name) {
    if (name = (new RegExp('[?&]' + encodeURIComponent(name) + '=([^&]*)')).exec(location.search))
        return decodeURIComponent(name[1]);
}


function loadHtml(dest, page) {
    $(dest).load(page, function() {

        //Activamos el procesamiento del formulario mediante json
        $(".frmForm").ajaxForm({
            dataType: 'json',
            success: processShow,
            resetForm: false,
            error: function(response, status, err) {
                alert(err);
            }
        });

        //guardamos en History
        var url = page.replace(/^.*#/, '');
        $.history.load(dest + '|' + url);
    });
}

/*
function loadEditors() {
    var $editors = $("textarea.ckeditor");
    if ($editors.length) {
        $editors.each(function() {
            var editorID = $(this).attr("id");
            var instance = CKEDITOR.instances[editorID];
            if (instance) { CKEDITOR.remove(instance); }
            CKEDITOR.replace(editorID,

            {
                filebrowserBrowseUrl: directorioPortal + 'javascript/ckfinder/ckfinder.html',
                filebrowserImageBrowseUrl: directorioPortal + 'javascript/ckfinder/ckfinder.html?Type=Images',
                filebrowserFlashBrowseUrl: directorioPortal + 'javascript/ckfinder/ckfinder.html?Type=Flash',
                filebrowserUploadUrl: directorioPortal + 'javascript/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Files',
                filebrowserImageUploadUrl: directorioPortal + 'javascript/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Images',
                filebrowserFlashUploadUrl: directorioPortal + 'javascript/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Flash'
            }

             );
        });
    }
}
*/