function addtransaction(type,totall){

    $.ajax({
        type: 'GET',
        url: 'http://localhost/exampleservice/addtransaction/'+type+"/"+totall,
        async:false,
        success: function (data) {
            console.log(data);
            var jsonData = JSON.parse(data)
            console.log(jsonData[0].invoiceNo);
            //invoiceNo

            localStorage.setItem("InvoiceNo",jsonData[0].invoiceNo+"")
        },
        dataType: 'json'
    });
}
function grouptransaction(from,to){

    $.ajax({
        type: 'GET',
        url: 'http://localhost/exampleservice/groupTran/'+from+"/"+to,
        success: function (data) {
            console.log(data);
        },
        dataType: 'json'
    });
}
function selecttransaction(from,to){

    $.ajax({
        type: 'GET',
        url: 'http://localhost/exampleservice/SelectTran/'+from+"/"+to,
        success: function (data) {
            console.log(data);
        },
        dataType: 'json'
    });
}