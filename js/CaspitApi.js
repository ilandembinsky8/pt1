function startCaspit(price){
    priceString=price
    $.ajax({
        type: 'GET',
        url: 'http://localhost/exampleService/startCaspit/?val='+priceString,
        success: function (data) {
            console.log(data);
        },
        dataType: 'json'
    });   


}