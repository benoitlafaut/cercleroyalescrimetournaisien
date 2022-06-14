var myInterval= "";

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
        url: "/Home/Transform",
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

function changeCurrentLigne(element) {
    $("#CurrentLigne").val(element.value);
}

function ChangeEpub(url, IsToListenNew, addLines) {
    $("#translateText").val('');
    var allfilesSelect = $("#GetFile").get(0).files;

    if (allfilesSelect.length == 0) {
        var data = $("#EpubDIV :input").serializeArray();
        var processdata = 'application/x-www-form-urlencoded; charset=UTF-8';
        var contentType = 'application/x-www-form-urlencoded; charset=UTF-8';
        data.push({ name: "IsToListenNew", value: IsToListenNew });
        data.push({ name: "addLines", value: addLines });
    }
    else {
        var data = new FormData();
        for (var i = 0; i < allfilesSelect.length; i++) {
            data.append(allfilesSelect[i].name, allfilesSelect[i]);
        }
        data.append('IsToListenNew', IsToListenNew);
        data.append('addLines', addLines );
        var processdata = false;
        var contentType = false;
    }

    $.ajax({
        url: url,
        type: 'POST',
        cache: false,
        data: data,
        dataType: 'json',
        processData: processdata,
        contentType: contentType,
        success: function (result) {
            $('#EpubDIV').html(result.modelViewEpubFile);
            removeInputHidden();
            ChangeVisibilityDiv();

            if ($("#IsChangeLanguageEnglish").prop('checked') == true || $("#IsChangeLanguageAllemand").prop('checked') == true || $("#IsChangeLanguageEspagnol").prop('checked') == true || $("#IsChangeLanguageNeerlandais").prop('checked') == true) { translateText(); }
            if ($("#PartFrenchDIV").css('display') == 'none' ) { translateText();}

            

            if ($("#IsToListen").val() == 'True') {
                PlaySound('fr');
            }

            if ($("#IsVisible_PartFrenchDIV").val() == "False" || $("#IsVisible_PartTranslateDIV").val() == "False") {
                $("#PartFrenchDIV").css('height', '100%');
                $("#texteTranslate").css('height', '100%');
            }
        },
        failure: function (response) {},
        error: function (response) {}
    });
}

function ChangeVisibilityDiv() {
    ChangeVisibilityDivByElements($("#IsVisible_PartChargerFileDIV"), $("#PartChargerFileDIV"), $("#btn_PartChargerFileDIV"));
    ChangeVisibilityDivByElements($("#IsVisible_PartButtonsDIV"), $("#PartButtonsDIV"), $("#btn_PartButtonsDIV"));
    ChangeVisibilityDivByElements($("#IsVisible_PartSliderDIV"), $("#PartSliderDIV"), $("#btn_PartSliderDIV"));
    ChangeVisibilityDivByElements($("#IsVisible_PartFrenchDIV"), $("#PartFrenchDIV"), $("#btn_PartFrenchDIV"));
    ChangeVisibilityDivByElements($("#IsVisible_PartChangeLanguageDIV"), $("#PartChangeLanguageDIV"), $("#btn_PartChangeLanguageDIV"));
    ChangeVisibilityDivByElements($("#IsVisible_PartTranslateDIV"), $("#PartTranslateDIV"), $("#btn_PartTranslateDIV"));
}

function ChangeVisibilityDivByElements(element1, element2, element3) {
    if (element1.val() == 'True') {
        element2.css("display", "block");
        element3.css("color", "yellowgreen");
    }
    else {
        element2.css("display", "none");
        element3.css("color", "red");
    }
}

function ChangeVisibility(boutonElement, element) {
    if (element.css("display")  == "none") {
        element.css("display", "block");
        boutonElement.css("color", "yellowgreen");
    }
    else {
        element.css("display", "none");
        boutonElement.css("color", "red");        
    }
}

function SayHour() {
    var secondes = $('#hour').val() * 60000;
    setInterval(SayHourEachSeconde, secondes);
}

function SayHourEachSeconde() {
  var language = 'fr-FR_ReneeV3Voice';
    var dt = new Date();
    var time = dt.getHours() + " heures " + dt.getMinutes() ;
    textToSpeak = time;
    var audioElement = document.getElementById('audioElement1');
    audioElement.src = 'https://text-to-speech-demo.ng.bluemix.net/api/v3/synthesize?' + 'text=' + encodeURIComponent(textToSpeak) + '&voice=' + language + '&download=true&accept=audio%2Fmp3';
    audioElement.pause();
    audioElement.load();
    audioElement.play();
}

function ChangeLayout(url) {
    var data = C;
    var processdata = 'application/x-www-form-urlencoded; charset=UTF-8';
    var contentType = 'application/x-www-form-urlencoded; charset=UTF-8';

    $.ajax({
        url: url,
        type: 'POST',
        cache: false,
        data: data,
        dataType: 'json',
        processData: processdata,
        contentType: contentType,
        success: function (result) {
                $('#EpubDIV').html(result.modelViewEpubFile);
                ChangeEpub('/Home/ChangeEpub', '0', 0);
        },
        failure: function (response) { },
        error: function (response) { }
    });
}

