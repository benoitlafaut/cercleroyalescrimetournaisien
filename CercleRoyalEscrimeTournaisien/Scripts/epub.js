function addEvent(el, name, func, bool) {
    if (el.addEventListener) el.addEventListener(name, func, bool);
    else if (el.attachEvent) el.attachEvent('on' + name, dunc);
    else el['on' + name] = func;
}

function delay(callback, ms) {
    var timer = 0;
    return function () {
        var context = this, args = arguments;
        clearTimeout(timer);
        timer = setTimeout(function () {
            callback.apply(context, args);
        }, ms || 0);
    };
}

function changeBluetooth(numeroToChange, checked) {
    $("#bluetoothEspagnol").prop('checked', false)
    $("#bluetoothNeerlandais").prop('checked', false)
    $("#bluetoothAllemand").prop('checked', false)
    $("#bluetoothAnglais").prop('checked', false)


    switch (numeroToChange) {
        case 1:
            $("#bluetoothEspagnol").prop('checked', checked)
            break;
        case 2:
            $("#bluetoothNeerlandais").prop('checked', checked)
            break;
        case 3:
            $("#bluetoothAllemand").prop('checked', checked)
            break;
        case 4:
            $("#bluetoothAnglais").prop('checked', checked)
            break;

    }

    OpenFileEpub('0', '3');
}

function PlaySound(langue) {
    if (langue == "fr") {
        var audioElement = document.getElementById('audioElement1');
        var textToSpeak = $("#row_1").val() + $("#row_2").val() + $("#row_3").val() + $("#row_4").val();
        var language = 'fr-FR_ReneeV3Voice';
        audioElement.src = 'https://text-to-speech-demo.ng.bluemix.net/api/v3/synthesize?' + 'text=' + encodeURIComponent(textToSpeak) + '&voice=' + language + '&download=true&accept=audio%2Fmp3';
        audioElement.pause();
        audioElement.load();
        audioElement.play();
        $("#playSoundFR").addClass("colorRed");


        audioElement.onended = function () {
            $("#playSoundFR").removeClass("colorRed");

            if ($("#IsToListen").val() == '1') {
                PlaySound("Other");
            }

            $("#IsToListen").val('0');
        };
    }
    else {
        $("#playSoundOther").addClass("colorRed");

        var language = "";
        if ($("#bluetoothEspagnol").prop('checked')) { var language = 'es-ES_LauraV3Voice'; }
        if ($("#bluetoothNeerlandais").prop('checked')) { var language = 'nl-NL_EmmaVoice'; }
        if ($("#bluetoothAllemand").prop('checked')) { var language = 'de-DE_DieterV3Voice'; }
        if ($("#bluetoothAnglais").prop('checked')) { var language = 'en-GB_KateV3Voice'; }

        if (language == "") {
            endLectureAutomatique();

            return;
        }

        var audioElement = document.getElementById('audioElement1');
        var textToSpeak = $("#translateText").val();

        audioElement.src = 'https://text-to-speech-demo.ng.bluemix.net/api/v3/synthesize?' + 'text=' + encodeURIComponent(textToSpeak) + '&voice=' + language + '&download=true&accept=audio%2Fmp3';
        audioElement.pause();
        audioElement.load();
        audioElement.play();

        audioElement.onended = function () {
            endLectureAutomatique();
        };
    }
}

function endLectureAutomatique() {
    $("#playSoundOther").removeClass("colorRed");
    $(".fa-headphones").removeClass("fa-spin");

    $("#bluetoothAnglais").prop("readonly", false);
    $("#bluetoothAnglais").prop("disabled", false);
    $("#bluetoothEspagnol").prop("readonly", false);
    $("#bluetoothEspagnol").prop("disabled", false);
    $("#bluetoothNeerlandais").prop("readonly", false);
    $("#bluetoothNeerlandais").prop("disabled", false);
    $("#bluetoothAllemand").prop("readonly", false);
    $("#bluetoothAllemand").prop("disabled", false);
}

