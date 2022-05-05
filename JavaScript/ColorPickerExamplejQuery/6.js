//DOCUMENT READY

// $(() => { 

// });

$(document).ready(function () { //calback func.
    $(".form-range").on("input", changeColor); //callback func.
    changeColor();

    $("#picker-div").on("click", copyClipboard);
    $("#picker-div").on("mouseover", divMouseOver);
    $("#picker-div").on("mouseleave", divMouseLeave);
});

const divMouseOver = () => { 
    const pickerDiv = $("#picker-div");
    
    // pickerDiv.addClass("animate__animated");
    // pickerDiv.addClass("animate__pulse");

    pickerDiv.toggleClass("animate__animated");
    pickerDiv.toggleClass("animate__pulse");
};

const divMouseLeave = () => { 
    const pickerDiv = $("#picker-div");
    
    // pickerDiv.removeClass("animate__animated");
    // pickerDiv.removeClass("animate__pulse");

    pickerDiv.toggleClass("animate__animated");
    pickerDiv.toggleClass("animate__pulse");
};

//tıklandığı zaman içindeki metni kopyalar
const copyClipboard = () => {
    Swal.fire({
        icon: 'question',
        title: 'Kopyalansın mı?',
        showCancelButton: true,
        confirmButtonText: 'Kopyala',
        cancelButtonText: `İptal`,
    }).then((result) => {
        /* Read more about isConfirmed, isDenied below */
        //console.log(result);
        if (result.isConfirmed) {
            const pickerDiv = $("#picker-div");
            //kopyalama işlemi yaptığımız js kodu
            navigator.clipboard.writeText(pickerDiv.html());
            Swal.fire('Kopyalandı!', pickerDiv.html(), 'success')
        } else if (result.isDismissed) {
            Swal.fire('İptal', 'Kopyalama işlemi iptal edildi', 'info')
        }
    })
};

const changeColor = () => {
    const rangeRed = $("#range-red");
    const rangeGreen = $("#range-green");
    const rangeBlue = $("#range-blue");

    const pickerDiv = $("#picker-div");

    //console.log([rangeRed.val(), rangeGreen.val(), rangeBlue.val()]);

    const color = `rgb(${rangeRed.val()},${rangeGreen.val()},${rangeBlue.val()})`;
    const colorRev = `rgb(${255 - rangeRed.val()},${255 - rangeGreen.val()},${255 - rangeBlue.val()})`;
    //console.log(color)

    // pickerDiv.html(color);
    // pickerDiv.css("background-color", color);
    // pickerDiv.css("color", colorRev);
    pickerDiv.html(color).css("background-color", color).css("color", colorRev);
};