function PlaySound(langue) {
    if (langue == "fr") {
        $(".fa-headphones").addClass("fa-spin");
        var audioElement = document.getElementById('audioElement1');
        var textToSpeak = "";
        var nombreDeLignesToSelect = $("#NombreDeLignesToSelect").val();
        for (var i = 0; i < nombreDeLignesToSelect; i++) {
            textToSpeak += $("#RowsEpubToShow" + "_" + i + "_").val();
        }

        var language = 'fr-FR_ReneeV3Voice';
        audioElement.src = 'https://text-to-speech-demo.ng.bluemix.net/api/v3/synthesize?' + 'text=' + encodeURIComponent(textToSpeak) + '&voice=' + language + '&download=true&accept=audio%2Fmp3';
        audioElement.pause();
        audioElement.load();
        audioElement.play();
        $("#playSoundFR").addClass("colorRed");


        audioElement.onended = function () {
            $("#playSoundFR").removeClass("colorRed");
            if ($("#IsToListen").val() == 'True') {
                PlaySound("Other");
            }

            $("#IsToListen").val('False');
        };
    }
    else {
        $("#playSoundOther").addClass("colorRed");

        var language = "";
        if ($("#IsChangeLanguageEspagnol").prop('checked') == true) { var language = 'es-ES_LauraV3Voice'; }
        if ($("#IsChangeLanguageNeerlandais").prop('checked') == true) { var language = 'nl-NL_EmmaVoice'; }
        if ($("#IsChangeLanguageAllemand").prop('checked') == true) { var language = 'de-DE_DieterV3Voice'; }
        if ($("#IsChangeLanguageEnglish").prop('checked') == true) { var language = 'en-GB_KateV3Voice'; }

        if (language == "") {
            endLectureAutomatique();

            return;
        }

        var audioElement = document.getElementById('audioElement1');
        var textToSpeak = $("#translateText").val();
        textToSpeak = textToSpeak.replaceAll('  \u2022  ', '');
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

function translateText() {
    var essai = [];
    var langueDestination = "";
    var texteToTranslate = "";
    if ($("#IsChangeLanguageEnglish").prop('checked') == true) { langueDestination = "eng"; }
    if ($("#IsChangeLanguageEspagnol").prop('checked') == true) { langueDestination = "spa"; }
    if ($("#IsChangeLanguageNeerlandais").prop('checked') == true) { langueDestination = "dut"; }
    if ($("#IsChangeLanguageAllemand").prop('checked') == true) { langueDestination = "ger"; }

    if (langueDestination == "") { return; }

    var nombreDeLignesToSelect = $("#NombreDeLignesToSelect").val();
    $("#translateText").val('');

    for (var i = 0; i < $("#RowsEpubToShowTranslated_Count").val(); i++) {
        $("#RowsEpubToShowTranslated_" + i + "_").val('');
    }
    
    for (var i = 0; i < nombreDeLignesToSelect; i++) {
        texteToTranslate = $("#RowsEpubToShow" + "_" + i + "_").val();

        if (texteToTranslate != '') {
            clickTranslate(essai,texteToTranslate, langueDestination, i);
        }        
    }
}

function fillTranslateText(nombreDeLignesToSelect) {
    var splittedFormatted = "";

    for (var i = 0; i < nombreDeLignesToSelect; i++) {
        splittedFormatted += "  \u2022  " + $("#RowsEpubToShowTranslated_" + i + "_").val() + "\r\n" + "\r\n";
    }

    $("#translateText").val(splittedFormatted);

    if ($("#IsToListenOnlyOtherLanguage").val() == 'True') {
        PlaySound('Other');
    }
}

function NextPaginationAutomatic(language) {
    if (myInterval == "") {
        everyTime(language);
        myInterval = setInterval(everyTime, 7000, language);
    }
    else {
        $(".fa-headphones").removeClass("fa-spin");
        $(".fa-podcast").removeClass("fa-spin");


        clearInterval(myInterval);
        myInterval = "";
    }
}




function everyTime(language) {
    if ($("#playSoundFR").hasClass("colorRed")) {
        return;
    }
    if ($("#playSoundOther").hasClass("colorRed")) {
        return;
    }

    ChangeEpub('/Home/ChangeEpub', language, 5);
    
}
function clickTranslate(essai,texteToTranslate, langueDestination, indexRow) {
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
            var splittedFormatted = '';

            for (var i = 0; i < request.responseJSON.translation.length; i++) {
                splittedFormatted += request.responseJSON.translation[i].replace("#", "").trim();
            }

            $("#RowsEpubToShowTranslated_" + indexRow + "_").val(splittedFormatted);

            var nombreDeLignesToSelect = $("#NombreDeLignesToSelect").val();
            fillTranslateText(nombreDeLignesToSelect);
        },
        success: function (result, status) {
        },
        error: function (a, b, c) {
            alert(b + '-' + c);
        }
    });
}