function PlaySound(langue) {
    if (langue == "fr") {
        var audioElement = document.getElementById('audioElement1');
        var textToSpeak = $("#row_1").val() + $("#row_2").val() + $("#row_3").val() + $("#row_4").val();
        var language = 'fr-FR_ReneeV3Voice';
        audioElement.src = 'https://text-to-speech-demo.ng.bluemix.net/api/v3/synthesize?' + 'text=' + encodeURIComponent(textToSpeak) + '&voice=' + language + '&download=true&accept=audio%2Fmp3';
        audioElement.pause();
        audioElement.load();
        audioElement.play();
        $("#playSoundFR").addClass("colorRed");


        audioElement.onended = function () {
            $("#playSoundFR").removeClass("colorRed");

            if ($("#IsToListen").val() == '1') {
                PlaySound("Other");
            }

            $("#IsToListen").val('0');
        };
    }
    else {
        $("#playSoundOther").addClass("colorRed");

        var language = "";
        if ($("#bluetoothEspagnol").prop('checked')) { var language = 'es-ES_LauraV3Voice'; }
        if ($("#bluetoothNeerlandais").prop('checked')) { var language = 'nl-NL_EmmaVoice'; }
        if ($("#bluetoothAllemand").prop('checked')) { var language = 'de-DE_DieterV3Voice'; }
        if ($("#bluetoothAnglais").prop('checked')) { var language = 'en-GB_KateV3Voice'; }

        if (language == "") {
            endLectureAutomatique();

            return;
        }

        var audioElement = document.getElementById('audioElement1');
        var textToSpeak = $("#translateText").val();

        audioElement.src = 'https://text-to-speech-demo.ng.bluemix.net/api/v3/synthesize?' + 'text=' + encodeURIComponent(textToSpeak) + '&voice=' + language + '&download=true&accept=audio%2Fmp3';
        audioElement.pause();
        audioElement.load();
        audioElement.play();

        audioElement.onended = function () {
            endLectureAutomatique();
        };
    }
}

function endLectureAutomatique() {
    $("#playSoundOther").removeClass("colorRed");
    $(".fa-headphones").removeClass("fa-spin");

    $("#bluetoothAnglais").prop("readonly", false);
    $("#bluetoothAnglais").prop("disabled", false);
    $("#bluetoothEspagnol").prop("readonly", false);
    $("#bluetoothEspagnol").prop("disabled", false);
    $("#bluetoothNeerlandais").prop("readonly", false);
    $("#bluetoothNeerlandais").prop("disabled", false);
    $("#bluetoothAllemand").prop("readonly", false);
    $("#bluetoothAllemand").prop("disabled", false);
}

function clickTranslate(texteToTranslate, langueDestination) {

    //ger , dut, spa, eng

    var data = JSON.stringify({
        format: "text",
        from: "fra",
        input: texteToTranslate,
        to: langueDestination,
        options: { sentenceSplitter: true, origin: "translation.web", contextResults: true, languageDetection: true }

    })

    $.ajax({
        url: 'https://api.reverso.net/translate/v1/translation',
        type: "POST",
        data: data,
        dataType: 'json',
        jsonp: 'oncomplete',
        contentType: "application/json",
        complete: function (request, status) {
        },

        success: function (result, status) {
            var splitted = '';
            var splittedFormatted = '';

            for (var i = 0; i < result.translation.length; i++) {
                splitted = splitted + result.translation[i];
            }


            var arraySplittedFormatted = splitted.split("##");

            for (var i = 0; i < arraySplittedFormatted.length; i++) {
                splittedFormatted += "• " + arraySplittedFormatted[i].replace("#", "").trim() + "\r\n" + "\r\n";
            }

            $("#translateText").val(splittedFormatted);
        },
        error: function (a, b, c) {
            alert(b + '-' + c);
        }
    });
}

function clickOpen() {
    var audioElement = document.getElementById('audioElement1');
    var textToSpeak = 'hello how are you?';
    textToSpeak = 'hello comment ca va?';
    textToSpeak = 'hola como estas?';
    textToSpeak = 'gutendag';
    textToSpeak = 'goeden morgen';
    var language = 'en-US_AllisonVoice';
    language = 'fr-FR_ReneeV3Voice';
    language = 'es-ES_LauraV3Voice';
    language = 'de-DE_DieterV3Voice';
    language = 'nl-NL_LiamVoice';
    audioElement.src = 'https://text-to-speech-demo.ng.bluemix.net/api/v3/synthesize?' + 'text=' + encodeURIComponent(textToSpeak) + '&voice=' + language + '&download=true&accept=audio%2Fmp3';
    audioElement.pause();
    audioElement.load();
    audioElement.play();
}


