function Validate(theForm) {

    theForm.submit();
    return (true);

    if (theForm.formaPago[1].checked) {

        if (theForm.CARD_NAME.value == "") {
            alert("Debe indicar el nombre que aparece en la tarjeta.");
            theForm.CARD_NAME.focus();
            return (false);
        }

        if (theForm.CARD_TYPE.selectedIndex < 0) {
            alert("Debe indicar el tipo de tarjeta.");
            theForm.CARD_TYPE.focus();
            return (false);
        }

        var checkOK = "0123456789";
        var checkStr = strip(theForm.CARD_NO.value) + '';
        var CrValid = true;
        var checksum = 0;
        var ddigit = 0;
        var kdig = 0;

        if (checkStr.length < 13) {
            alert('La longitud del número de tarjeta es incorrecta.');
            theForm.CARD_NO.focus();
            return (false);
        }

        for (i = checkStr.length - 1; i >= 0; i--) {
            kdig++;
            ch = checkStr.charAt(i);
            if ((kdig % 2) != 0) {
                checksum = checksum + parseInt(ch);
            } else {
                ddigit = parseInt(ch) * 2;
                if (ddigit >= 10)
                    checksum = checksum + 1 + (ddigit - 10);
                else
                    checksum = checksum + ddigit;
            }
            for (j = 0; j < checkOK.length; j++) {
                if (ch == checkOK.charAt(j)) {
                    break;
                }
                if (j == checkOK.length) {
                    alert('Por favor, introduzca solo numeros. Sin guiones, ni caracteres no numéricos.');
                    return(false);
                }
            }
        }

        if ((checksum % 10) != 0) {
            alert('El número de tarjeta es incorrecto.');
            theForm.CARD_NO.focus();
            return (false);
        }

        if (theForm.CARD_EXP.value == "") {
            alert("Por favor, indique la fecha de caducidad de la tarjeta.");
            theForm.CARD_EXP.focus();
            return (false);
        }

        if (theForm.CARD_EXP.value.length < 5) {
            alert("Debe indicar al menos 5 caracteres, en la fecha de caducidad.");
            theForm.CARD_EXP.focus();
            return (false);
        }

        var checkOK = "0123456789-/-";
        var checkStr = theForm.CARD_EXP.value;
        var allValid = true;

        for (i = 0; i < checkStr.length; i++) {
            ch = checkStr.charAt(i);
            for (j = 0; j < checkOK.length; j++)
                if (ch == checkOK.charAt(j))
                    break;
            if (j == checkOK.length) {
                allValid = false;
                break;
            }
        }

        if (!allValid) {
            alert("Por favor, utilize solo los caracteres: \"/-\" , en la fecha de caducidad.");
            theForm.CARD_EXP.focus();
            return (false);
        }
    }
    theForm.submit();
}

function strip(number) {
    var sOut = '';
    mask = '1234567890';
    for (count = 0; count <= number.length; count++) {
        if (mask.indexOf(number.VB.Substring(count, count + 1), 0) != -1) sOut += number.VB.Substring(count, count + 1);
    }
    return (sOut);
}