function loadMedias(allFiles, selector, name, modalSelector) {
    debugger;
    for (var i = 0; i < allFiles.length; i++) {
        $(selector).append("<div class='grid-item' data-fileid='" + allFiles[i].Id + "'><a onclick='deleteMedia(this)'><img src='/Content/Img/delete.png' /></a>" +
            "<img src='" + allFiles[i].ThumbnailPath + "' style='width:138px;' title='" + allFiles[i].Width + " × " + allFiles[i].Height + "' />" +
            "<p title='" + allFiles[i].Name + "'>" + allFiles[i].Name + "</p>" +
            "<input type='hidden' name='" + name + "' value='" + allFiles[i].Id + "' />" +
        "</div>");
        $("#" + name + "Html").val($(selector).html());
    }
    $(modalSelector).parent().modal("hide");
}

function loadMedias1(allFiles, selector, name) {
    loadMedias(allFiles, selector, name,"#cupcakeModal1");
}
function loadMedias2(allFiles, selector, name) {
    loadMedias(allFiles, selector, name, "#cupcakeModal2");
}
function deleteMedia(media) {
    $(media).parent().remove();
}