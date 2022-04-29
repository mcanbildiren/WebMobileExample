function mesaj() {
  //alert("Merhaba JavaScript");
  var pElement = document.getElementById("icerik");
    pElement.innerHTML = "<strong>Merhaba JavaScript</strong>";
    
}

//degişken tanımlama
var degisken1 = "Ahmet";
degisken1 = 123;
degisken1 = 1.4;
degisken1 = true;

degisken1 = [1, 2, 3];
degisken1[3] = "Kamil";
degisken1[10] = "Kamil";
console.log(degisken1);

degisken1 = {
  ad: "Kamil",
  soyad: "Fıdıl",
  yas: 23,
  notlar: [50, 60, 70],
  sinavlar: [
    {
      ad: "Matematik",
      not: 50,
    },
    {
      ad: "Fizik",
      not: 60,
    },
    {
      ad: "Kimya",
      not: 70,
    },
  ],
  meslek: "Çalışan",
  bilgileriGoster: function () {
    return this.ad + " " + this.soyad + " " + this.yas + " " + this.meslek;
  },
};
degisken1.tckn = "12345453432";
degisken1.falan = function () {
  return "filan";
};

function fibonacciHesapla(sayi) {
  if (sayi == 0) return 0;
  if (sayi == 1) return 1;
  return fibonacciHesapla(sayi - 1) + fibonacciHesapla(sayi - 2);
}