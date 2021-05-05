/*
 Milonic DHTML Website Navigation Menu - Version 3.4
 Written by Andy Woolley - Copyright 2002 (c) Milonic Solutions Limited. All Rights Reserved.
 Please visit http://www.milonic.co.uk/menu or e-mail menu3@milonic.com for more information.
 
 The Free use of this menu is only available to Non-Profit, Educational & Personal web sites.
 Commercial and Corporate licenses  are available for use on all other web sites & Intranets.
 All Copyright notices MUST remain in place at ALL times and, please keep us informed of your 
 intentions to use the menu and send us your URL.
*/


//The following line is critical for menu operation, and MUST APPEAR ONLY ONCE. If you have more than one menu_array.js file rem out this line in subsequent files
menunum = 0;
menus = new Array();
_d = document;

function addmenu() {
    menunum++;
    menus[menunum] = menu;
}

function dumpmenus() {
    mt = "<script language=javascript>";
    for (a = 1; a < menus.length; a++) {
        mt += " menu" + a + "=menus[" + a + "];";
    }
    mt += "<\/script>";
    _d.write(mt);
}

//Please leave the above line intact. The above also needs to be enabled if it not already enabled unless this file is part of a multi pack.


////////////////////////////////////
// Editable properties START here //
////////////////////////////////////

// Special effect string for IE5.5 or above please visit http://www.milonic.co.uk/menu/filters_sample.php for more filters
if (navigator.appVersion.indexOf("MSIE 6.0") > 0) {
    effect = "Fade(duration=0.2);Alpha(style=0,opacity=100);Shadow(color='#777777', Direction=135, Strength=5)";
} else {
    effect = "Shadow(color='#777777', Direction=135, Strength=5)"; // Stop IE5.5 bug when using more than one filter
}

var consultaServicios = ('pagina.aspx?id=311&wFechaHasta={fecha}&wFechaDesde={fecha}');
var CerrarSesion = ('servicios/desconectar.aspx');


timegap = 500; // The time delay for menus to remain visible
followspeed = 5; // Follow Scrolling speed
followrate = 40; // Follow Scrolling Rate
suboffset_top = 10; // Sub menu offset Top position 
suboffset_left = 10; // Sub menu offset Left position

plain_style = [
// style1 is an array of properties. You can have as many property arrays as you need. This means that menus can have their own style.
    "white", // Mouse Off Font Color
    "2B426B", // Mouse Off Background Color
    "white", // Mouse On Font Color
    "3E3E3E", // Mouse On Background Color
    "white", // Menu Border Color 
    11, // Font Size in pixels
    "normal", // Font Style (italic or normal)
    "normal", // Font Weight (bold or normal)
    "Tahoma,sans serif", // Font Name
    6, // Menu Item Padding
    "", // Sub Menu Image (Leave this blank if not needed)
    4, // 3D Border & Separator bar
    "A9ADAD", // 3D High 
    "707072", // 3D Low Color
    , // Current Page Item Font Color (leave this blank to disable)
    , // Current Page Item Background Color (leave this blank to disable)
    , // Top Bar image (Leave this blank to disable)
    "white", // Menu Header Font Color (Leave blank if headers are not needed)
    "#4F6376", // Menu Header Background Color (Leave blank if headers are not needed)
    "#FFFFFF", // Menu Item Separator Color
];
addmenu(menu = [
// This is the array that contains your menu properties and details
    "mainmenu", // Menu Name - This is needed in order for the menu to be called
    48, // Menu Top - The Top position of the menu in pixels
    0, // Menu Left - The Left position of the menu in pixels
    180, // Menu Width - Menus width in pixels
    0, // Menu Border Width 
    , // Screen Position - here you can use "center;left;right;middle;top;bottom" or a combination of "center:middle"
    plain_style, // Properties Array - this is set higher up, as above
    1, // Always Visible - allows the menu item to be visible at all time (1=on/0=off)
    "center", // Alignment - sets the menu elements text alignment, values valid here are: left, right or center
    , // Filter - Text variable for setting transitional effects on menu activation - see above for more info
    0, // Follow Scrolling - Tells the menu item to follow the user down the screen (visible at all times) (1=on/0=off)
    1, // Horizontal Menu - Tells the menu to become horizontal instead of top to bottom style (1=on/0=off)
    , // Keep Alive - Keeps the menu visible until the user moves over another menu or clicks elsewhere on the page (1=on/0=off)
    , // Position of TOP sub image left:center:right
    , // Set the Overall Width of Horizontal Menu to 100% and height to the specified amount (Leave blank to disable)
    , // Right To Left - Used in Hebrew for example. (1=on/0=off)
    , // Open the Menus OnClick - leave blank for OnMouseover (1=on/0=off)
    , // ID of the div you want to hide on MouseOver (useful for hiding form elements)
    , // Background image for menu when BGColor set to transparent.
    , // Scrollable Menu
    , // Reserved for future use
// "Menu Item Text", "URL", "Alternate URL for submenu holders", "Status Text", "Separator Bar Width"
    , "&nbsp;&nbsp;&nbsp;Servicios de agencia&nbsp;&nbsp;&nbsp;", "show-menu=SERVICIOS DE AGENCIA",,, 1, "&nbsp;&nbsp;&nbsp;Informaci&oacute;n del sector&nbsp;&nbsp;&nbsp;", "show-menu=INFORMACION DEL SECTOR",,, 1, "&nbsp;&nbsp;&nbsp;Intranet&nbsp;&nbsp;&nbsp;", "show-menu=INTRANET",,, 1
]);
addmenu(menu = [
    "SERVICIOS DE AGENCIA",,, 180, 1, "", plain_style,,, effect,,,,,,,, "form1",,,,, "&nbsp;&nbsp;&nbsp;Consulta de servicios", "javascript:location.href=(consultaServicios)",, "Consulta de servicios", 1
]);
var maj = ('p_marco.aspx'); //)",,"Marco jur&iacute;dico",1
var las = ('p_las_asociaciones.aspx'); //",,"Las asociaciones",1
var edi = ('p_enlaces_interes.aspx'); //",,"Enlaces de inter&eacute;s",1
var nds = ('p_noticias_sector.aspx'); //",,"Noticias del sector",1


addmenu(menu = [
    "INFORMACION DEL SECTOR",,, 162, 1, "", plain_style,,, effect,,,,,,,, "form1",,,,, "&nbsp;&nbsp;&nbsp;Marco jur&iacute;dico", "javascript:abrir(maj)",, "Marco jur&iacute;dico", 1, "&nbsp;&nbsp;&nbsp;Las asociaciones", "javascript:abrir(las)",, "Las asociaciones", 1, "&nbsp;&nbsp;&nbsp;Enlaces de inter&eacute;s", "javascript:abrir(edi)",, "Enlaces de inter&eacute;s", 1,"&nbsp;&nbsp;&nbsp;Noticias del sector", "javascript:abrir(nds)",, "Noticias del sector", 1
]);
addmenu(menu = [
    "INTRANET",,, 162, 1, "", plain_style,,, effect,,,,,,,, "form1",,,,
    , "&nbsp;&nbsp;&nbsp;Cerrar sesi&oacute;n", "javascript:location.href=(CerrarSesion)",, "Cerrar sesi&oacute;n", 1
]);
dumpmenus();