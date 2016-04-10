/// <reference path="../typings/jquery/jquery.d.ts" />
var AssignLocationModel = (function () {
    function AssignLocationModel() {
    }
    return AssignLocationModel;
}());
function initAssetAssignment() {
    $("#AssignAssetButton").click(function () {
        //alert("Toimii!");
        var locationCode = $("#LocationCode").val();
        var assetCode = $("#assetCode").val();
        alert("L: " + locationCode + ", A:" + assetCode);
        var data = new AssignLocationModel();
        data.LocationCode = locationCode;
        data.AssetCode = assetCode;
        //lähetetään JSON-muotoista dataa palvelimelle
        $.ajax({
            type: "POST",
            url: "/Asset/AssignLocation",
            data: JSON.stringify(data),
            contentType: "application",
            success: function (data) {
                if (data.success == true) {
                    alert("Asset successfully assigned.");
                }
                else {
                    alert("There was an error: " + data.error);
                }
            },
            dataType: "json"
        });
    });
}
