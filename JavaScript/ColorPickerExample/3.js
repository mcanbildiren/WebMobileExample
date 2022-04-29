var changeColor = () => {
  var rangeRed = document.getElementById("range-red");
  var rangeGreen = document.getElementById("range-green");
  var rangeBlue = document.getElementById("range-blue");

  var pickerDiv = document.getElementById("picker-div");
  var color = `rgb(${rangeRed.value},${rangeGreen.value},${rangeBlue.value})`;
  var colorRev = `rgb(${255 - rangeRed.value},${255 - rangeGreen.value},${
    255 - rangeBlue.value
  })`;

  pickerDiv.innerHTML = color;
  pickerDiv.style.backgroundColor = color;
  pickerDiv.style.color = colorRev;
};
var copyClipboard = () => {
  Swal.fire({
    icon: "question",
    title: "Kopyalansın mı?",
    showCancelButton: true,
    confirmButtonText: "Kopyala",
    cancelButtonText: "İptal",
  }).then((result) => {
    if (result.isConfirmed) {
      var pickerDiv = document.getElementById("picker-div");
      navigator.clipboard.writeText(pickerDiv.innerHTML);
      Swal.fire("Kopyalandı!", pickerDiv.innerHTML, "success");
    } else if (result.isDenied) {
      Swal.fire("İptal", "Kopyalama işlemi iptal edildi.", "info");
    }
  });
};

var divMouseOver = () => {
  var pickerDiv = document.getElementById("picker-div");

  pickerDiv.classList.add("animate__animated");
  pickerDiv.classList.add("animate__pulse");
};

var divMouseLeave = () => {
  var pickerDiv = document.getElementById("picker-div");

  pickerDiv.classList.remove("animate__animated");
  pickerDiv.classList.remove("animate__pulse");
};

changeColor();
