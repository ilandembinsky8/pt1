function printorders(xml){
    var data1 = {
        "input": xml,
    };
    // We need to turn this object into JSON.  I use the JSON2.js library for this.
    var json = JSON.stringify(data1);

    $.ajax({
        type: 'POST',
        data: json,
        url: 'http://localhost/exampleService/printOrder/',
        success: function (data) {
            console.log(data);
        },
        contentType: "application/json",
        dataType: 'json'
    });
}