function CreateFileEpub() {
    var formData = new FormData();
    formData.append('name', $("#txtName").val());

    var allfilesSelect = $("#GetFile").get(0).files;
    if (allfilesSelect.length == 0) { alert('il n y a pas de fichier selectionné'); return; }
    if (allfilesSelect[0].name.split('.').pop().toLowerCase() != "epub") { alert('le fichier n est pas un fichier epub.'); return; }

    for (var i = 0; i < allfilesSelect.length; i++) {
        formData.append(allfilesSelect[i].name, allfilesSelect[i]);
    }

    $.ajax({
        url: "/CercleRoyalEscrimeTournaisien/Home/Transform",
        type: 'POST',
        cache: false,
        contentType: false,
        processData: false,
        data: formData,
        complete: function (response) {
            const FileName = $("#GetFile").get(0).files[0].name.replace(".epub", ".epublaf");

            var dataArray = response.responseJSON.split('||');

            const textToBLOB = new Blob([dataArray.join("\r\n")], { type: 'text/plain' });

            let newLink = document.createElement("a");
            newLink.download = FileName;

            if (window.webkitURL != null) {
                newLink.href = window.webkitURL.createObjectURL(textToBLOB);
            }
            else {
                newLink.href = window.URL.createObjectURL(textToBLOB);
                newLink.style.display = "none";
                document.body.appendChild(newLink);
            }

            newLink.click();

            alert('le fichier a été transformé et téléchargé.')
        },
        error: function (response) {
            alert("Error. " + response.responseText);  //
        }
    });
}
function changeCursor(element) {
    $('#numeroPageHidden').val(element.value);
    OpenFileEpub('0');
}

function OpenFileEpub(IsToListen, nombreLignesALire) {
    $("#translateText").val('');
    var allfilesSelect = $("#GetFile").get(0).files;

    if (allfilesSelect.length == 0) {
        var data = $("#EpubDIV :input").serializeArray();
        var processdata = 'application/x-www-form-urlencoded; charset=UTF-8';
        var contentType = 'application/x-www-form-urlencoded; charset=UTF-8';        
        data.push({ name: "IsToListenNew", value: IsToListen });
    }
    else {
        var data = new FormData();
        for (var i = 0; i < allfilesSelect.length; i++) {
            data.append(allfilesSelect[i].name, allfilesSelect[i]);
        }
        data.append('IsToListenNew', IsToListen);
        var processdata = false;
        var contentType = false;
    }    

    $.ajax({
        url: "/Home/OpenEpub",
        type: 'POST',
        cache: false,
        data: data,
        dataType: 'json',
        processData: processdata,
        contentType: contentType,
        success: function (result, IsToListen) {
            $('#EpubDIV').html(result.modelViewEpubFile);            
            $('#myRange').attr('max', $("#CountRowsEpub").val());            
            $('#myRange').attr('value', $("#CurrentLigne").val());
            $("#numeroLigne").val($("#CurrentLigne").val() + " / " + $("#CountRowsEpub").val());

            

            //translateText();


            //if ($("#IsToListen").val() == '1') {
            //    PlaySound('fr');
            //}
            //if ($("#IsToListen").val() == '2') {

            //    setTimeout(function () { PlaySound('other'); }, 3000);
            //}
        },
        failure: function (response) {

        },
        error: function (response) {

        }
    });

    //var formData = new FormData();
    //formData.append('name', $("#txtName").val());
    //formData.append('pagination', $('#numeroPageHidden').val());
    //formData.append('IsToListen', IsToListen);
    //formData.Push(data);
    //formData.append('nombreLignesALire', nombreLignesALire);


    
   
   
    //if (allfilesSelect.length == 0) { alert('il n y a pas de fichier selectionné'); return; }
   // if (allfilesSelect[0].name.split('.').pop().toLowerCase() != "epublaf") { alert('le fichier n est pas un fichier epublaf.'); return; }


    //if (IsToListen == "1" || IsToListen == "2") {
    //    $(".fa-headphones").addClass("fa-spin");
    //    $("#bluetoothAnglais").prop("readonly", true);
    //    $("#bluetoothAnglais").prop("disabled", true);
    //    $("#bluetoothEspagnol").prop("readonly", true);
    //    $("#bluetoothEspagnol").prop("disabled", true);
    //    $("#bluetoothNeerlandais").prop("readonly", true);
    //    $("#bluetoothNeerlandais").prop("disabled", true);
    //    $("#bluetoothAllemand").prop("readonly", true);
    //    $("#bluetoothAllemand").prop("disabled", true);
    //}

}

function translateText() {
    var langueDestination = "";
    if ($("#bluetoothAnglais").prop('checked') == true) { langueDestination = "eng"; }
    if ($("#bluetoothEspagnol").prop('checked') == true) { langueDestination = "spa"; }
    if ($("#bluetoothNeerlandais").prop('checked') == true) { langueDestination = "dut"; }
    if ($("#bluetoothAllemand").prop('checked') == true) { langueDestination = "ger"; }

    if (langueDestination == "") { return; }

    texteToTranslate = row_1.value + "##" + row_2.value + "##" + row_3.value + "##" + row_4.value;
    clickTranslate(texteToTranslate, langueDestination);

}

