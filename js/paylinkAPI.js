function returnCashAfterCancel(cash){
    $.ajax({
        type: 'GET',
        url: 'http://localhost/exampleService/getRemainder/?val='+cash,
        async: false,
        success: function (data) {
            console.log(data);
            finishPaylink();
            
            $('#light').html('<div  id="titlepop" style="padding-top:50px;color:red;" class="titlePopUP" > העסקה בוטלה ! </div>')
            setTimeout(() => {
                location.href='index.html'
            }, 5000);
            //document.getElementById('light').style.display='none';
            //document.getElementById('fade').style.display='none';
        },
        dataType: 'json'
    });
}
function getCash(){
    $.ajax({
        type: 'POST',
        url: 'http://localhost/exampleService/getcash/',
        dataType: "json",
        contentType: "application/json; charset=utf-8",
            // data: myObject,                
                success: function (data) {
                    $("#cashValue").html('₪'+data);
        }
});    
}
function getRemainder(totall,cash){
    var remain=parseFloat(cash)-parseFloat(totall);
    var cards=JSON.parse(localStorage.getItem("cards"));
    
    if(remain>0 && cards.length>0 && totall> 0){
    $.ajax({
        type: 'GET',
        url: 'http://localhost/exampleService/getRemainder/?val='+remain,
        async: false,
        success: function (data) {
            console.log(data);
            
            if(localStorage.getItem("cards") != null){
                printorders(localStorage.getItem("cards"));
                addtransaction("מזומן",totall);
                localStorage.removeItem("cards");
    
            }
            
            $('#light').html('<div  id="titlepop" style="padding-top:50px;color:green;" class="titlePopUP" > התשלום בוצע בהצלחה </div>')
            finishPaylink();
            setTimeout(() => {
                location.href='index.html'
            }, 5000);
            //document.getElementById('light').style.display='none';
            //document.getElementById('fade').style.display='none';
        },
        dataType: 'json'
    });
    }else //if
    if(cards.length !=0 && remain==0){
        if(localStorage.getItem("cards") != null){
            printorders(localStorage.getItem("cards"));
            addtransaction("מזומן",totall);
            localStorage.removeItem("cards");

        }
        cards=[];
       // localStorage.setItem("cards",null);
        $('#light').html('<div  id="titlepop" style="padding-top:50px;color:green;" class="titlePopUP" > התשלום בוצע בהצלחה </div>')
        setTimeout(() => {
            location.href='index.html'
            finishPaylink();
        }, 2000);   
     }

}
function startPaylink(){
    $.ajax({
        type: 'GET',
        async: false,
        url: 'http://localhost/exampleService/startSdkPaylink/',
        success: function (data) {
            console.log(data);
        },
        dataType: 'json'
    }); 
}
function finishPaylink(){
    $.ajax({
        type: 'GET',
        async: false,
        url: 'http://localhost/exampleService/finishSdkPaylink/',
        success: function (data) {
            console.log(data);
        },
        dataType: 'json'
    }); 
}