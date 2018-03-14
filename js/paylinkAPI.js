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
    
    if(remain>=0 && cards.length !=0 && totall!=0){
    $.ajax({
        type: 'GET',
        url: 'http://localhost/exampleService/getRemainder/?val='+remain,
        success: function (data) {
            console.log(data);
            
            printorders(localStorage.getItem("cards"));
            $('#light').html('<div  id="titlepop" style="padding-top:50px;color:green;" class="titlePopUP" > התשלום בוצע בהצלחה </div>')
            setTimeout(() => {
                location.href='index.html'
            }, 5000);
            //document.getElementById('light').style.display='none';
            //document.getElementById('fade').style.display='none';
        },
        dataType: 'json'
    });
    }//if
    if(cards.length !=0 && totall==0){
        printorders(localStorage.getItem("cards"));
        cards=[];
        localStorage.setItem("cards",null);
        $('#light').html('<div  id="titlepop" style="padding-top:50px;color:green;" class="titlePopUP" > התשלום בוצע בהצלחה </div>')
        setTimeout(() => {
            location.href='index.html'
        }, 2000);   
     }

}
function startPaylink(){
    $.ajax({
        type: 'GET',
        url: 'http://localhost/exampleService/startPaylink/',
        success: function (data) {
            console.log(data);
        },
        dataType: 'json'
    }); 
}