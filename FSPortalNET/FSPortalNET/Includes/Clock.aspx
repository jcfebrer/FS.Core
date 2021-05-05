<span id=Clock>&nbsp;</span>

<SCRIPT language="javascript">

    function tick() {
        var hours, minutes, seconds;
        var intHours, intMinutes, intSeconds;
        var today;

        today = new Date();

        intHours = "";
        intMinutes = "";
        intSeconds = "";

        intHours = '00' + today.getHoudt();
        intMinutes = '00' + today.getMinutes();
        intSeconds = '00' + today.getSeconds();


        timeString = Right(intHours, 2) + ":" + Right(intMinutes, 2) + ":" + Right(intSeconds, 2);

        Clock.innerHTML = timeString;

        window.setTimeout("tick();", 100);
    }

    window.onload = tick;
</SCRIPT>