function PreviousPagination() {
    var currentValue = parseInt($("#numeroPageHidden").val(), 10);
    $('#numeroPageHidden').val(currentValue - 4);
}

function NextPagination() {
    var currentValue = parseInt($("#numeroPageHidden").val(), 10);
    $('#numeroPageHidden').val(currentValue + 4);
}

function NextPaginationAutomatic() {
    var allfilesSelect = $("#GetFile").get(0).files;
    if (allfilesSelect.length == 0) { alert('il n y a pas de fichier selectionné'); return; }

    if (myInterval == "") {
        everyTime();
        myInterval = setInterval(everyTime, 5000);
    }
    else {
        $(".fa-headphones").removeClass("fa-spin");
        $(".fa-podcast").removeClass("fa-spin");


        clearInterval(myInterval);
        myInterval = "";
    }
}

//function ChangeLayout(isChanged) {
//    $("#textFrancais").css('display', 'none');
//    $("#buttonsForBlueToothDiv").css('display', 'none');
//    $("#texteTranslate").css('display', 'none');
//    $("#partialDIV").css('height', '32%')
//    $("#texteTranslate").css('height', '49%')
//    $("#translateText").css('height', '79%')

//    if (isChanged) {
//        switch (layoutPosition) {
//            case 0:
//                $("#textFrancais").css('display', '');
//                $("#partialDIV").css('height', '100%')
//                layoutPosition = 1;
//                break;
//            case 1:
//                $("#texteTranslate").css('display', '');
//                $("#partialDIV").css('height', '')
//                $("#texteTranslate").css('height', '')
//                $("#translateText").css('height', '100%')
//                layoutPosition = 2;
//                break;

//            case 2:
//                $("#textFrancais").css('display', '');
//                $("#texteTranslate").css('display', '');
//                $("#buttonsForBlueToothDiv").css('display', '');
//                layoutPosition = 0;

//                break;
//        }

//    }
//    else {
//        switch (layoutPosition) {
//            case 1:
//                $("#textFrancais").css('display', '');
//                $("#partialDIV").css('height', '100%')
//                break;
//            case 2:
//                $("#texteTranslate").css('display', '');
//                $("#partialDIV").css('height', '')
//                $("#texteTranslate").css('height', '')
//                $("#translateText").css('height', '100%')
//                break;

//            case 0:
//                $("#textFrancais").css('display', '');
//                $("#texteTranslate").css('display', '');
//                $("#buttonsForBlueToothDiv").css('display', '');
//                break;
//        }
//    }

//}

function NextPaginationAutomaticOtherLanguageThanFr() {
    var language = "";
    if ($("#bluetoothEspagnol").prop('checked')) { var language = 'es-ES_LauraV3Voice'; }
    if ($("#bluetoothNeerlandais").prop('checked')) { var language = 'nl-NL_EmmaVoice'; }
    if ($("#bluetoothAllemand").prop('checked')) { var language = 'de-DE_DieterV3Voice'; }
    if ($("#bluetoothAnglais").prop('checked')) { var language = 'en-GB_KateV3Voice'; }


    if (language == "") { alert("vous n'avez pas sélectionné de langue étrangère."); return; }
    if (myInterval == "") {
        everyTimeForOtherLangage();
        myInterval = setInterval(everyTimeForOtherLangage, 5000);
    }
    else {
        $(".fa-headphones").removeClass("fa-spin");
        $(".fa-ravelry").removeClass("fa-spin");


        clearInterval(myInterval);
        myInterval = "";
    }
}

function NextPaginationAutomaticOneTime() {
    var allfilesSelect = $("#GetFile").get(0).files;
    if (allfilesSelect.length == 0) { alert('il n y a pas de fichier selectionné'); return; }

    NextPagination();
    OpenFileEpub('1', '3');
}

function NextPaginationAutomaticOneTimeForOtherLanguage() {
    NextPagination();
    OpenFileEpub('2', '3');
}

function everyTime() {
    if ($("#playSoundFR").hasClass("colorRed")) {
        return;
    }
    if ($("#playSoundOther").hasClass("colorRed")) {
        return;
    }

    NextPaginationAutomaticOneTime();
    $(".fa-podcast").addClass("fa-spin");
}

function everyTimeForOtherLangage() {

    if ($("#playSoundOther").hasClass("colorRed")) {
        return;
    }

    NextPaginationAutomaticOneTimeForOtherLanguage();
    $(".fa-ravelry").addClass("fa-spin");
}

function showPopup() {
    $("#popUp").dialog({
        modal: true,
        resizable: false
    });
}