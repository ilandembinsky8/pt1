function convertToCSV(objArray) {
    var array = typeof objArray != 'object' ? JSON.parse(objArray) : objArray;
    var str = '';

    for (var i = 0; i < array.length; i++) {
        var line = '';
        for (var index in array[i]) {
            if (line != '') line += ','

            line += array[i][index];
        }

        str += line + '\r\n';
    }

    return str;
}

function exportCSVFile(headers, items, fileTitle) {
    if (headers) {
        items.unshift(headers);
    }

    // Convert Object to JSON
    var jsonObject = JSON.stringify(items);

    var csv = this.convertToCSV(jsonObject);

    var exportedFilenmae = fileTitle + '.csv' || 'export.csv';

    var blob = new Blob([csv], {
        type: 'text/csv;charset=Windows-1255;'
    });
    if (navigator.msSaveBlob) { // IE 10+
        navigator.msSaveBlob(blob, exportedFilenmae);
    } else {
        var link = document.createElement("a");
        if (link.download !== undefined) { // feature detection
            // Browsers that support HTML5 download attribute
            var url = URL.createObjectURL(blob);
            link.setAttribute("href", url);
            link.setAttribute("download", exportedFilenmae);
            link.style.visibility = 'hidden';
            document.body.appendChild(link);
            link.click();
            document.body.removeChild(link);
        }
    }
}

function download1() {


    var headers = {
        invoiceNo: "InvoiceNo".replace(/,/g, ''), // remove commas to avoid errors
        date: "Date",
        time: "Time",
        total: "Total",
        type: "Type"
    };
    var itemsFormatted = [];

    itemsNotFormatted = JSON.parse(localStorage.getItem("dataTable"))

    var type
    // format the data
    itemsNotFormatted.forEach((item) => {
            type="visa"
            console.log(item.type);
        if (item.type == "מזומן     "){
            type = "cash"
        }

        itemsFormatted.push({
            invoiceNo: item.invoiceNo, // remove commas to avoid errors,
            date: item.date.substr(0,10),
            time: item.time.substr(0,8),
            total: item.total,
            type: type
        });
    });

    var fileTitle = 'Transactions'; // or 'my-unique-title'

    exportCSVFile(headers, itemsFormatted, fileTitle); // call the exportCSVFile() function to process the JSON and trigger the download
}

function download2() {


    var headers = {
        TranDate: "Date",
        Total_Count: "Amount",
        Total_Amount: "Total",
        Total_Cash: "Amount Cash",
        Total_Cash_Amount: "Total Cash",
        Total_Visa: "Amount Visa",
        Total_Visa_Amount: "Total Visa"
    };
    var itemsFormatted = [];

    itemsNotFormatted = JSON.parse(localStorage.getItem("dataTable"))

    var type
    // format the data
    itemsNotFormatted.forEach((item) => {

        itemsFormatted.push({
            TranDate: item.TranDate.substr(0,10), // remove commas to avoid errors,
            Total_Count: item.Total_Count,
            Total_Amount: item.Total_Amount,
            Total_Cash: item.Total_Cash,
            Total_Cash_Amount: item.Total_Cash_Amount,
            Total_Visa: item.Total_Visa,
            Total_Visa_Amount: item.Total_Visa_Amount,
        });
    });

    var fileTitle = 'TransactionsSummary'; // or 'my-unique-title'

    exportCSVFile(headers, itemsFormatted, fileTitle); // call the exportCSVFile() function to process the JSON and trigger the download
}