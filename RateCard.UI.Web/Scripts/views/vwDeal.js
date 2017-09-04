
function UpdateDealOutputs() {

    if (ValidateDealInputs() && AreTechUnitsSelected()) {
        UPDDealInputs();
        UPDTblTechInputs();
        RetrieveGenNetPriceTableData();
        RetrieveFixedPriceTableData();
        RetrieveTechnologyPriceTableData();
        RetrieveCasperCostTableData();
    }

}
function AreTechUnitsSelected() {
    var table = $("#tblSelectedTechnologies");
    var totalQty = 0;
    table.find('tr').each(function (i) {

        var quantity = $(this).find("td:eq(1) input[type='number']").val();
        totalQty += quantity;
    });

    if (totalQty > 0) {

        return true;
    } else {

        return false;
    }

}

function ValidateTblTechUnits() {
   
    if (AreTechUnitsSelected()) {
        $("#div-technologies-panel-header").css({
            "border-color": "#01a982"
        });
        UpdateDealOutputs();
        return true;

    } else {
        $("#div-technologies-panel-header").css({
            "border-color": "#DC3E0C"
        });
    }


}
function UPDTblTechInputs() {
    var table = $("#tblSelectedTechnologies");
    var list = [];

    table.find('tr').each(function (i) {

        var quantity = $(this).find("td:eq(1) input[type='number']").val();
        var monitoring = $(this).find("td:eq(2) input[type='checkbox']").is(':checked');
        var opsAdmin = $(this).find("td:eq(3) input[type='checkbox']").is(':checked');
        var $tds = $(this).find('td'),
            technology = $tds.eq(0).text();

        list.push({
            "Description": technology,
            "Quantity": quantity,
            "HasMonitoring": monitoring,
            "HasOperAndAdmin": opsAdmin
        });
    });



    $.ajax({
        type: 'POST',
        url: '/Deal/UPDSelectedTechnologies',
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify({ 'list': list }),
        dataType: "json",
        async: false,
        success: function (response) {
            ProcessResponse(response);
        },
        failure: function (response) {
            ProcessErrorResponse(response);
        }
    });

}
function UPDDealInputs() {
    var deal = {
        "Location": $("#txtLocationCost").text(),
        "SLA": $("#txtSLA").text(),
        "RegionAOH": $("#txtAOH").text(),
        "TargetMargin": $("#txtTargetMargin").val()
    };

    $.ajax({
        type: 'POST',
        url: '/Deal/UPDDealInputs',
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify(deal),
        dataType: "json",
        async: false,
        success: function (response) {
            ProcessResponse(response);
        },
        failure: function (response) {
            ProcessErrorResponse(response);
        }
    });

}
function ProcessResponse(response) {
    
}
function ProcessErrorResponse(response) {
    alert("Error");
}

function RetrieveGenNetPriceTableData() {

    $.ajax({
        type: 'GET',
        url: '/Deal/RETTBLGeneralNetPriceData',
        contentType: 'application/json; charset=utf-8',
        dataType: "json",
        success: function (response) {
            UpdateTBLGenNetPrice(response);
        },
        failure: function (response) {
            ProcessErrorResponse(response);
        }
    });

}
function UpdateTBLGenNetPrice(response) {
    $("#tblNetPricePerYear").find("tr:gt(0)").remove();
    var table = $("#tblNetPricePerYear");
    $(function() {
        $.each(response.list, function (i) {

            var $tr = $('<tr>').append(
                $('<td>').text(response.list[i].Detail),
                $('<td>').text("$ "+  (response.list[i].ValueAmounts[0]).formatMoney(2)),
                $('<td>').text("$ " + (response.list[i].ValueAmounts[1]).formatMoney(2)),
                $('<td>').text("$ " + (response.list[i].ValueAmounts[2]).formatMoney(2)),
                $('<td>').text("$ " + (response.list[i].ValueAmounts[3]).formatMoney(2))
            );
            table.append($tr);
        });
    });

}

function RetrieveFixedPriceTableData() {

    $.ajax({
        type: 'GET',
        url: '/Deal/RETTBLFixedPriceData',
        contentType: 'application/json; charset=utf-8',
        dataType: "json",
        success: function (response) {
            UpdateTBLFixedPrice(response);
        },
        failure: function (response) {
            ProcessErrorResponse(response);
        }
    });

}
function UpdateTBLFixedPrice(response) {
    $("#tblFixedPricePerYear").find("tr:gt(0)").remove();
    var table = $("#tblFixedPricePerYear");
    $(function () {
        $.each(response.list, function (i) {

            var $tr = $('<tr>').append(
                $('<td>').text(response.list[i].Detail),
                $('<td>').text("$ " + (response.list[i].ValueAmounts[0]).formatMoney(2)),
                $('<td>').text("$ " + (response.list[i].ValueAmounts[1]).formatMoney(2)),
                $('<td>').text("$ " + (response.list[i].ValueAmounts[2]).formatMoney(2)),
                $('<td>').text("$ " + (response.list[i].ValueAmounts[3]).formatMoney(2))
            );
            table.append($tr);
        });
    });

}
function RetrieveTechnologyPriceTableData() {

    $.ajax({
        type: 'GET',
        url: '/Deal/RETTBLTechnologyPriceData',
        contentType: 'application/json; charset=utf-8',
        dataType: "json",
        success: function (response) {
            UpdateTBLTechnologyPrice(response);
        },
        failure: function (response) {
            ProcessErrorResponse(response);
        }
    });

}
function UpdateTBLTechnologyPrice(response) {
    $("#tblTechnologyPrice").find("tr:gt(0)").remove();
    var table = $("#tblTechnologyPrice");
    $(function () {
        $.each(response.list, function (i) {

            var $tr = $('<tr>').append(
                $('<td>').text(response.list[i].Detail),
                $('<td>').text("$ " + (response.list[i].ValueAmounts[0]).formatMoney(2)),
                $('<td>').text("$ " + (response.list[i].ValueAmounts[1]).formatMoney(2)),
                $('<td>').text("$ " + (response.list[i].ValueAmounts[2]).formatMoney(2)),
                $('<td>').text("$ " + (response.list[i].ValueAmounts[3]).formatMoney(2))
            );
            table.append($tr);
        });
    });

}

