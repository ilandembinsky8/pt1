function startCaspit(price) {
    priceString = price
    $.ajax({
        type: 'GET',
        url: 'http://localhost/exampleService/startCaspit/?val=' + priceString,
        success: function (data) {
            console.log(data);
            var $xml = $(data)
            console.log($xml.find("AshStatus"));
            if (parseInt($xml.find("AshStatus")) == 0) {
                printorders(localStorage.getItem("cards"));
                addtransaction("אשראי",price);

                $('#light').html('<div  id="titlepop" style="padding-top:50px;color:green;" class="titlePopUP" > התשלום בוצע בהצלחה </div>')
                setTimeout(() => {
                    location.href = 'index.html'
                }, 5000);
            }else {
                location.href = 'index.html'
            }

        },
        dataType: 'json'
    });


}