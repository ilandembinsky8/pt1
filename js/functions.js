function showCanvasGrade(cName, x1, w) {
    var canvas = document.getElementById(cName);
    canvas.width = 944 * w;
    canvas.height = 220 * w;
    var ctx = canvas.getContext("2d");
    var ctx1 = canvas.getContext("2d");
    var ctx2 = canvas.getContext("2d");
    var ctx3 = canvas.getContext("2d");
    var ctx4 = canvas.getContext("2d");

    var grd = ctx.createLinearGradient(0, 170, 0, 0);
    grd.addColorStop(0, "#233a7a");
    grd.addColorStop(1, "#009ed8");

    ctx.font = "18px Arial";

    ctx.fillStyle = grd;
    ctx.fillRect(0*w, 20*w, 221*w, 200*w);
    ctx.fillRect(241*w, 80*w, 221*w, 140*w);
    ctx.fillRect(482*w, 60*w, 221*w, 160*w);
    ctx.fillRect(723*w, 120*w, 221*w, 100*w);
    ctx.fillStyle = '#FFFFFF';
    ctx.fillText("90%", 95*w, 200*w);
    ctx.fillStyle = '#FFFFFF';
    ctx.fillText("60%", 335*w, 200*w);
    ctx.fillStyle = '#FFFFFF';
    ctx.fillText("70%", 575*w, 200*w);
    ctx.fillStyle = '#FFFFFF';
    ctx.fillText("40%", 813*w, 200*w);
    ctx.lineWidth = 5 * w;

    ctx.beginPath();
    ctx.strokeStyle = "#74B847";
    ctx.moveTo(110*w, 40);
    ctx.lineTo(351*w, 30);
    ctx.lineTo(592*w, 50);
    ctx.lineTo(813*w, 50);
    ctx.fillStyle = '#74B847';
    ctx.fillText("9", 110*w, 30);
    ctx.fillText("10", 351*w, 20);
    ctx.fillText("8", 592*w, 40);
    ctx.fillText("8", 813*w, 40);
    ctx.stroke();

    ctx1.beginPath();
    ctx1.strokeStyle = "#ee4822";
    ctx1.moveTo(110*w, 60);
    ctx1.lineTo(351*w, 40);
    ctx1.lineTo(592*w, 30);
    ctx1.lineTo(813*w, 80);
    ctx.fillStyle = '#ee4822';
    ctx.fillText("7", 110*w, 50);
    ctx.fillText("9", 351*w, 30);
    ctx.fillText("10", 592*w, 20);
    ctx.fillText("5", 813*w, 70);
    ctx1.stroke();

}

function showCanvas(cName, v1, v2, v3, image, w) {
    var canvas = document.getElementById(cName);
    canvas.width = 300*w;
    canvas.height = 300*w;
    var ctx = canvas.getContext("2d");
    var ctx1 = canvas.getContext("2d");
    var ctx2 = canvas.getContext("2d");
    var ctx3 = canvas.getContext("2d");
    var imageObj = new Image();

    


    ctx.beginPath();
    ctx.lineWidth = 12*w;
    ctx.strokeStyle = "#f28310";
    if (v1 > 0.25)
        ctx.arc(137.5 * w, 125 * w, 100 * w, 0.5 * Math.PI, v1 * 2.5 * Math.PI);
    else
        ctx.arc(137.5 * w, 125 * w, 100 * w, 0.5 * Math.PI, 0.25 * 2.5 * Math.PI);
    ctx.stroke();
    ctx1.beginPath();
    ctx1.lineWidth = 12*w;
    ctx1.strokeStyle = "#74b847";
    ctx1.moveTo(137.5 * w, 210 * w);
    if (v2>0.25)
        ctx1.arc(137.5 * w, 125 * w, 85 * w, 0.5 * Math.PI, v2 * 2.5 * Math.PI);
    else
        ctx1.arc(137.5 * w, 125 * w, 85 * w, 0.5 * Math.PI, 0.25 * 2.5 * Math.PI);
    ctx1.stroke();
    ctx2.beginPath();
    ctx2.lineWidth = 12*w;
    ctx2.strokeStyle = "#1fabaa";
    ctx2.moveTo(137.5 * w, 195 * w);
    if (v3>0.25)
        ctx2.arc(137.5 * w, 125 * w, 70 * w, 0.5 * Math.PI, v3 * 2.5 * Math.PI);
    else
        ctx2.arc(137.5 * w, 125 * w, 70 * w, 0.5 * Math.PI, 0.25 * 2.5 * Math.PI);
    ctx2.stroke();
    
    ctx3.font = "" + 12 * w + "px Arial";
    //if (v1 > 0.95)
        ctx3.fillStyle = '#FFFFFF';
    //else
    //    ctx3.fillStyle = '#ee4822';
    ctx3.fillText(v1 * 100 + "%", 110 * w, 227 * w);
    //if (v2 > 0.95)
        ctx3.fillStyle = '#FFFFFF';
    //else
    //    ctx3.fillStyle = '#74b847';
    ctx3.fillText(v2 * 100 + "%", 110 * w, 212 * w);
    //if (v3 > 0.95)
        ctx3.fillStyle = '#FFFFFF';
    //else
    //    ctx3.fillStyle = '#1fabaa';
    ctx3.fillText(v3 * 100 + "%", 110*w, 197*w);
    imageObj.onload = function () {
        ctx3.drawImage(imageObj, 100*w, 90*w, 80*w, 80*w);
    };
    imageObj.src = image;
}