function RetrieveCasperCostTableData() {

    $.ajax({
        type: 'GET',
        url: '/Deal/RETTBLCasperCostData',
        contentType: 'application/json; charset=utf-8',
        dataType: "json",
        success: function (response) {
            UpdateTBLCasperCost(response);
        },
        failure: function (response) {
            ProcessErrorResponse(response);
        }
    });

}
function UpdateTBLCasperCost(response) {
    $("#tblCasperCostInput").find("tr:gt(0)").remove();
    var table = $("#tblCasperCostInput");
    $(function () {
        $.each(response.list, function (i) {

            var $tr = $('<tr>').append(
                $('<td>').text(response.list[i].Detail),
                $('<td>').text("$ " + (response.list[i].ValueAmounts[0]).formatMoney(2))
            );
            table.append($tr);
        });
    });

}

function ValidateDealInputs() {

    if (($("#txtLocationCost").text() != "Select a location cost") && ($("#txtSLA").text() != "Select SLA")
        && ($("#txtAOH").text() != "Select a Region") && ($("#txtTargetMargin").val() <= 100 && $("#txtTargetMargin").val() > 0)) {
        
        return true;
    } else {
        return false;
    }
}
function UPDTxtLocationCost(pli) {

        $("#txtLocationCost").text($(pli).text());
        $("#txtLocationCost").css({
            "border-color": "#01a982",
            "border-width": "2px",
            "border-style": "solid"
        });
   if ($("#txtLocationCost").text() != "Select a location cost") {
        UpdateDealOutputs();
    } 
}
function validatePercentageRange() {

}
Number.prototype.formatMoney = function (c, d, t) {
    var n = this,
        c = isNaN(c = Math.abs(c)) ? 2 : c,
        d = d == undefined ? "." : d,
        t = t == undefined ? "," : t,
        s = n < 0 ? "-" : "",
        i = String(parseInt(n = Math.abs(Number(n) || 0).toFixed(c))),
        j = (j = i.length) > 3 ? j % 3 : 0;
    return s + (j ? i.substr(0, j) + t : "") + i.substr(j).replace(/(\d{3})(?=\d)/g, "$1" + t) + (c ? d + Math.abs(n - i).toFixed(c).slice(2) : "");
};


function UPDTxtSLA(pli) {


        $("#txtSLA").text($(pli).text());
        $("#txtSLA").css({
            "border-color": "#01a982",
            "border-width": "2px",
            "border-style": "solid"
        });
        if ($("#txtSLA").text() != "Select SLA") {
        UpdateDealOutputs();
        }
}
function UPDTxtAOH(pli) {


        $("#txtAOH").text($(pli).text());
        $("#txtAOH").css({
            "border-color": "#01a982",
            "border-width": "2px",
            "border-style": "solid"
        });
    if ($("#txtAOH").text() != "Select a Region") {
        UpdateDealOutputs();
    }

}
function UPDTxtTargetMargin() {

    if ($("#txtTargetMargin").val() == 35) {
        $("#txtTargetMargin").css({
            "border-color": "#01a982",
            "border-width": "2px",
            "border-style": "solid"
        });
       
    } else {
        $("#txtTargetMargin").css({
            "border-color": "#FF8D6D",
            "border-width": "2px",
            "border-style": "solid"
        });
    }
    if ($("#txtTargetMargin").val() > 100 || $("#txtTargetMargin").val() < 0) {
        $("#txtTargetMargin").css({
            "border-color": "#DC3E0C",
            "border-width": "2px",
            "border-style": "solid"
        });
        return false;
    } else {
        UpdateDealOutputs();
    }

    return true;
}
function changeColor(txt) {
    $(txt).css({
        "border-color": "#01a982",
        "border-width": "2px",
        "border-style": "solid"
    });
}
function UPDAllMonit() {
    var table = $("#tblSelectedTechnologies");
     
    if ($("#chkbxSelectAllMonit").is(':checked')) {
        table.find('tr').each(function (i) {
            $(this).find("td:eq(2) input[type='checkbox']").prop('checked', true);
        });

    } else {
        table.find('tr').each(function (i) {
            $(this).find("td:eq(2) input[type='checkbox']").prop('checked', false);
        });
    }

    UpdateDealOutputs();
}
function UPDAllOpsAdmin() {
    var table = $("#tblSelectedTechnologies");

    if ($("#chkbxSelectAllOpsAdmin").is(':checked')) {
        table.find('tr').each(function (i) {

            $(this).find("td:eq(3) input[type='checkbox']").prop('checked', true);

        });

    } else {
        table.find('tr').each(function (i) {

            $(this).find("td:eq(3) input[type='checkbox']").prop('checked', false);

        });
    }
    UpdateDealOutputs();
}