function addtransaction(type,totall){

    $.ajax({
        type: 'GET',
        url: 'http://localhost/exampleservice/addtransaction/'+type+"/"+totall,
        success: function (data) {
            console.log(data);
        },
        dataType: 'json'
    });
}