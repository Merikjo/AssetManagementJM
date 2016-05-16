﻿/// <reference path="../typings/jquery/jquery.d.ts" />

class AssignLocationModel {
    public AssetCode: string; 
    public LocationCode: string;
    }

function initAssetAssignment() {
    $("#AssignAssetButton").click(function () {
        //alert("Toimii!");

        var locationCode: string = $("#LocationCode").val();
        var assetCode: string = $("#assetCode").val();
        alert("L: " + locationCode + ", A:" + assetCode);

        //määritetään muuttuja:
        var data: AssignLocationModel = new AssignLocationModel();
        data.LocationCode = locationCode;
        data.AssetCode = assetCode;

        //lähetetään JSON-muotoista dataa palvelimelle
        $.ajax({
            type: "POST",
            url: "/Asset/AssignLocation",
            data: JSON.stringify(data),
            contentType: "application",
            success: function (data){
            if(data.success == true) {
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